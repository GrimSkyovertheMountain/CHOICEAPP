using System;
using System.Collections.Generic;
using System.Text;

namespace CHOICEAPP.MODELS
{
    public class ClinicModel
    {
        public string clinicName { get; set; }
        public string Location { get; set; }
        
       private string clinicMedRecord;

        public string GetClinicMedRecord()
        {
            return clinicMedRecord;
        }

        public void SetClinicMedRecord(string value)
        {
            clinicMedRecord = value;
        }

        public string clinicLogo { get; set; }
        public string clinicIntroduction { get; set; }
        public string clinicStatus { get; set; }
        public int ID { get; set; }


    }
}
