using CHOICEAPP.MODELS;
using CHOICEAPP.NOTIFICATION;
using CHOICEAPP.SOURCE;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHOICEAPP
{
    public partial class App : Application
    {
       

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage( new LoginDesign());

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
