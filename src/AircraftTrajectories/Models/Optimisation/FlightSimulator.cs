using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.Optimisation
{
    using Trajectory;
    using MathNet.Numerics.Interpolation;
    using TemporalGrid;
    public enum VERTICAL_STATE { TAKEOFF, CLIMB, FREECLIMB, END }
    public enum HORIZONTAL_STATE { STRAIGHT, TURN }

    /// <summary>
    /// Class that simulates a flight, given an aircraft, an end position and a set of control parameters
    /// </summary>
    public class FlightSimulator
    {
        protected ISimulatorModel _aircraft;
        protected VERTICAL_STATE _vertical_state;
        protected HORIZONTAL_STATE _horizontal_state;
        protected Point3D _endPoint;
        protected int _numberOfSegments;

        // Constants
        protected const int MAX_SEGMENT_LENGTH = 15000;
        protected const int MAX_TURN_RADIUS = 8000;

        // State variables
        public double _x;
        public double _y;
        protected double _height;
        protected double _speed;
        protected double _heading;
        protected double _angle;
        protected double _bankAngle;

        // Segment variables
        protected int _segmentIndex;
        protected Point3D _segmentStartPoint;
        protected double _segmentStartHeading;

        // Optimisation data
        public int duration;
        public double fuel;

        // Trajectory data
        protected List<double> _settings;
        protected List<double> _xData;
        protected List<double> _yData;
        protected List<double> _zData;
        protected List<double> _tData;

        /// <summary>
        /// Create a new FlightSimulator object
        /// </summary>
        /// <param name="aircraft">The aircraft that will be used in the simulation</param>
        /// <param name="endPoint">The position the aircraft will fly towards</param>
        /// <param name="numberOfSegments">The number of segments used in the simulation. The simulation starts and ends with a straight segment, so the number of segments should be odd.</param>
        /// <param name="settings">The control parameters, which are a list of doubles between 0 and 1. The number of controls needed for any number of segments can be calculated by calling TrajectoryChromosome.ChromosomeLength().</param>
        public FlightSimulator(ISimulatorModel aircraft, Point3D endPoint, int numberOfSegments, List<double> settings)
        {
            _settings = settings;
            _vertical_state = VERTICAL_STATE.TAKEOFF;
            _aircraft = aircraft;
            _speed = 160;
            _heading = Math.PI / 2;
            _endPoint = endPoint;
            _numberOfSegments = numberOfSegments;
            _segmentIndex = 1;

            _xData = new List<double>();
            _yData = new List<double>();
            _zData = new List<double>();
            _tData = new List<double>();

        }

        /// <summary>
        /// Start the simulation process
        /// </summary>
        public void Simulate()
        {
            duration = 0;
            NoisePowerDistance.Instance.LAMaxGrid = null;
            while (_vertical_state != VERTICAL_STATE.END)
            {
                _xData.Add(_x);
                _yData.Add(_y);
                _zData.Add(_height * 0.3048);
                _tData.Add(duration);

                updateNoise();
                updatePosition();
                updateVerticalState();
                updateHorizontalState();
                duration++;
            }
            LAMaxGrid = NoisePowerDistance.Instance.LAMaxGrid;
        }


        public Grid LAMaxGrid;
        /// <summary>
        /// Update the noise values underneath the aircraft trajectory
        /// </summary>
        public void updateNoise()
        {
            if (duration % 10 != 0) { return; }
            var NPD = NoisePowerDistance.Instance;
            NPD.CalculateNoise(new Point3D(_x,_y, _height * 0.3048, CoordinateUnit.metric), CurrentThrust()/4.0*0.2248);
        }

        /// <summary>
        /// Convert the simulation into a trajectory object
        /// </summary>
        /// <returns>A trajectory object representing the simulation</returns>
        public Trajectory createTrajectory()
        {
            var xSpline = CubicSpline.InterpolateNatural(_tData, _xData);
            var ySpline = CubicSpline.InterpolateNatural(_tData, _yData);
            var zSpline = CubicSpline.InterpolateNatural(_tData, _zData);

            var trajectory = new Trajectory(xSpline, ySpline, zSpline, null, null);
            trajectory.Duration = (int) _tData[_tData.Count - 1];
            
            return trajectory;
        }

        /// <summary>
        /// Return the setting at the given position for the segment we are currently in
        /// </summary>
        /// <param name="offset">The index at which we want to get the control parameter</param>
        /// <returns>The requested control parameter</returns>
        protected double Setting(int offset)
        {
            return _settings[TrajectoryChromosome.SegmentSettingIndex(_segmentIndex, offset)];
        }

        /// <summary>
        /// Update the position of the aircraft for 1sec
        /// </summary>
        protected void updatePosition()
        {
            double currentThrust = CurrentThrust();
            double currentDrag = _aircraft.Drag(_speed, _height);

            updateAngle(currentThrust, currentDrag);
            updateSpeed(currentThrust, currentDrag);
            UpdateBankAngle();
            _height += _speed * 1.6878099 * Math.Sin(_angle);
            _heading += (9.81 / _speed * 0.514444444 * Math.Tan(_bankAngle));
            _heading = (_heading + (2*Math.PI)) % (2 * Math.PI);
            _x += _speed * 0.514444444 * Math.Cos(_angle) * Math.Sin(_heading);
            _y += _speed * 0.514444444 * Math.Cos(_angle) * Math.Cos(_heading);
            fuel += _aircraft.FuelFLow(currentThrust, _speed, _height);
        }
        
        /// <summary>
        /// Update the flight path angle of the aircraft
        /// </summary>
        /// <param name="thrust">the current thrust for all engines combined</param>
        /// <param name="drag">the current drag</param>
        protected void updateAngle(double thrust, double drag)
        {
            double maxAngle = MaxAngle(thrust, drag);
            if (_vertical_state == VERTICAL_STATE.FREECLIMB)
            {
                _angle = Interpolate(0, maxAngle, Setting(0));
            } else {
                _angle = maxAngle;
            }
        }

        /// <summary>
        /// Update the bank angle of the aircraft
        /// </summary>
        public void UpdateBankAngle()
        {
            _bankAngle = 0;
            if (_horizontal_state == HORIZONTAL_STATE.TURN)
            {
                var radius = Interpolate(_aircraft.MinimumTurnRadius(_speed), MAX_TURN_RADIUS, Setting(2));
                var speedMs = _speed * 0.514444444;
                _bankAngle = Math.Atan(_speed * _speed / radius / 9.81);
                _bankAngle = (Setting(3) > 0.5) ? _bankAngle : -_bankAngle;
            }
        }

        /// <summary>
        /// Update the speed of the aircraft
        /// </summary>
        /// <param name="thrust">the current thrust for all engines combined</param>
        /// <param name="drag">the current drag</param>
        protected void updateSpeed(double thrust, double drag)
        {
            if (_vertical_state == VERTICAL_STATE.FREECLIMB) {
                _speed += (1/_aircraft.Mass) * (thrust - drag - ((_aircraft.Mass * 9.81) * Math.Sin(_angle)));
            }
        }

        /// <summary>
        /// Switch to a different vertical state if needed
        /// </summary>
        protected void updateVerticalState()
        {
            switch (_vertical_state)
            {
                case VERTICAL_STATE.TAKEOFF:
                    if (_height >= 1500) {
                        _vertical_state = VERTICAL_STATE.CLIMB;
                    }
                    break;
                case VERTICAL_STATE.CLIMB:
                    if (_height >= 3600 || _speed >= 250) {
                        _vertical_state = VERTICAL_STATE.FREECLIMB;
                    }
                    break;
            }
        }

        /// <summary>
        /// Switch to the next segment if needed
        /// </summary>
        protected void updateHorizontalState()
        {
            if (_vertical_state == VERTICAL_STATE.FREECLIMB) {
                if (_segmentStartPoint == null) {
                    _segmentStartPoint = new Point3D(_x, _y, 0, CoordinateUnit.metric);
                }
                switch (_horizontal_state)
                {
                    case HORIZONTAL_STATE.STRAIGHT:
                        CheckStraightEnd();
                        break;
                    case HORIZONTAL_STATE.TURN:
                        CheckEndOfTurn();
                        break;
                }
            }
        }

        /// <summary>
        /// Check whether we reached the end of a straight segment
        /// </summary>
        protected void CheckStraightEnd()
        {
            var currentPoint = new Point3D(_x, _y, 0, CoordinateUnit.metric);
            if (_segmentIndex == _numberOfSegments)
            {
                if (currentPoint.DistanceTo(_segmentStartPoint) >= _endPoint.DistanceTo(_segmentStartPoint))
                {
                    _vertical_state = VERTICAL_STATE.END;
                }
                return;
            }

            CheckStraightNotLastSegment(currentPoint);
        }

        /// <summary>
        /// Check whether we are at the end of a straight segment, that is not the last segment
        /// </summary>
        /// <param name="currentPoint">The current position</param>
        protected void CheckStraightNotLastSegment(Point3D currentPoint)
        {
            if (_segmentIndex == _numberOfSegments - 2 &&
                currentPoint.DistanceTo(_endPoint) / 2 < _aircraft.MinimumTurnRadius(_aircraft.VMax)) {
                return;
            }

            if (currentPoint.DistanceTo(_segmentStartPoint) >= Interpolate(0, MAX_SEGMENT_LENGTH, Setting(2)))
            {
                _horizontal_state = HORIZONTAL_STATE.TURN;
                _segmentStartHeading = _heading;
                _segmentIndex++;
            }
        }

        /// <summary>
        /// Check whether we reached the end of a turn
        /// </summary>
        public void CheckEndOfTurn()
        {
            double deltaHeading = Interpolate(-Math.PI / 2, Math.PI / 2, Setting(3));
            double targetHeading = _heading;
            bool switchHorizontalState = SwitchHorizontalState(deltaHeading, targetHeading);

            if (switchHorizontalState)
            {
                _horizontal_state = HORIZONTAL_STATE.STRAIGHT;
                _segmentStartPoint = new Point3D(_x, _y, 0, CoordinateUnit.metric);
                _heading = targetHeading;
                _segmentIndex++;
            }
        }

        /// <summary>
        /// Return whether we should switch the horizontal state
        /// </summary>
        /// <param name="deltaHeading">The heading </param>
        /// <param name="targetHeading"></param>
        /// <returns></returns>
        protected bool SwitchHorizontalState(double deltaHeading, double targetHeading)
        {
            bool switchHorizontalState = false;
            
            if (_segmentIndex == _numberOfSegments - 1)
            {
                var currentPoint = new Point3D(_x, _y, 0, CoordinateUnit.metric);
                targetHeading = currentPoint.HeadingTo(_endPoint) * Math.PI / 180;

                switchHorizontalState =
                    (deltaHeading <= 0 && AngleDifference(_heading, _segmentStartHeading) >= AngleDifference(targetHeading, _segmentStartHeading)) ||
                    (deltaHeading >= 0 && AngleDifference(_segmentStartHeading, _heading) >= AngleDifference(_segmentStartHeading, targetHeading));
            }
            else
            {
                targetHeading = (_segmentStartHeading + deltaHeading) % (2 * Math.PI);
                switchHorizontalState =
                    (deltaHeading <= 0 && AngleDifference(_heading, _segmentStartHeading) >= AngleDifference(targetHeading, _segmentStartHeading)) ||
                    (deltaHeading >= 0 && AngleDifference(_segmentStartHeading, _heading) >= AngleDifference(_segmentStartHeading, targetHeading));
            }
            return switchHorizontalState;
        }

        /// <summary>
        /// Return the angle difference from A to B
        /// </summary>
        /// <param name="a">The left most angle (in radians)</param>
        /// <param name="b">The right most angle (in radians)</param>
        /// <returns></returns>
        public double AngleDifference(double a, double b)
        {
            if (a > b)
            {
                if (a < Math.PI)
                {
                    return SmallAngleDifference(a, Math.PI) + Math.PI + SmallAngleDifference(0, b);
                }
                return SmallAngleDifference(a, 0) + SmallAngleDifference(0, b);
            }
            return SmallAngleDifference(a, b);
        }
        protected double SmallAngleDifference(double a, double b)
        {
            double phi = (a - b) % (Math.PI * 2);
            double distance = phi > Math.PI ? (Math.PI * 2) - phi : phi;
            return Math.Abs(distance);
        }

        /// <summary>
        /// Interpolate a value with the given factor between the given lower and upper bound
        /// </summary>
        /// <param name="min">the lower bound of the interpolation</param>
        /// <param name="max">the upper bound of the interpolation</param>
        /// <param name="factor">the factor in between the lower and upper bound</param>
        /// <returns></returns>
        protected double Interpolate(double min, double max, double factor)
        {
            return (max - min) * factor + min;
        }

        /// <summary>
        /// Calculate the maximum flight path angle at which the aircraft remains at the current speed
        /// </summary>
        /// <param name="thrust">the current thrust for all engines combined</param>
        /// <param name="drag">the current drag</param>
        /// <returns></returns>
        protected double MaxAngle(double thrust, double drag)
        {
            return Math.Asin((thrust - drag) / (_aircraft.Mass * 9.81));
        }

        /// <summary>
        /// Calculate the current thrust of the aircraft based on the thrust setting for the current segment
        /// </summary>
        /// <returns></returns>
        public double CurrentThrust()
        {
            switch (_vertical_state)
            {
                case VERTICAL_STATE.TAKEOFF:
                    return _aircraft.TakeOffThrust(_speed, _height);
                case VERTICAL_STATE.CLIMB:
                    return _aircraft.ClimbThrust(_speed, _height);
                default:
                    double maxThrust = _aircraft.ClimbThrust(_speed, _height);
                    return Interpolate(_aircraft.Drag(_speed, _height), maxThrust, Setting(1));
            }
        }
    }
}