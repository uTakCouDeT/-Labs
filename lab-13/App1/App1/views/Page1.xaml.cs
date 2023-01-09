using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Models;

namespace App1.views
{
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collection.ItemsSource =await App.DB.GetGoodsAsync();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(GoodAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Good good = (Good)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(GoodAddingPage)}?{nameof(GoodAddingPage.ItemId)}={good.Id.ToString()}");
            }
        }
    }
}