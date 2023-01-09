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

    public partial class GoodAddingPage : ContentPage
    {
        public string ItemId 
        {
            set
            {
                LoadGood(value);
            }
        }

        public GoodAddingPage()
        {
            InitializeComponent();

            BindingContext = new Good();
        }
        private async void LoadGood(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);

                Good good = await App.DB.GetGoodAsync(id);

                BindingContext = good;

            }
            catch (Exception ex)
            {
            }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Good good = (Good)BindingContext;

            if (!string.IsNullOrWhiteSpace(good.Name))
            {
                await App.DB.SaveGoodAsync(good);
            }

            await Shell.Current.GoToAsync("..");
        }
        private async void OnDeleteButton_clicked(object sender, EventArgs e)
        {
            Good good = (Good)BindingContext;

            await App.DB.DeleteGoodAsync(good);
            await Shell.Current.GoToAsync("..");
        }
    }



}