using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;
using VacationsTracker.Core.Resources;
using VacationsTracker.iOS.Themes;

namespace VacationsTracker.iOS.Views.Login
{
    public class LoginView : LayoutView
    {
        public UITextField LoginTextFiled { get; set; }
        
        public UITextField PasswordTextField { get; set; }

        public UIButton LoginButton { get; private set; }

        public UIImageView BackgroundImage { get; set; }

        public UILabel ErrorMessage { get; set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.White;

            BackgroundImage = new UIImageView(UIImage.FromFile("Login_bg.jpg"));

            LoginTextFiled = new UITextField().SetPrimaryStyle(Strings.LoginPage_LoginPlaceholder);

            PasswordTextField = new UITextField().SetPrimaryStyle(Strings.LoginPage_PasswordPlaceholder);

            LoginButton = new UIButton().SetPrimaryStyle(Strings.LoginPage_SignIn);

            ErrorMessage = new UILabel().SetErrorLabelStyle();
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            AddSubview(BackgroundImage);
            AddSubview(LoginTextFiled);
            AddSubview(PasswordTextField);
            AddSubview(LoginButton);
            AddSubview(ErrorMessage);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(BackgroundImage.FullSizeOf(this));

            this.AddConstraints(
                LoginButton.BelowCenterOf(this, 40),
                LoginButton.AtLeftOf(PasswordTextField, 10),
                LoginButton.AtRightOf(PasswordTextField, 10));

            this.AddConstraints(
                PasswordTextField.Above(LoginButton, 15),
                PasswordTextField.AtLeftOf(this, 40),
                PasswordTextField.AtRightOf(this, 40));

            this.AddConstraints(
                LoginTextFiled.Above(PasswordTextField, 10),
                LoginTextFiled.AtLeftOf(this, 40),
                LoginTextFiled.AtRightOf(this, 40));

            this.AddConstraints(
                ErrorMessage.Above(LoginTextFiled, 20),
                ErrorMessage.AtLeftOf(this, 40),
                ErrorMessage.AtRightOf(this, 40));
        }
    }
}
