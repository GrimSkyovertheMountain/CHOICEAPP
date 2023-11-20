using CHOICEAPP.MODELS;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHOICEAPP
{
    public partial class LoginDesign : ContentPage
    {
        private DatabaseHelper databaseHelper = new DatabaseHelper();

        public LoginDesign()
        {
            InitializeComponent();

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new UserRegistration());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string username = txtEmail.Text;
            string password = txtPassword.Text;

            UserModel user = await databaseHelper.GetUserAsync(username, password);

            if (user != null)
            {
                
                
                if (user.UserType == 2)
                {
                    await DisplayAlert("Login Successful", "Welcome, " + user.Name + "!", "OK");
                    await Navigation.PushAsync(new SourceHomePage()); // Replace with your Source
                }
                else if (user.UserType == 1)
                {
                    await DisplayAlert("Login Successful", "Welcome, " + user.Name + "!", "OK");
                    await Navigation.PushAsync(new DoneeHomePage()); // Replace with your Donee
                }
                else
                {
                    await DisplayAlert("We couldnt Find your Account", "Invalid, " + user.Name + "!", "Register");

                }
            }
            else
            {
                // Failed login
                await DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }

    }
}

