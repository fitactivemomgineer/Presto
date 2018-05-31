using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PrestoInventoryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeoLocation : Page
    {
        public GeoLocation()
        {
            this.InitializeComponent();
        }
        private async void OneShotLocation_Click(object sender, RoutedEventArgs e)
        {

            //using System.Threading.Tasks;
            //using Windows.Devices.Geolocation;
            //https://msdn.microsoft.com/en-us/library/windows/apps/jj206956(v=vs.105).aspx

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                LatitudeTextBlock.Text = geoposition.Coordinate.Latitude.ToString("0.00");
                LongitudeTextBlock.Text = geoposition.Coordinate.Longitude.ToString("0.00");
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    StatusTextBlock.Text = "location  is disabled in phone settings.";
                }
                //else
                {
                    // something else happened acquring the location
                }
            }
        }

    }
}