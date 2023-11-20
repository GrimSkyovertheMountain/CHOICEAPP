using System;
using System.Collections.Generic;
using System.Text;

namespace CHOICEAPP.MODELS
{
    public class UserModel
    {

        public int ID { get; set; }
        public int UserType {  get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
