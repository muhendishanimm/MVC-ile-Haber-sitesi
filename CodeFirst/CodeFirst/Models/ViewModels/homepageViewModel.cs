using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirst.Models.ViewModels
{
    public class homepageViewModel
    {
        public List<Kisi> Kisiler { get; set; }
        public List<Adres> Adresler { get; set; }
    }
}