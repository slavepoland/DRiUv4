using System.Collections.Generic;
using Xamarin.Forms;
using System;
using System.Linq;
using Xamarin.Essentials;

namespace DRiUv4
{
    public partial class MainPage : ContentPage
    {
        private readonly string version = "1.1";
        private readonly string url = "https://drive.google.com/file/d/1HYDR41Hjp6ZujC3ZS6nbOvQb992ff3Wo/view?usp=share_link";

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

        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                //reconfigure layout
            }
        }
        public MainPage()
        {
            InitializeComponent();

            if (Send.CheckVersion() != version)
            {
                Btn_Send.IsEnabled = false; 
                Btn_Send.Text = "Pobierz aktualną wersję!!!!";
                //LinkLabel linkLabel = new LinkLabel(new Uri(url));
                //ResultLabel.Focus();
                //ResultLabel.Text = linkLabel.ToString();
                Btn_Hyper.IsVisible = true; Btn_Send.IsVisible = false;
            }
            else
            {//uruchomienie apliakcji
                Btn_Send.IsVisible = false;
                Picker_Change(cb_Nazwisko, GoogleList.GetListsItem(1));//add item to combo
                Picker_Change(cb_FormaZgloszenia, GoogleList.GetListsItem(2));//add item to combo
                Picker_Change(cb_Kto, GoogleList.GetListsItem(3));//add item to combo
                Picker_Change(cb_BU, GoogleList.GetListsItem(4));//add item to combo
                Picker_Change(cb_Aplikacja, GoogleList.GetListsItem(5));//add item to combo
                Picker_Change(cb_NazwaZlecenia, GoogleList.GetListsItem(6)); //add item to combo      

                ////get last user
                LastUser("open");
            }
        }

        //metody
        private void SubmitButton_Pressed(object sender, EventArgs e)
        {
            //DateTime start = DateTime.Now;
            Btn_Send.IsEnabled = false;
            Send send = new Send(); //clasa do wysyłki danych
            send.SendData(cb_Nazwisko.SelectedItem.ToString() ?? "null",
                          cb_FormaZgloszenia.SelectedItem.ToString() ?? "null",
                          cb_Kto.SelectedItem.ToString() ?? "null",
                          cb_BU.SelectedItem.ToString() ?? "null",
                          cb_Aplikacja.SelectedItem.ToString() ?? "null",
                          cb_NazwaZlecenia.SelectedItem.ToString() ?? "null",
                          macadress: DeviceId.Id ?? "null"
                          ); //append data to file

            cb_NazwaZlecenia.SelectedIndex = 0;
            Btn_Send.IsEnabled = true;
            //DateTime stop = DateTime.Now;
            //ResultLabel.Text = (stop - start).Milliseconds.ToString();
        }

        private void Picker_Change(object sender, List<string> list)
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

        private void Cb_Aplikacja_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker cb = (Picker)sender;
            int i = DynamiclistApp.Where(k => k.Key == cb.SelectedIndex
                .ToString()).Select(k => k.Value).FirstOrDefault();
            Picker_Change(cb_NazwaZlecenia, GoogleList.GetListsItem(i));
        }

        private void CB_NazwaZlecenia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker cb = (Picker)sender;
            if (cb.Items.Count > 0)
            {
                if (cb.SelectedItem.ToString() != ".")
                    Btn_Send.IsVisible = true;
                else
                    Btn_Send.IsVisible = false;
            }
        }

        private void LastUser(string action)
        {
            bool WhoNotExist = false;
            DeviceId.GetDeviceId();

                if(action.Equals("open"))
                {
                    Dictionary<string, string> kto = Send.CheckWho();
                            if (kto.ContainsKey(DeviceId.Id))
                            {
                                cb_Nazwisko.SelectedItem = kto.Where(x => x.Key == DeviceId.Id)
                                .Select(x => x.Value).FirstOrDefault();
                                WhoNotExist = true;
                            }

                    if(WhoNotExist == false)
                        Send.SendWho(cb_Nazwisko.SelectedItem.ToString(), DeviceId.Id);
                }
        }//while open get last user, while closed write last user

        private void CB_Nazwisko_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                Picker cb = (Picker)sender;
                Send.SendWho(cb.SelectedItem.ToString(), DeviceId.Id);
            }
            catch { } 
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //ResultLabel.Focus();
            await Browser.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);
        }

        private async void Btn_Hyper_Pressed(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);
        }
    }
}

