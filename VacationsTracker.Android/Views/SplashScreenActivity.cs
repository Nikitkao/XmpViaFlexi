using Android.App;
using Android.Content.PM;
using Android.OS;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using FlexiMvvm.Views.V7;
using Plugin.CurrentActivity;
using VacationsTracker.Android.Bootstrappers;
using VacationsTracker.Core.Bootstrappers;
using VacationsTracker.Core.Presentation.ViewModels;

namespace VacationsTracker.Android.Views
{
    [Activity(
        MainLauncher = true,
        NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : FlxAppCompatActivity<EntryViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            var config = new BootstrapperConfig();
            config.SetSimpleIoc(new SimpleIoc());

            var compositeBootstrapper = new CompositeBootstrapper(
                new CoreBootstrapper(),
                new AndroidBootstrapper());

            compositeBootstrapper.Execute(config);

            base.OnCreate(savedInstanceState);
        }
    }
}