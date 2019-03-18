using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;
using VacationsTracker.iOS.Themes;

namespace VacationsTracker.iOS.Views.Home.VacationsTable
{
    internal class VacationItemView : LayoutView
    {
        public UIImageView TypeImage { get; private set; }

        public UILabel DurationLabel { get; private set; }

        public UILabel TypeLabel { get; private set; }

        public UILabel StatusLabel { get; private set; }

        public UIView Separator { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            TypeImage = new UIImageView();

            DurationLabel = new UILabel().SetHeadline2Style();

            TypeLabel = new UILabel().SetSubhead1Style();

            StatusLabel = new UILabel().SetSubhead1Style();

            Separator = new UIView().SetSeparator2Style();
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(TypeImage)
                .AddLayoutSubview(DurationLabel)
                .AddLayoutSubview(TypeLabel)
                .AddLayoutSubview(StatusLabel)
                .AddLayoutSubview(Separator);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                TypeImage.AtLeftOf(this, AppDimens.Inset1X),
                TypeImage.AtTopOf(this, AppDimens.Inset1X),
                TypeImage.AtBottomOf(this, AppDimens.Inset1X),
                TypeImage.WithSameCenterY(this),
                TypeImage.Height().EqualTo(AppDimens.DefaultTypeImageSize),
                TypeImage.Width().EqualTo(AppDimens.DefaultTypeImageSize));

            this.AddConstraints(
                DurationLabel.ToRightOf(TypeImage, AppDimens.Inset1X),
                DurationLabel.AtTopOf(TypeImage, AppDimens.InsetHalf));

            this.AddConstraints(
                TypeLabel.ToRightOf(TypeImage, AppDimens.Inset1X),
                TypeLabel.AtBottomOf(TypeImage, AppDimens.InsetHalf));

            this.AddConstraints(
                StatusLabel.AtRightOf(this, AppDimens.Inset1X),
                StatusLabel.WithSameCenterY(this));

            this.AddConstraints(
                Separator.WithSameLeft(DurationLabel),
                Separator.WithSameRight(this),
                Separator.WithSameBottom(this),
                Separator.Height().EqualTo(AppDimens.DefaultSeparatorSize));
        }
    }
}