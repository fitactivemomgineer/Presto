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
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System.Threading.Tasks;

using Windows.Devices.Geolocation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PrestoInventoryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
        public class InventoryTable
        {
            public string ID { get; set; }
            public string Text { get; set; }
            public string TITLE { get; set; }
            public string AUTHOR { get; set; }
            public string PRICE { get; set; }
            public Boolean _deleted { get; set; }
        }

        //private MobileServiceCollection<InventoryTable, InventoryTable> items;
        //private IMobileServiceTable<InventoryTable> todoTable = App.website20180420120114.GetTable<InventoryTable>();

        private async void InsertRecord(string strInput)
        {
            try
            {

                InventoryTable item = new InventoryTable { Text = strInput, TITLE = "Awesome item", AUTHOR = "John Doe", PRICE = "$50.00" };
                // await App.website20180420120114.GetTable<InventoryTable>().InsertAsync(item);


                //using Windows.UI.Popups;
                MessageDialog messageDialog = new MessageDialog("Completed Successfully!", "Windows 8");
                await messageDialog.ShowAsync();

            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog("An Error Occurred: " + e.Message, "Windows 8");
                await messageDialog.ShowAsync();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            InsertRecord(txtBoxInput.Text);
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            ButtonRefresh.IsEnabled = false;

            //await SyncAsync(); // offline sync
            await RefreshInventoryTables();

            ButtonRefresh.IsEnabled = true;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var InventoryTable = new InventoryTable { Text = txtBoxInput.Text };
            InsertRecord(txtBoxInput.Text);
            await InsertInventoryTable(InventoryTable);
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            InventoryTable item2 = cb.DataContext as InventoryTable;
            await UpdateCheckedInventoryTable(item2);
        }

        private async Task InsertInventoryTable(InventoryTable InventoryTable)
        {
            // This code inserts a new InventoryTable into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            //await InventoryTable.InsertAsync(InventoryTable);
            //items.Add(InventoryTable);

            //await SyncAsync(); // offline sync
        }

        private async Task RefreshInventoryTables()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the InventoryTables table.
                // The query excludes completed InventoryTables
                //items = await InventoryTable
                //.Where(InventoryTable => InventoryTable._deleted == false)
                // .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                //ListItems.ItemsSource = items;
                //this.ButtonSave.IsEnabled = true;
            }
        }

        private async Task UpdateCheckedInventoryTable(InventoryTable item)
        {
            try
            {
                // This code takes a freshly completed InventoryTable and updates the database. When the MobileService 
                // responds, the item is removed from the list 
                //await InventoryTable.UpdateAsync(item);
                //items.Remove(item);

                //The following code illustrates how to delete an existing instance. The instance is identified by the "Id" field set on the InventoryTable.
                // await InventoryTable.DeleteAsync(item);

                ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

                //await SyncAsync(); // offline sync

            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog("An Error Occurred: " + e.Message, "Windows 8");
                await messageDialog.ShowAsync();
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //await InitLocalStoreAsync(); // offline sync
            await RefreshInventoryTables();
        }
        private async void OneShotLocation_Click(object sender, RoutedEventArgs e)

        {



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

                StatusTextBlock.Text = "Found GPS Location.";

            }

            catch (Exception ex)

            {

                if ((uint)ex.HResult == 0x80004004)

                {

                    // the application does not have the right capability or the location master switch is off

                    StatusTextBlock.Text = "GPS location is disabled in phone settings.";

                }

                //else

                {

                    // something else happened acquring the location

                    StatusTextBlock.Text = "Error: " + ex.Message;

                }

            }

        }
    }
}

