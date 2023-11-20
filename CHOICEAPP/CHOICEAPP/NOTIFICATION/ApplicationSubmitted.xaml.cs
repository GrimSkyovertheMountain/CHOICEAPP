using CHOICEAPP.SOURCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHOICEAPP.NOTIFICATION
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicationSubmitted : ContentPage
    {
        public ApplicationSubmitted()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SourceBank());
        }
    }
}