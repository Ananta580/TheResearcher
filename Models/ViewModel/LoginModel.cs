using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheResearcher.Models.ViewModel
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool rememberme { get; set; }
    }
}