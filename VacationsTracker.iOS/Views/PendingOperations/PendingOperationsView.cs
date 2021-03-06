﻿using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;
using VacationsTracker.iOS.Views.Home.VacationsTable;

namespace VacationsTracker.iOS.Views.PendingOperations
{
    public class PendingOperationsView : LayoutView
    {
        public UITableView VacationsTableView { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.White;

            VacationsTableView = new UITableView();
            VacationsTableView.RegisterClassForCellReuse(
                typeof(PendingOperationItemCell),
                PendingOperationItemCell.CellId);

            VacationsTableView.RefreshControl = new UIRefreshControl();

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

            this.AddConstraints(VacationsTableView.FullSizeOf(this));
        }
    }
}
