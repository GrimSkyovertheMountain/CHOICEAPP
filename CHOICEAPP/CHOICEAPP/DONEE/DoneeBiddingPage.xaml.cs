using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHOICEAPP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DoneeBiddingPage : ContentPage
	{
		public DoneeBiddingPage ()
		{
            InitializeComponent();
		}
        private async void TopButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to ContentPage2 when the second button is clicked
            await Navigation.PushAsync(new DoneeHomePage());
        }
    }
}