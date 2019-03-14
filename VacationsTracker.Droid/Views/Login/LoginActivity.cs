using Android.App;
using Android.OS;
using FlexiMvvm.Bindings;
using FlexiMvvm.ValueConverters;
using FlexiMvvm.Views.V7;
using VacationsTracker.Core.Presentation.ViewModels.Login;

namespace VacationsTracker.Droid.Views
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : FlxBindableAppCompatActivity<LoginViewModel>
    {
        private LoginActivityViewHolder ViewHolder { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            ViewHolder = new LoginActivityViewHolder(this);
        }

        public override void Bind(BindingSet<LoginViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(ViewHolder.LoginEditText)
                .For(v => v.TextChangedBinding())
                .To(vm => vm.Login);

            bindingSet.Bind(ViewHolder.PasswordEditText)
                .For(v => v.TextChangedBinding())
                .To(vm => vm.Password);

            bindingSet.Bind(ViewHolder.ErrorMessageLayout)
                .For(v => v.Visibility)
                .To(vm => vm.ErrorVisibility)
                .WithConvertion<VisibleGoneVisibilityValueConverter>();
      
            bindingSet.Bind(ViewHolder.SignInButton)
                .For(v => v.ClickBinding())
                .To(vm => vm.LoginCommand);

            bindingSet.Bind(ViewHolder.ErrorMessageTextView)
                .For(v => v.Text)
                .To(vm => vm.ErrorMessage);
        }
    }
}
