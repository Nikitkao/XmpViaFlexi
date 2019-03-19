using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Cirrious.FluentLayouts.Touch;

namespace VacationsTracker.iOS.Themes
{
    public static class AppStyles
    {
        public static UIButton SetPrimaryStyle(this UIButton button, string text = null)
        {
            if (text != null)
            {
                button.SetTitle(text, UIControlState.Normal);
            }

            button.AddConstraints(button.Height().EqualTo(AppDimens.DefaultButtonHeight));

            button.SetMainBackgroundColorStyle();

            return button;
        }

        public static UILabel SetHeadline1Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.GrayColor, 30);

            return label;
        }

        public static UILabel SetHeadline2Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.LightBlueColor, AppDimens.DefaultDurationFontSize);

            return label;
        }

        public static UILabel SetDisplay1Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.LightBlueColor, 60);

            return label;
        }

        public static UILabel SetDisplay2Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.LightBlueColor, 30);

            return label;
        }

        public static UILabel SetDisplay3Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.LightBlueColor, 25);

            return label;
        }

        public static UILabel SetDisplay4Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.LightGreenColor, 60);

            return label;
        }

        public static UILabel SetDisplay5Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.LightGreenColor, 30);

            return label;
        }

        public static UILabel SetDisplay6Style(this UILabel label, string text = null)
        {
            label.SetDisplayStyle(text, AppColors.LightGreenColor, 25);

            return label;
        }

        public static UIView SetMainBackgroundColorStyle(this UIView view)
        {
            view.BackgroundColor = AppColors.LightBlueColor;

            return view;
        }

        public static UISegmentedControl SetPrimarySegmentControlStyle(this UISegmentedControl segmentedControl)
        {
            segmentedControl.TintColor = AppColors.LightGreenColor;

            return segmentedControl;
        }

        public static UIPageControl SetPrimaryPagerStyle(this UIPageControl pageControl)
        {
            pageControl.PageIndicatorTintColor = AppColors.GrayColor;
            pageControl.CurrentPageIndicatorTintColor = AppColors.LightBlueColor;

            return pageControl;
        }

        public static UIDatePicker SetPrimaryDatePickerStyle(this UIDatePicker picker)
        {
            picker.SetValueForKey(AppColors.LightBlueColor, new NSString("textColor"));

            return picker;
        }

        public static UIDatePicker SetSecondaryDatePickerStyle(this UIDatePicker picker)
        {
            picker.SetValueForKey(AppColors.LightGreenColor, new NSString("textColor"));

            return picker;
        }

        public static UILabel SetErrorLabelStyle(this UILabel label, string text)
        {
            label.Text = text;

            label.BackgroundColor = AppColors.WhiteColor;
            label.TextColor = AppColors.ErrorTextColor;
            label.TextAlignment = UITextAlignment.Center;

            label.WrapText();

            label.SetCornerRadiusTo(5);

            return label;
        }

        public static UITextField SetDefaultTextFieldStyle(this UITextField textField, string placeholder = null)
        {
            if (placeholder != null)
            {
                textField.Placeholder = placeholder;
            }

            textField.BackgroundColor = AppColors.WhiteColor;
            textField.SetLeftPaddingTo(5);

            return textField;
        }

        public static UILabel SetSubhead1Style(this UILabel label)
        {
            label.SetDefaultFontSize();
            label.TextColor = AppColors.GrayColor;

            return label;
        }

        public static UIView SetSeparator1Style(this UIView separator)
        {
            separator.BackgroundColor = AppColors.LightBlueColor;

            return separator;
        }

        public static UIView SetSeparator2Style(this UIView separator)
        {
            separator.BackgroundColor = AppColors.GrayColor;

            return separator;
        }

        private static void SetDefaultFontSize(this UILabel label)
        {
            label.SetFontSize(AppDimens.DefaultFontSize);
        }

        private static void SetFontSize(this UILabel label, int fontSize)
        {
            label.Font = UIFont.SystemFontOfSize(fontSize);
        }

        private static void SetBoldFontSize(this UILabel label, int fontSize)
        {
            label.Font = UIFont.BoldSystemFontOfSize(fontSize);
        }

        private static void SetDisplayStyle(this UILabel label, string text, UIColor textColor, int fontSize)
        {
            label.SetText(text);
            label.SetBoldFontSize(fontSize);
            label.TextColor = textColor;
        }

        private static void SetLeftPaddingTo(this UITextField textField, int paddingWidth)
        {
            var paddingView = new UIView(new CGRect(0, 0, paddingWidth, AppDimens.DefaultTextFieldHeight));
            textField.LeftView = paddingView;
            textField.LeftViewMode = UITextFieldViewMode.Always;
        }

        private static void WrapText(this UILabel label)
        {
            label.Lines = 0;
        }

        private static void SetCornerRadiusTo(this UIView label, int cornerRadius)
        {
            label.Layer.MasksToBounds = true;
            label.Layer.CornerRadius = cornerRadius;
        }

        private static void SetText(this UILabel label, string text)
        {
            if (text != null)
            {
                label.Text = text;
            }
        }
    }
}
