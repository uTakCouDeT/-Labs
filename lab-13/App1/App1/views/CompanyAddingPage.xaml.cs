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
    public partial class CompanyAddingPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadGood(value);
            }
        }

        public CompanyAddingPage()
        {
            InitializeComponent();

            BindingContext = new Company();
        }
        private async void LoadGood(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);
                Company company = await App.DB.GetCompanyAsync(id);
                BindingContext = company;

            }
            catch (Exception ex)
            {
            }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Company company = (Company)BindingContext;

            if (!string.IsNullOrWhiteSpace(company.Name) && !string.IsNullOrWhiteSpace(company.Country))
            {
                await App.DB.SaveCompanyAsync(company);
            }
            await Shell.Current.GoToAsync("..");
        }
        private async void OnDeleteButton_clicked(object sender, EventArgs e)
        {
            Company company = (Company)BindingContext;

            await App.DB.DeleteCompanyAsync(company);
            await Shell.Current.GoToAsync("..");
        }
    }
}