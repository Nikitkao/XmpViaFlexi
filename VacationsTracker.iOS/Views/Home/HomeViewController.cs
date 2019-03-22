using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using FlexiMvvm.Views;
using UIKit;
using VacationsTracker.Core.Presentation.ViewModels.Home;
using VacationsTracker.Core.Resources;
using VacationsTracker.iOS.Views.Home.VacationsTable;

namespace VacationsTracker.iOS.Views.Home
{
    public class HomeViewController : FlxBindableViewController<HomeViewModel>
    {
        private UITableViewObservablePlainSource VacationsSource { get; set; }

        //private UIBarButtonItem OperationsButton { get; } = new UIBarButtonItem(Strings.Operations_Title, UIBarButtonItemStyle.Done, null);

        private UIBarButtonItem AddButton { get; } = new UIBarButtonItem("Add", UIBarButtonItemStyle.Done, null);

        public new HomeView View
        {
            get => (HomeView)base.View.NotNull();
            set => base.View = value;
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //NavigationItem.LeftBarButtonItem = OperationsButton;
            NavigationItem.RightBarButtonItem = AddButton;

            await ViewModel.LoadVacations();
        }

        public override void LoadView()
        {
            View = new HomeView();

            NavigationController.NavigationBar.Hidden = false;

            VacationsSource = new UITableViewObservablePlainSource(
                View.VacationsTableView,
                _ => VacationItemViewCell.CellId)
            {
                Items = ViewModel.Vacations,
                ItemsContext = ViewModel
            };

            Title = Strings.HomePage_Title;
            View.VacationsTableView.Source = VacationsSource;
        }

        public override void Bind(BindingSet<HomeViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(VacationsSource)
                .For(v => v.RowSelectedBinding())
                .To(vm => vm.VacationSelectedCommand);

            bindingSet.Bind(View.VacationsTableView.RefreshControl)
                .For(v => v.BeginRefreshingBinding())
                .To(vm => vm.Busy);

            bindingSet.Bind(View.VacationsTableView.RefreshControl)
                .For(v => v.EndRefreshingBinding())
                .To(vm => vm.Busy);

            bindingSet.Bind(View.VacationsTableView.RefreshControl)
                .For(v => v.ValueChangedBinding())
                .To(vm => vm.RefreshCommand);

            //bindingSet.Bind(OperationsButton)
                //.For(v => v.NotNull().ClickedBinding())
                //.To(vm => vm.OpenOperationsCommand);

            bindingSet.Bind(AddButton)
                .For(v => v.NotNull().ClickedBinding())
                .To(vm => vm.AddCommand);
        }
    }
}