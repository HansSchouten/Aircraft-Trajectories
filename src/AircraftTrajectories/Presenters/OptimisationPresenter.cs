
using AircraftTrajectories.Views.Optimisation;
using System;

namespace AircraftTrajectories.Presenters
{
    public class OptimisationPresenter
    {
        protected IOptimisationForm _view;

        public OptimisationPresenter(IOptimisationForm view)
        {
            _view = view;
            _view.RunOptimisation += delegate (object sender, EventArgs e) { RunOptimisation(); };
            _view.CancelOptimisation += delegate (object sender, EventArgs e) { CancelOptimisation(); };
        }

        private void RunOptimisation()
        {

        }

        private void CancelOptimisation()
        {

        }
    }
}