using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Data;
using System.IO;

namespace App1
{
    public partial class App : Application
    {
        static IndustrialDB db;

        public static IndustrialDB DB
        {
            get
            {
                if (db == null)
                {
                    db = new IndustrialDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Industrial.db3"));
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
