using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using DRiUv4;

namespace DRiUv4
{
    public class MyLists
    {

        //readonly string listKto;
        public static string SelectedListNazwisko
        {
            get { return ListNazwisko[0].ToString(); }
        }

        //readonly string listForma;
        public static string SelectedListForma
        {
            get { return ListForma[0].ToString(); }
        }
        public static string SelectedListKto
        {
            get { return ListKto[0].ToString(); }
        }

        public static IList<string> ListNazwisko
        {
            get
            {
                return new List<string> { "Ireneusz Wręczycki", "Jacek Rutkowski", "Krzysztof Krause" };
            }
        }
        public static IList<string> ListForma
        {
            get
            {
                return new List<string> { "Telefon", "Mail", "Jira", "Wizyta/Spotkanie" };
            }
        }
        
        public static IList<string> ListKto
        {
            get
            {
                return new List<string> { "DRiU", 
                                        "Klient",
                                        "COK", 
                                        "DWS",
                                        "Logistyka",
                                        "KOS",
                                        "PH/DK",
                                        "Fresh",
                                        "Biznes",
                                        "PH Obcy",
                                        "Inne"     };
            }
        }

        public static IList<string> ListBU
        {
            get
            {
                return new List<string> { "ECT (Dystrybucja)",
                                        "ECK (Serwis)",
                                        "ECM (Gastronomia)",
                                        "ONTRADE",
                                        "Franczyzy",
                                        "CC",
                                        "testowy BU" };
            }
        }

    }
}
