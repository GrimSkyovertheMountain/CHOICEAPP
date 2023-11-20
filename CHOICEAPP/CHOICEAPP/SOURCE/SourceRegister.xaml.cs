using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using CHOICEAPP;
using CHOICEAPP.MODELS;
using System.Threading;

namespace CHOICEAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SourceRegister : ContentPage
    {

        public SourceRegister()
        {
            InitializeComponent();
            this.BindingContext = new UserModel();

        }
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            string alias = aliasEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            int NoDigits = 4;
            Random rnd = new Random();
            int num = rnd.Next((int)Math.Pow(10, (NoDigits - 1)), (int)Math.Pow(10, NoDigits) - 1);

            try
            {
                SqlConnection db = new SqlConnection();
                if (db.State == System.Data.ConnectionState.Closed)
                {
                    db.Open();
                    if (string.IsNullOrWhiteSpace(alias) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
                    {
                        registrationStatus.Text = "All fields are required.";
                    }
                    else if (!IsValidEmail(email))
                    {
                        registrationStatus.Text = "Invalid email format.";
                    }
                    else if (password != confirmPassword)
                    {
                        registrationStatus.Text = "Passwords do not match.";
                    }
                    else
                    {

                        SqlCommand cmd = new SqlCommand("INSERT INTO UserModel(ID, UserType, Allias,Password,Email) VALUES(@id, @usertype, @name, @password, @email)", db);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", num);
                        cmd.Parameters.AddWithValue("@usertype", 2);
                        cmd.Parameters.AddWithValue("@name", alias);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.ExecuteNonQuery();

                        registrationStatus.Text = "Registration successful!";
                        PageLogin();
                    }

                }
                else
                {
                    await DisplayAlert("Database is Closed", "Closed", "OK");
                }
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Registration Failed", ex.Message, "OK");
            }
        }

        async void PageLogin()
        {
            await Navigation.PushAsync(new LoginDesign());
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}