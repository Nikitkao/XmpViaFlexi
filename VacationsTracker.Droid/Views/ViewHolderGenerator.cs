﻿// <auto-generated />
// ReSharper disable All
using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace VacationsTracker.Droid.Views
{
    public partial class DetailsActivityViewHolder
    {
         private readonly Activity activity;

         private TextView navTitle;
         private ProgressBar ctrlActivityIndicator;
         private LinearLayout rootLayout;
         private Android.Support.V4.View.ViewPager vacationTypePager;
         private Android.Support.Design.Widget.TabLayout tabDots;
         private RelativeLayout dateStart;
         private TextView vacationStartDay;
         private TextView vacationStartMonth;
         private TextView vacationStartYear;
         private RelativeLayout dateEnd;
         private TextView vacationEndDay;
         private TextView vacationEndMonth;
         private TextView vacationEndYear;
         private RadioGroup statusRadioGroup;
         private RadioButton approvedRadio;
         private RadioButton closedRadio;

        public DetailsActivityViewHolder( Activity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));

            this.activity = activity;
        }

        
        public TextView NavTitle =>
            navTitle ?? (navTitle = activity.FindViewById<TextView>(Resource.Id.nav_title));

        
        public ProgressBar CtrlActivityIndicator =>
            ctrlActivityIndicator ?? (ctrlActivityIndicator = activity.FindViewById<ProgressBar>(Resource.Id.ctrlActivityIndicator));

        
        public LinearLayout RootLayout =>
            rootLayout ?? (rootLayout = activity.FindViewById<LinearLayout>(Resource.Id.root_layout));

        
        public Android.Support.V4.View.ViewPager VacationTypePager =>
            vacationTypePager ?? (vacationTypePager = activity.FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.vacation_type_pager));

        
        public Android.Support.Design.Widget.TabLayout TabDots =>
            tabDots ?? (tabDots = activity.FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tab_dots));

        
        public RelativeLayout DateStart =>
            dateStart ?? (dateStart = activity.FindViewById<RelativeLayout>(Resource.Id.date_start));

        
        public TextView VacationStartDay =>
            vacationStartDay ?? (vacationStartDay = activity.FindViewById<TextView>(Resource.Id.vacation_start_day));

        
        public TextView VacationStartMonth =>
            vacationStartMonth ?? (vacationStartMonth = activity.FindViewById<TextView>(Resource.Id.vacation_start_month));

        
        public TextView VacationStartYear =>
            vacationStartYear ?? (vacationStartYear = activity.FindViewById<TextView>(Resource.Id.vacation_start_year));

        
        public RelativeLayout DateEnd =>
            dateEnd ?? (dateEnd = activity.FindViewById<RelativeLayout>(Resource.Id.date_end));

        
        public TextView VacationEndDay =>
            vacationEndDay ?? (vacationEndDay = activity.FindViewById<TextView>(Resource.Id.vacation_end_day));

        
        public TextView VacationEndMonth =>
            vacationEndMonth ?? (vacationEndMonth = activity.FindViewById<TextView>(Resource.Id.vacation_end_month));

        
        public TextView VacationEndYear =>
            vacationEndYear ?? (vacationEndYear = activity.FindViewById<TextView>(Resource.Id.vacation_end_year));

        
        public RadioGroup StatusRadioGroup =>
            statusRadioGroup ?? (statusRadioGroup = activity.FindViewById<RadioGroup>(Resource.Id.status_radio_group));

        
        public RadioButton ApprovedRadio =>
            approvedRadio ?? (approvedRadio = activity.FindViewById<RadioButton>(Resource.Id.approved_radio));

        
        public RadioButton ClosedRadio =>
            closedRadio ?? (closedRadio = activity.FindViewById<RadioButton>(Resource.Id.closed_radio));
    }

    public partial class LoginActivityViewHolder
    {
         private readonly Activity activity;

         private LinearLayout errorMessageLayout;
         private TextView errorMessageTextView;
         private EditText loginEditText;
         private EditText passwordEditText;
         private Button signInButton;

        public LoginActivityViewHolder( Activity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));

            this.activity = activity;
        }

        
        public LinearLayout ErrorMessageLayout =>
            errorMessageLayout ?? (errorMessageLayout = activity.FindViewById<LinearLayout>(Resource.Id.error_message_layout));

        
        public TextView ErrorMessageTextView =>
            errorMessageTextView ?? (errorMessageTextView = activity.FindViewById<TextView>(Resource.Id.error_message_text_view));

        
        public EditText LoginEditText =>
            loginEditText ?? (loginEditText = activity.FindViewById<EditText>(Resource.Id.login_edit_text));

        
        public EditText PasswordEditText =>
            passwordEditText ?? (passwordEditText = activity.FindViewById<EditText>(Resource.Id.password_edit_text));

        
        public Button SignInButton =>
            signInButton ?? (signInButton = activity.FindViewById<Button>(Resource.Id.sign_in_button));
    }

    public partial class MainListActivityViewHolder
    {
         private readonly Activity activity;

         private Android.Support.V4.Widget.DrawerLayout drawerLayout;
         private Android.Support.V4.Widget.SwipeRefreshLayout refresher;
         private Android.Support.V7.Widget.RecyclerView recyclerView;
         private Android.Support.Design.Widget.FloatingActionButton fab;
         private Android.Support.Design.Widget.NavigationView navigationView;

        public MainListActivityViewHolder( Activity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));

            this.activity = activity;
        }

        
        public Android.Support.V4.Widget.DrawerLayout DrawerLayout =>
            drawerLayout ?? (drawerLayout = activity.FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawer_layout));

        
        public Android.Support.V4.Widget.SwipeRefreshLayout Refresher =>
            refresher ?? (refresher = activity.FindViewById<Android.Support.V4.Widget.SwipeRefreshLayout>(Resource.Id.refresher));

        
        public Android.Support.V7.Widget.RecyclerView RecyclerView =>
            recyclerView ?? (recyclerView = activity.FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.recycler_view));

        
        public Android.Support.Design.Widget.FloatingActionButton Fab =>
            fab ?? (fab = activity.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab));

        
        public Android.Support.Design.Widget.NavigationView NavigationView =>
            navigationView ?? (navigationView = activity.FindViewById<Android.Support.Design.Widget.NavigationView>(Resource.Id.navigation_view));
    }

    public partial class VacationCellViewHolder
    {
         private ImageView vacationImage;
         private TextView vacationDuration;
         private TextView vacationType;
         private TextView vacationStatus;
         private View separatorView;



        
        public ImageView VacationImage =>
            vacationImage ?? (vacationImage = ItemView.FindViewById<ImageView>(Resource.Id.vacation_image));

        
        public TextView VacationDuration =>
            vacationDuration ?? (vacationDuration = ItemView.FindViewById<TextView>(Resource.Id.vacation_duration));

        
        public TextView VacationType =>
            vacationType ?? (vacationType = ItemView.FindViewById<TextView>(Resource.Id.vacation_type));

        
        public TextView VacationStatus =>
            vacationStatus ?? (vacationStatus = ItemView.FindViewById<TextView>(Resource.Id.vacation_status));

        
        public View SeparatorView =>
            separatorView ?? (separatorView = ItemView.FindViewById<View>(Resource.Id.separator_view));
    }

    public partial class VacationTypeFragmentViewHolder
    {
         private readonly View rootView;

         private ImageView imageVacationType;
         private TextView textViewVacationName;

        public VacationTypeFragmentViewHolder( View rootView)
        {
            if (rootView == null) throw new ArgumentNullException(nameof(rootView));

            this.rootView = rootView;
        }

        
        public ImageView ImageVacationType =>
            imageVacationType ?? (imageVacationType = rootView.FindViewById<ImageView>(Resource.Id.image_vacation_type));

        
        public TextView TextViewVacationName =>
            textViewVacationName ?? (textViewVacationName = rootView.FindViewById<TextView>(Resource.Id.text_view_vacation_name));
    }

    /*
    "LayoutDefinitionOptions" are not specified for "navigation_header" layout file therefore view holder can't be generated for it.
    public partial class HeaderNavigationViewHolder
    {
    }
    */

    /*
    "LayoutDefinitionOptions" are not specified for "navigation_menu" layout file therefore view holder can't be generated for it.
    public partial class MenuNavigationViewHolder
    {
    }
    */

    /*
    "LayoutDefinitionOptions" are not specified for "toolbar" layout file therefore view holder can't be generated for it.
    public partial class ToolbarViewHolder
    {
    }
    */

}
// ReSharper restore All
