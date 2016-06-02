using System;

namespace AircraftTrajectories.Models.Optimisation
{
    public enum STATE { TAKEOFF, CLIMB, FREECLIMB, END }

    public class FlightSimulator
    {
        protected ISimulatorModel _aircraft;
        protected STATE _state;

        // State variables
        protected double _x;
        protected double _y;
        protected double _height;
        protected double _speed;
        protected double _heading;
        protected double _angle;

        public double A;
        public double B;
        public double C;

        public int duration;
        public double fuel;

        public FlightSimulator(ISimulatorModel aircraft)
        {
            _state = STATE.TAKEOFF;
            _aircraft = aircraft;
            _height = 0;
            _speed = 155;
            fuel = 0;
        }

        public void Simulate()
        {
            duration = 0;
            //Console.WriteLine("A: " + A + " B:" + B + " C:" + C);
            while (_state != STATE.END)
            {
                updateState();
                updatePosition();
                duration++;
                //Console.WriteLine("A: " + _angle + " H:" + _height + " V:" + _speed);
            }
            Console.WriteLine(duration + " " + fuel);
        }

        public void updatePosition()
        {
            double currentThrust = CurrentThrust();
            double currentDrag = _aircraft.Drag(_speed, _height);

            // Update angle
            double maxAngle = MaxAngle(currentThrust, currentDrag);
            if (_state == STATE.FREECLIMB) {
                _angle = Interpolate(0, maxAngle, B);
            } else {
                _angle = maxAngle;
            }

            updateSpeed(currentThrust, currentDrag);

            _height += _speed * 1.6878099 * Math.Sin(_angle);

            fuel += _aircraft.FuelFLow(currentThrust, _speed, _height);
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
            switch (_state)
            {
                case STATE.TAKEOFF:
                    return _aircraft.TakeOffThrust(_speed, _height);
                case STATE.CLIMB:
                    return _aircraft.ClimbThrust(_speed, _height);
                default:
                    double maxThrust = _aircraft.ClimbThrust(_speed, _height);
                    return Interpolate(_aircraft.Drag(_speed, _height), maxThrust, C);
            }
        }

        public void updateSpeed(double thrust, double drag)
        {
            if (_state == STATE.FREECLIMB)
            {
                //Console.WriteLine("T:" + thrust + " D:" + drag + " A:"+_angle);
                _speed += (1/_aircraft.Mass) * (thrust - drag - ((_aircraft.Mass * 9.81) * Math.Sin(_angle)));
            }
        }

        public void updateState()
        {
            switch (_state)
            {
                case STATE.TAKEOFF:
                    if (_height >= 1500) {
                        _state = STATE.CLIMB;
                    }
                    break;
                case STATE.CLIMB:
                    if (_height >= 3600 || _speed >= 250) {
                        _state = STATE.FREECLIMB;
                    }
                    break;
                case STATE.FREECLIMB:
                    if (_height >= 6000 || _speed >= _aircraft.VClean) {
                        _state = STATE.END;
                    }
                    // force stop:
                    if (duration >= 750) {
                        fuel = int.MaxValue;
                        _state = STATE.END;
                    }
                    break;
            }
        }
    }
}