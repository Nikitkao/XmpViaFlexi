using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexiMvvm.Views;
using Foundation;
using UIKit;

namespace VacationsTracker.iOS.Views.Details
{
    public class DetailsView : LayoutView
    {
        public UITextField LoginTextFiled { get; set; }

        public UITextField PasswordTextField { get; set; }

        public UIButton LoginButton { get; private set; }

        public UIImageView BackgroundImage { get; set; }

        public UILabel ErrorMessage { get; set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.Yellow;

            BackgroundImage = new UIImageView(UIImage.FromFile("Login_bg.jpg"));

            LoginTextFiled = new UITextField
            {
                Placeholder = "Login",
                BackgroundColor = UIColor.White,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                SpellCheckingType = UITextSpellCheckingType.No
            };

            PasswordTextField = new UITextField()
            {
                Placeholder = "Password",
                BackgroundColor = UIColor.White,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                SpellCheckingType = UITextSpellCheckingType.No
            };

            LoginButton = new UIButton();
            LoginButton.SetTitle("sign in".ToUpper(), UIControlState.Normal);
            LoginButton.BackgroundColor = UIColor.Cyan;

            ErrorMessage = new UILabel
            {
                Text = "Please, retry your login and password pair. Check current Caps Lock and input settings",
                Lines = 5,
                BackgroundColor = UIColor.White,
                TextAlignment = UITextAlignment.Center
            };
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
        }
    }
}