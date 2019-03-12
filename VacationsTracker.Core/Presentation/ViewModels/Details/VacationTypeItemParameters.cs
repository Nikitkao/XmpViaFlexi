using FlexiMvvm;
using VacationsTracker.Core.Data;

namespace VacationsTracker.Core.Presentation.ViewModels.Details
{
    public class VacationTypeItemParameters : ViewModelBundleBase
    {
        public VacationTypeItemParameters(VacationType type)
        {
            VacationType = type;
        }

        public VacationType VacationType
        {
            get => (VacationType) Bundle.GetInt();
            set => Bundle.SetInt((int) value);
        }
    }
}
