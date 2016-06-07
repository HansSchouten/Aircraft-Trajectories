using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.Optimisation
{
    using AircraftTrajectories.Models.Trajectory;
    using MathNet.Numerics.Interpolation;
    public enum VERTICAL_STATE { TAKEOFF, CLIMB, FREECLIMB, END }
    public enum HORIZONTAL_STATE { STRAIGHT, TURN }

    public class FlightSimulator
    {
        protected void Log(string message)
        {
            ///Console.WriteLine(message);
        }

        protected const int MAX_SEGMENT_LENGTH = 15000;
        protected const int MAX_TURN_RADIUS = 8000;

        protected ISimulatorModel _aircraft;
        protected VERTICAL_STATE _vertical_state;
        protected HORIZONTAL_STATE _horizontal_state;
        protected Point3D _endPoint;
        protected int _numberOfSegments;

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

        protected List<double> _settings;
        protected List<double> _xData;
        protected List<double> _yData;
        protected List<double> _zData;
        protected List<double> _tData;

        public int duration;
        public double fuel;

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

        public void Simulate()
        {
            duration = 0;
            //Console.WriteLine("A: " + A + " B:" + B + " C:" + C);
            while (_vertical_state != VERTICAL_STATE.END)
            {
                _xData.Add(_x);
                _yData.Add(_y);
                _zData.Add(_height * 0.3048);
                _tData.Add(duration);

                updatePosition();
                updateVerticalState();
                updateHorizontalState();
                duration++;
                //Console.WriteLine("A: " + _angle + " H:" + _height + " V:" + _speed);
            }
            Log("X:"+_x+" Y:"+_y);
            //Console.WriteLine(duration + " " + fuel);
        }

        public Trajectory createTrajectory()
        {
            var xSpline = CubicSpline.InterpolateNatural(_tData, _xData);
            var ySpline = CubicSpline.InterpolateNatural(_tData, _yData);
            var zSpline = CubicSpline.InterpolateNatural(_tData, _zData);

            var trajectory = new Trajectory(xSpline, ySpline, zSpline, null, null);
            trajectory.Duration = (int) _tData[_tData.Count - 1];
            
            return trajectory;
        }

        protected double Setting(int offset)
        {
            return _settings[TrajectoryChromosome.SegmentSettingIndex(_segmentIndex, offset)];
        }

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
            //Console.WriteLine(_x + " " + _y + " " + _heading);

            /*
            if (_height >= 0) {
                Console.WriteLine(_aircraft.TakeOffThrust(160, 1640));
                Console.WriteLine(currentThrust + " " + currentDrag);
                Console.WriteLine(_aircraft.FuelFLow(currentThrust, _speed, _height));
            }
            */

            fuel += _aircraft.FuelFLow(currentThrust, _speed, _height);
        }

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

        protected void updateSpeed(double thrust, double drag)
        {
            if (_vertical_state == VERTICAL_STATE.FREECLIMB) {
                //Console.WriteLine("T:" + thrust + " D:" + drag + " A:"+_angle);
                _speed += (1/_aircraft.Mass) * (thrust - drag - ((_aircraft.Mass * 9.81) * Math.Sin(_angle)));
            }
        }

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
                default:
                    /*
                    if (_height >= 6000 || _speed >= _aircraft.VClean) {
                        Console.WriteLine("Trajectory reached max height or VClean");
                        _vertical_state = VERTICAL_STATE.END;
                    }
                    */
                    /*
                    // force stop:
                    if (duration >= 750) {
                        Console.WriteLine("Trajectory timeout");
                        fuel = int.MaxValue;
                        _vertical_state = VERTICAL_STATE.END;
                    }
                    */
                    break;
            }
        }

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

        protected void CheckStraightEnd()
        {
            var currentPoint = new Point3D(_x, _y, 0, CoordinateUnit.metric);
            // At the last straight segment, quit the optimisation when we reach the endpoint
            if (_segmentIndex == _numberOfSegments)
            {
                if (currentPoint.DistanceTo(_segmentStartPoint) >= _endPoint.DistanceTo(_segmentStartPoint))
                {
                    Log("Reached end of segment" + _segmentIndex + " (straight) X:" + _x + " Y:" + _y + " Heading:" + _heading * 180 / Math.PI);
                    _vertical_state = VERTICAL_STATE.END;
                }
            }
            else
            {
                // At the second last straight segment, 
                // continue flying as long as we cannot reach the endpoint with a minimum radius turn in less than a 180degree turn
                if (_segmentIndex == _numberOfSegments - 2 && 
                    currentPoint.DistanceTo(_endPoint)/2 < _aircraft.MinimumTurnRadius(_aircraft.VMax))
                {
                    Log("Continue X:"+_x);
                    return;
                }

                // Switch from flying straight to a turn if we reach beyond the given segment length
                if (currentPoint.DistanceTo(_segmentStartPoint) >= Interpolate(0, MAX_SEGMENT_LENGTH, Setting(2)))
                {
                    Log("Reached end of segment" + _segmentIndex + " (straight) X:"+_x + " Y:"+_y + " Heading:"+_heading*180/Math.PI);
                    _horizontal_state = HORIZONTAL_STATE.TURN;
                    _segmentStartHeading = _heading;
                    _segmentIndex++;
                }
            }
        }

        public void CheckEndOfTurn()
        {
            bool switchHorizontalState = false;
            double deltaHeading = Interpolate(-Math.PI / 2, Math.PI / 2, Setting(3));
            double targetHeading = _heading;

            // Check whether we are in the last turn of the trajectory
            if (_segmentIndex == _numberOfSegments - 1)
            {
                var currentPoint = new Point3D(_x, _y, 0, CoordinateUnit.metric);
                targetHeading = currentPoint.HeadingTo(_endPoint) * Math.PI / 180;
                /*
                Console.WriteLine((_heading * 180 / Math.PI) + " to " + targetHeading * 180 / Math.PI + " seg:"+ _segmentStartHeading * 180 / Math.PI);
                Console.WriteLine(AngleDifference(_heading, _segmentStartHeading));
                Console.WriteLine(AngleDifference(_segmentStartHeading, targetHeading));
                Console.WriteLine(deltaHeading);
                Console.WriteLine(Setting(3));
                */

                switchHorizontalState =
                    (deltaHeading < 0 && AngleDifference(_heading, _segmentStartHeading) >= AngleDifference(targetHeading, _segmentStartHeading)) ||
                    (deltaHeading > 0 && AngleDifference(_segmentStartHeading, _heading) >= AngleDifference(_segmentStartHeading, targetHeading));
            }
            else
            {
                targetHeading = (_segmentStartHeading + deltaHeading) % (2 * Math.PI);
                switchHorizontalState =
                    (deltaHeading < 0 && AngleDifference(_heading, _segmentStartHeading) >= AngleDifference(targetHeading, _segmentStartHeading)) ||
                    (deltaHeading > 0 && AngleDifference(_segmentStartHeading, _heading) >= AngleDifference(_segmentStartHeading, targetHeading));
            }

            if (switchHorizontalState || deltaHeading == 0)
            {
                Log("Reached end of segment" + _segmentIndex + " (turn) X:" + _x + " Y:" + _y + " Heading:" + _heading * 180 / Math.PI);

                _horizontal_state = HORIZONTAL_STATE.STRAIGHT;
                _segmentStartPoint = new Point3D(_x, _y, 0, CoordinateUnit.metric);
                _heading = targetHeading;
                _segmentIndex++;
            }
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

        protected double Interpolate(double min, double max, double factor)
        {
            return (max - min) * factor + min;
        }

        protected double MaxAngle(double thrust, double drag)
        {
            return Math.Asin((thrust - drag) / (_aircraft.Mass * 9.81));
        }

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