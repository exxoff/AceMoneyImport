using AceMoneyImport.Models;
using AceMoneyImport.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AceMoneyImport
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ComposeObjects();
            Application.Current.MainWindow.Show();

        }

        private void ComposeObjects()
        {
            ImportItem item = new ImportItem();
            MainViewModel viewModel = new MainViewModel(new ImportItem());
            MainWindow = new MainWindow(viewModel);
        }
    }
}
