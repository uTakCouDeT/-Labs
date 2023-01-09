using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipientPage : ContentPage
    {
        public RecipientPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collection.ItemsSource = await App.DB.GetRecipientsAsync();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RecipientAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Recipient rec = (Recipient)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(RecipientAddingPage)}?{nameof(RecipientAddingPage.ItemId)}={rec.Id.ToString()}");
            }
        }
    }
}