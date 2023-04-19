using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;
using System;
using System.Linq;
using Xamarin.Forms.Internals;

namespace DRiUv4
{
    public partial class MainPage : ContentPage
    {
        private readonly string version = "1.0";
        private readonly static string _macAdress;

        public Dictionary<string, int> DynamiclistApp = new Dictionary<string, int>()
        {
            { "0", 6 },
            { "1", 7 },
            { "2", 8 },
            { "3", 9 },
            { "4", 10 },
            { "5", 11 },
            { "6", 12 },
            { "7", 13 },
            { "8", 14 },
            { "9", 15 },
            { "10", 16 },
            { "11", 17 }
        };


        public MainPage()
        {
            InitializeComponent();

            //_macAdress = MacAdress.GetMacAdress();

            if (Send.CheckVersion() != version)
            {
                btn_Send.IsEnabled = false; btn_Send.Text = "Pobierz aktualną wersję!!!!";
            }
            else
            {//uruchomienie apliakcji
                btn_Send.IsVisible = false;
                Picker_Change(cb_Nazwisko, GoogleList.GetListsItem(1));//add item to combo
                Picker_Change(cb_FormaZgloszenia, GoogleList.GetListsItem(2));//add item to combo
                Picker_Change(cb_Kto, GoogleList.GetListsItem(3));//add item to combo
                Picker_Change(cb_BU, GoogleList.GetListsItem(4));//add item to combo
                Picker_Change(cb_Aplikacja, GoogleList.GetListsItem(5));//add item to combo
                Picker_Change(cb_NazwaZlecenia, GoogleList.GetListsItem(6)); //add item to combo      

                ////get last user
                LastUser();
                cb_Aplikacja.SelectedIndex = 0;
            }
        }

        private void SubmitButton_Pressed(System.Object sender, System.EventArgs e)
        {
            DateTime start = DateTime.Now;
            btn_Send.IsEnabled = false;
            Send send = new Send(); //clasa do wysyłki danych

            send.SendData(cb_Nazwisko.SelectedItem.ToString() ?? "null",
                          cb_FormaZgloszenia.SelectedItem.ToString() ?? "null",
                          cb_Kto.SelectedItem.ToString() ?? "null",
                          cb_BU.SelectedItem.ToString() ?? "null",
                          cb_Aplikacja.SelectedItem.ToString() ?? "null",
                          cb_NazwaZlecenia.SelectedItem.ToString() ?? "null",
                          _macAdress ?? "null"
                          ); //append data to file

            cb_NazwaZlecenia.SelectedIndex = 0;
            btn_Send.IsEnabled = true;
            DateTime stop = DateTime.Now;
            ResultLabel.Text = (stop - start).Milliseconds.ToString();
        }

        //metody
        private void Picker_Change(System.Object sender, List<string> list)
        {
            Picker cb = (Picker)sender;
            if (cb.Items.Count > 0)
                cb.Items.Clear();

            if (list != null)
            {
                foreach (string item in list)
                {
                    cb.Items.Add(item);
                }
            }
            if (cb.Items.Count > 0)
                cb.SelectedIndex = 0;
        } //update picker list

        private void Cb_Aplikacja_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Picker cb = (Picker)sender;
            int i = DynamiclistApp.Where(k => k.Key == cb.SelectedIndex.ToString()).Select(k => k.Value).FirstOrDefault();
            Picker_Change(cb_NazwaZlecenia, GoogleList.GetListsItem(i));
        }

        private void CB_NazwaZlecenia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker cb = (Picker)sender;
            if (cb.Items.Count > 0)
            {
                if (cb.SelectedItem.ToString() != ".")
                    btn_Send.IsVisible = true;
                else
                    btn_Send.IsVisible = false;
            }
        }

        private void LastUser()
        {
            string kto;
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;//
            //Stream stream = assembly.GetManifestResourceStream("DRiUv2.who.txt");
            //StreamReader sr = new StreamReader(stream);
            try
            {
                kto = Send.CheckWho();
                foreach (var item in GoogleList.GetListsItem(1))
                {
                    if (item.Equals(kto))
                    {
                        cb_Nazwisko.SelectedItem = item;
                    }
                }
            }
            catch
            {
                //System.IO.File.Create(folder);
            }

        }//while open get last user, while closed write last user

        private void CB_Nazwisko_Unfocused(object sender, FocusEventArgs e)
        {
            Picker cb = (Picker)sender;
            Send.SendWho(cb.SelectedItem.ToString());
        }
    }
}

