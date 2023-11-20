using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CHOICEAPP.MODELS;

namespace CHOICEAPP.SOURCE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SourcePreference : ContentPage
    {
        Dictionary<string, List<string>> nationalityEthnicityMap = new Dictionary<string, List<string>>
        {
            {"Mexican", new List<string> { "Mestizo", "Indigenous", "European", "African", "Others" }},
            {"American", new List<string> { "English", "Irish", "African American", "Asian American", "Others" }},
            {"Filipino", new List<string> { "Tagalog", "Cebuano", "Ilocano", "Bicolano", "Others" }},
            {"Chinese", new List<string> { "Han", "Mandarin", "Cantonese", "Hakka", "Others" }},
            {"Indian", new List<string> { "Hindi", "Bengali", "Telugu", "Marathi", "Others" }},
            {"Japanese", new List<string> { "Yamato", "Ainu", "Ryukyuan", "Others" }},
            {"Korean", new List<string> { "Koreans", "Yanbian Koreans", "Zainichi Koreans", "Others" }},
            {"Brazilian", new List<string> { "White", "Mixed-race", "Black", "Indigenous", "Others" }},
            {"Canadian", new List<string> { "English", "French", "Scottish", "Irish", "Others" }}
            // Add more entries as needed
        };
        public SourcePreference()
        {
            InitializeComponent();
            List<string> nationalityOptions = new List<string>(nationalityEthnicityMap.Keys);
            nationalityPicker.ItemsSource = nationalityOptions;
        }
        private void OnNationalitySelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedNationality = nationalityPicker.SelectedItem.ToString();

            if (nationalityEthnicityMap.TryGetValue(selectedNationality, out List<string> ethnicities))
            {
                ethnicityPicker.ItemsSource = ethnicities;
                ethnicityPicker.IsEnabled = true;
            }
            else
            {
                ethnicityPicker.ItemsSource = null;
                ethnicityPicker.IsEnabled = false;
            }
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            // Create a UserPreference object with selected data
            UserPreferenceModel userPreference = new UserPreferenceModel
            {
                Height = heightPicker.SelectedItem?.ToString(),
                Weight = weightPicker.SelectedItem?.ToString(),
                EyeColor = eyeColorPicker.SelectedItem?.ToString(),
                HairColor = hairColorPicker.SelectedItem?.ToString(),
                SkinColor = skinColorPicker.SelectedItem?.ToString(),
                Nationality = nationalityPicker.SelectedItem?.ToString(),
                Ethnicity = ethnicityPicker.SelectedItem?.ToString()
            };

            // Save the data to the database
            SaveUserPreferenceToDatabase(userPreference);
        }

        private void SaveUserPreferenceToDatabase(UserPreferenceModel userPreference)
        {
            string connectionString = "Your_Connection_String";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // TODO: Write SQL query to insert data into the UserPreference table
                    string query = "INSERT INTO UserPreference (Height, Weight, EyeColor, HairColor, SkinColor, Nationality, Ethnicity) " +
                                   "VALUES (@Height, @Weight, @EyeColor, @HairColor, @SkinColor, @Nationality, @Ethnicity)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Height", userPreference.Height);
                        command.Parameters.AddWithValue("@Weight", userPreference.Weight);
                        command.Parameters.AddWithValue("@EyeColor", userPreference.EyeColor);
                        command.Parameters.AddWithValue("@HairColor", userPreference.HairColor);
                        command.Parameters.AddWithValue("@SkinColor", userPreference.SkinColor);
                        command.Parameters.AddWithValue("@Nationality", userPreference.Nationality);
                        command.Parameters.AddWithValue("@Ethnicity", userPreference.Ethnicity);

                        command.ExecuteNonQuery();
                    }
                }

                DisplayAlert("Success", "User preferences saved to the database.", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to save user preferences: {ex.Message}", "OK");
            }
        }
    }
    
}