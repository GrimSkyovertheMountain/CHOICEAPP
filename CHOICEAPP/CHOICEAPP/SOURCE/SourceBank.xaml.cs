using CHOICEAPP.MODELS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHOICEAPP.SOURCE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SourceBank : ContentPage
    {
        public ObservableCollection<ClinicModel> Clinics { get; set; }
        public SourceBank()
        {
            InitializeComponent();
            Clinics = new ObservableCollection<ClinicModel>();
            clinicListView.ItemsSource = Clinics;
            // Load clinics from the database
            LoadClinicsFromDatabase();
        }
        private void LoadClinicsFromDatabase()
        {
            // TODO: Update the connection string with your database details
            string connectionString = "Data Source=MSI\\CHOICESERVER;AttachDbFilename=\"C:\\Program Files\\Microsoft SQL Server\\MSSQL16.CHOICESERVER\\MSSQL\\DATA\\Caps.mdf\";Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // TODO: Write SQL query to select clinic data from the Clinic table
                    string query = "SELECT ClinicName, Location FROM ClinicModel";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClinicModel clinic = new ClinicModel
                                {
                                    clinicName = reader["ClinicName"].ToString(),
                                    clinicLogo = reader["ClinicLogo"].ToString(),
                                    clinicIntroduction = reader["ClinicDetails"].ToString(),
                                    Location = reader["ClinicLocation"].ToString(),
                                    clinicStatus = reader["Status"].ToString()
                                    // Add other properties as needed
                                };

                                Clinics.Add(clinic);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to load clinics: {ex.Message}", "OK");
            }
        }
        private async void OnClinicSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ClinicModel selectedClinic = e.SelectedItem as ClinicModel;
            await DisplayAlert("Clinic Selected", $"Clinic Name: {selectedClinic.clinicName}\nLocation: {selectedClinic.Location}", "OK");
            await Navigation.PushAsync(new ClinicUserDetailsPage(selectedClinic));


            // Deselect the item
            clinicListView.SelectedItem = null;
        }
    }
}