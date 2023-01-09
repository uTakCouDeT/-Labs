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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class RecipientAddingPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadGood(value);
            }
        }

        public RecipientAddingPage()
        {
            InitializeComponent();

            BindingContext = new Recipient();
        }
        private async void LoadGood(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);

                Recipient rec = await App.DB.GetRecipientAsync(id);

                BindingContext = rec;

            }
            catch (Exception ex)
            {
            }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Recipient rec = (Recipient)BindingContext;

            if (!string.IsNullOrWhiteSpace(rec.FullName) && !string.IsNullOrWhiteSpace(rec.Country) && !string.IsNullOrWhiteSpace(rec.Address))
            {
                await App.DB.SaveRecipientAsync(rec);
            }

            await Shell.Current.GoToAsync("..");
        }
        private async void OnDeleteButton_clicked(object sender, EventArgs e)
        {
            Recipient rec = (Recipient)BindingContext;

            await App.DB.DeleteRecipientAsync(rec);
            await Shell.Current.GoToAsync("..");
        }
    }
}