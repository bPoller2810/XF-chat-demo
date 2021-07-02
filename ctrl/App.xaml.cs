using GalaSoft.MvvmLight.Ioc;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ctrl
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SimpleIoc.Default.Register<IThemeService, ThemeService>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
