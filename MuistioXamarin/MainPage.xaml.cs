using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace MuistioXamarin
{
    public partial class MainPage : CarouselPage
    {
       
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder
            .LocalApplicationData), "muistio.txt");

        string text = "";

        public MainPage()
        {
            InitializeComponent();

            bool doesExist = File.Exists(fileName);

            if (doesExist == true)
            {
                text = File.ReadAllText(fileName);
                if (text.Length > 0)
                {
                    outputLabel.Text = text;
                }
                else
                {
                    outputLabel.Text = "Mitään ei ole talletettu muistioon.";
                }

            }
            else
            {
                outputLabel.Text = "Tervetuloa uudelle käyttäjälle!";
            }
        }


        private void tallennusNappi_Clicked(object sender, EventArgs e)
        {
            text = text + Environment.NewLine + inputKentta.Text;
            File.WriteAllText(fileName, text);
            //text = File.ReadAllText(fileName);
            outputLabel.Text = text;
            inputKentta.Text = "";
        }

        private void poistoNappi_Clicked(object sender, EventArgs e)
        {
            poistoNappi.IsVisible = false;
            vahvistusInfo.IsVisible = true;
            vahvistusKytkin.IsVisible = true;
        }

        private void vahvistusKytkin_Toggled(object sender, ToggledEventArgs e)
        {
            File.WriteAllText(fileName, "");
            text = "";
            outputLabel.Text = "Muistiinpanot tyhjennetty.";
            poistoNappi.IsVisible = true;
            vahvistusInfo.IsVisible = false;
            vahvistusKytkin.IsVisible = false;
        }

        async void valoNappi_Clicked(object sender, EventArgs e)
        {
            try
            {
                double level = Battery.ChargeLevel;

                if (level < 0.1)
                {
                    await DisplayAlert("Akku vähissä", "Akun varaus on alle 10%" + Environment.NewLine +
                        "En halua tyhjentää akkuasi kokonaan", "Ok");
                    return;
                }
                valoPoisNappi.IsVisible = true;
                valoNappi.IsVisible = false;
                //await Flashlight.TurnOnAsync();
            }
            catch(Exception ex)
            {
                await DisplayAlert("", ex.ToString(), "ok");
            }
        }

        async void valoPoisNappi_Clicked(object sender, EventArgs e)
        {
            valoNappi.IsVisible = true;
            valoPoisNappi.IsVisible = false;
           // await Flashlight.TurnOffAsync();
        }

    }
}
