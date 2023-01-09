using App1.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(GoodAddingPage), typeof(GoodAddingPage));
            Routing.RegisterRoute(nameof(CompanyAddingPage), typeof(CompanyAddingPage));
            Routing.RegisterRoute(nameof(RecipientAddingPage), typeof(RecipientAddingPage));
        }
    }
}