using FlexiMvvm;
using VacationsTracker.Core.Data;

namespace VacationsTracker.Core.Presentation.ViewModels.Details
{
    public class VacationTypeItemViewModel : ViewModelBase<VacationTypeItemParameters>
    {
        public VacationType VacationType { get; private set; }

        protected override void Initialize(VacationTypeItemParameters parameters)
        {
            base.Initialize(parameters);
            VacationType = parameters.NotNull().VacationType;
        }
    }
}
