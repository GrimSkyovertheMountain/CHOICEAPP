using CHOICEAPP.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHOICEAPP.SOURCE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClinicUserDetailsPage : ContentPage
    {
        private ClinicModel selectedClinic;

        public ClinicUserDetailsPage()
        {
            InitializeComponent();
        }

        public ClinicUserDetailsPage(ClinicModel selectedClinic)
        {
            this.selectedClinic = selectedClinic;
        }
    }
}