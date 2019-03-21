using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using FlexiMvvm.Views;
using VacationsTracker.Core.Presentation.ViewModels.PendingOperations;
using VacationsTracker.Core.Resources;

namespace VacationsTracker.iOS.Views.PendingOperations
{
    public class PendingOperationsController : FlxBindableViewController<PendingOperationsViewModel>
    {
        private UITableViewObservablePlainSource VacationsSource { get; set; }

        public new PendingOperationsView View
        {
            get => (PendingOperationsView)base.View.NotNull();
            set => base.View = value;
        }

        public override void LoadView()
        {
            View = new PendingOperationsView();

            NavigationController.NavigationBar.Hidden = false;

            VacationsSource = new UITableViewObservablePlainSource(
                View.VacationsTableView,
                _ => PendingOperationItemCell.CellId)
            {
                Items = ViewModel.Vacations,
                ItemsContext = ViewModel
            };

            Title = Strings.Operations_Title;
            View.VacationsTableView.Source = VacationsSource;
        }

        public override void Bind(BindingSet<PendingOperationsViewModel> bindingSet)
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
        }
    }
}
