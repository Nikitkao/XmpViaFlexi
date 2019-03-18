using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using Foundation;
using UIKit;
using VacationsTracker.iOS.Views.Home.VacationsTable;

namespace VacationsTracker.iOS.Views.Home
{
    public class HomeView : LayoutView
    {
        public UITableView VacationsTableView { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.White;


            VacationsTableView = new UITableView();
            VacationsTableView.RegisterClassForCellReuse(
                typeof(VacationItemViewCell),
                VacationItemViewCell.CellId);

            VacationsTableView.RefreshControl = new UIRefreshControl();

            VacationsTableView.BackgroundColor = UIColor.Brown;
            //VacationsTableView.AllowsSelection = true;
            VacationsTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            AddSubview(VacationsTableView);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(VacationsTableView.WithSameRight(this),
                VacationsTableView.WithSameLeft(this),
                VacationsTableView.WithSameTop(this),
                VacationsTableView.WithSameBottom(this));
        }
    }
}