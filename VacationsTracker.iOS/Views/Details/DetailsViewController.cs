using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using Foundation;
using UIKit;
using VacationsTracker.Core.Presentation.ViewModels.Details;
using VacationsTracker.iOS.Views.Login;

namespace VacationsTracker.iOS.Views.Details
{
    public class DetailsViewController : FlxBindableViewController<DetailsViewModel, VacationDetailsParameters>
    {
        public new DetailsView View
        {
            get => (DetailsView)base.View.NotNull();
            set => base.View = value;
        }

        public DetailsViewController(VacationDetailsParameters parameters) : base(parameters)
        {

        }

        public override void LoadView()
        {
            View = new DetailsView();
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);
        }
    }
}