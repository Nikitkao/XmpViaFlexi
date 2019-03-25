using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using VacationsTracker.Core.Bootstrappers;
using VacationsTracker.Droid.Bootstrappers;

namespace VacationsTracker.Droid
{
    [Application]
    public class App : Application
    {
        public App(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            try
            {
                var config = new BootstrapperConfig();
                config.SetSimpleIoc(new SimpleIoc());

                var compositeBootstrapper = new CompositeBootstrapper(
                    new CoreBootstrapper(),
                    new AndroidBootstrapper());

                compositeBootstrapper.Execute(config);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}