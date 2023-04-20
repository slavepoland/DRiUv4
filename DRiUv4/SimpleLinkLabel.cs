using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DRiUv4
{
    public class SimpleLinkLabel : Label
    {
        public SimpleLinkLabel(string uri, string labelText = null)
        {
            Text = labelText ?? uri.ToString();
            GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => Launcher.TryOpenAsync(uri)) });
        }
    }
}
