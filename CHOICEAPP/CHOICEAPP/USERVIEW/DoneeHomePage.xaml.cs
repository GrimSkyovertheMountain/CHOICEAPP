using CHOICEAPP.MODELS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using CHOICEAPP.SOURCE;

namespace CHOICEAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoneeHomePage : ContentPage
    {
        public class MyTableList
        {
            public int ID { get; set; }
            public int UserType { get; set; }
            public string Name { get; set; }
            public string Height { get; set; }
            public string Weight { get; set; }
            public string EyeColor { get; set; }
            public string HairColor { get; set; }
            public string SkinColor { get; set; }
            public string Nationality { get; set; }
            public string Ethnicity { get; set; }


        }

    SqlConnection sqlConnection;

        public DoneeHomePage()
        {
            InitializeComponent();
            BindingContext = this;
            try
            {
                string serverdbname = "C:\\PROGRAM FILES\\MICROSOFT SQL SERVER\\MSSQL16.CHOICESERVER\\MSSQL\\DATA\\CAPS.MDF";
                string servername = "MSI\\CHOICESERVER";
                string serveruser = "";
                string serverpass = "";
                string sqlconn = $"Data Source={servername};Initial Catalog= {serverdbname};User ID= {serveruser};Password={serverpass}";
                sqlConnection = new SqlConnection(sqlconn);
                LoadUserType2FromDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async void LoadUserType2FromDatabase()
        {
            List<MyTableList> myTableLists = new List<MyTableList>();

            try
            {
            await  sqlConnection.OpenAsync();
            string queryString = "SELECT * FROM UserModel RIGHT JOIN UserPreferenceModel ON UserModel.ID = UserPreferenceModel.ID AND UserModel.UserType = UserPreferenceModel.UserType WHERE UserPreferenceModel.UserType = 2";
            SqlCommand command = new SqlCommand(queryString, sqlConnection); 
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                myTableLists.Add(new MyTableList
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name= reader["Name"].ToString(),
                    Height = reader["Height"].ToString(),
                    Weight = reader["Weight"].ToString(),
                    EyeColor = reader["EyeColor"].ToString(),
                    HairColor = reader["HairColor"].ToString(),
                    SkinColor = reader["SkinColor"].ToString(), 
                    Nationality = reader["Nationality"].ToString(),
                    Ethnicity = reader["Ethnicity"].ToString(),

                });
                SourceList.ItemsSource = myTableLists;
            }
        }
            catch (Exception ex)
            {   
                // Handle the exception or log it
                Console.WriteLine($"Exception: {ex.Message}");
            }
            finally
{
                // Close the connection in the finally block to ensure it's closed even if an exception occurs
                sqlConnection.Close();
            }
        }
     /*  private async void OnSourceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            MyTableList selectedSource = e.SelectedItem as MyTableList;
            await DisplayAlert("Clinic Selected", $"Clinic Name: {selectedSource.clinicName}\nLocation: {selectedClinic.Location}", "OK");
            await Navigation.PushAsync(new ClinicUserDetailsPage(selectedSource));


            // Deselect the item
            clinicListView.SelectedItem = null;
        }*/

        private async void ButtonBottom1_Clicked(object sender, EventArgs e)
        {
            // Navigate to ContentPage1 when the first button is clicked
            await Navigation.PushAsync(new DoneeBankPage());
        }

        private async void ButtonBottom2_Clicked(object sender, EventArgs e)
        {
            // Navigate to ContentPage2 when the second button is clicked
            await Navigation.PushAsync(new DoneeHomePage());
        }
        private async void ButtonBottom3_Clicked(object sender, EventArgs e)
        {
            // Navigate to ContentPage2 when the second button is clicked
            await Navigation.PushAsync(new DoneeHomePage());
        }

        private async void TopButton1_Clicked(object sender, EventArgs e)
        {
            // Navigate to ContentPage2 when the second button is clicked
            await Navigation.PushAsync(new DoneeHomePage());
        }
        private async void TopButton2_Clicked(object sender, EventArgs e)
        {
            // Navigate to ContentPage2 when the second button is clicked
            await Navigation.PushAsync(new DoneeHomePage());
        }
        private async void TopButton3_Clicked(object sender, EventArgs e)
        {
            // Navigate to ContentPage2 when the second button is clicked
            await Navigation.PushAsync(new DoneeHomePage());
        }
       
    }
}