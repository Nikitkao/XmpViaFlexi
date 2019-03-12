using FlexiMvvm;

namespace VacationsTracker.Core.Presentation.ViewModels.Details
{
    public class VacationDetailsParameters : ViewModelBundleBase
    {
        public VacationDetailsParameters(string vacationId)
        {
            VacationId = vacationId;
        }

        public string VacationId
        {
            get => Bundle.GetString();
            set => Bundle.SetString(value);
        }
    }
}
