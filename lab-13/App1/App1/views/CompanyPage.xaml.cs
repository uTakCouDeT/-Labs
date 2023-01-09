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
    public partial class CompanyPage : ContentPage
    {
        public CompanyPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collection.ItemsSource = await App.DB.GetCompaniesAsync();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CompanyAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Company company = (Company)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(CompanyAddingPage)}?{nameof(CompanyAddingPage.ItemId)}={company.Id.ToString()}");
            }
        }
    }
}