using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.ValueConverters;
using FlexiMvvm.Views;
using VacationsTracker.Core.Presentation.ViewModels.Login;

namespace VacationsTracker.iOS.Views.Login
{
    public class LoginViewController : FlxBindableViewController<LoginViewModel>
    {
        public new LoginView View
        {
            get => (LoginView)base.View.NotNull();
            set => base.View = value;
        }

        public override void LoadView()
        {
            NavigationController.NavigationBar.Hidden = true;

            View = new LoginView();
        }
        
        public override void Bind(BindingSet<LoginViewModel> bindingSet)
        {
            bindingSet.Bind(View.LoginButton)
                .For(v => v.TouchUpInsideBinding())
                .To(vm => vm.LoginCommand);

            bindingSet.Bind(View.ErrorMessage)
                .For(v => v.Hidden)
                .To(vm => vm.ErrorVisibility)
                .WithConvertion<InvertValueConverter>();

            bindingSet.Bind(View.LoginTextFiled)
                .For(v => v.TextAndEditingChangedBinding())
                .To(vm => vm.Login);

            bindingSet.Bind(View.PasswordTextField)
                .For(v => v.TextAndEditingChangedBinding())
                .To(vm => vm.Password);
        }
    }
}