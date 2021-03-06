﻿using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using JetBrains.Annotations;
using UIKit;
using VacationsTracker.Core.Presentation.ValueConverters;
using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.PendingOperations;
using VacationsTracker.iOS.Views.Home.VacationsTable;
using VacationsTracker.iOS.Views.ValueConverters;

namespace VacationsTracker.iOS.Views.PendingOperations
{
    public class PendingOperationItemCell : UITableViewBindableItemCell<PendingOperationsViewModel, VacationCellViewModel>
    {
        protected internal PendingOperationItemCell(IntPtr handle)
    : base(handle)
        {
        }

        public static string CellId { get; } = nameof(PendingOperationItemCell);

        private VacationItemView View { get; set; }

        public override void LoadView()
        {
            View = new VacationItemView();

            ContentView.NotNull().AddSubview(View);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            ContentView.AddConstraints(View.FullSizeOf(ContentView));

            SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        public override void Bind(BindingSet<VacationCellViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(View.StatusLabel)
                .For(v => v.Text)
                .To(vm => vm.Status)
                .WithConvertion<EnumToStringConverter>();

            bindingSet.Bind(View.TypeLabel)
                .For(v => v.Text)
                .To(vm => vm.Type)
                .WithConvertion<EnumToStringConverter>();

            bindingSet.Bind(View.DurationLabel)
                .For(v => v.Text)
                .To(vm => vm.Duration)
                .WithConvertion<DurationToStringConverter>("MMM dd");

            bindingSet.Bind(View.TypeImage)
                .For(v => v.Image)
                .To(vm => vm.Type)
                .WithConvertion<TypeToImageValueConverter>();
        }
    }
}
