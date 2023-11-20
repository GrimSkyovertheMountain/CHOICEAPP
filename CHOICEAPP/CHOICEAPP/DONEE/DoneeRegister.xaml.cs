using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Essentials;
using System.Data.SqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CHOICEAPP.MODELS;

namespace CHOICEAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoneeRegister : ContentPage
    {

        public DoneeRegister()
        {
            InitializeComponent();
            this.BindingContext = new UserModel();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
           
                string user = UserEntry.Text;
                string email = emailEntry.Text;
                string password = passwordEntry.Text;
                string confirmPassword = confirmPasswordEntry.Text;


            int NoDigits = 4;
            Random rnd = new Random();
            int num = rnd.Next((int)Math.Pow(10, (NoDigits - 1)), (int)Math.Pow(10, NoDigits) - 1);

            try
            {
                SqlConnection db = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\PATRICK\DOWNLOADS\CAPS.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
                if (db.State == System.Data.ConnectionState.Closed)
                {
                    db.Open();
                    if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
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
                    cmd.Parameters.AddWithValue("@usertype", 1);
                    cmd.Parameters.AddWithValue("@name", user);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();

                    registrationStatus.Text = "Registration successful!";
                    PageLogin();
                }
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