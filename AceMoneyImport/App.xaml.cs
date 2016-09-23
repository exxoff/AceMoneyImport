using AceMoneyImport.Interfaces;
using AceMoneyImport.Models;
using AceMoneyImport.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IImportItem, ImportItem>();
                       
            MainViewModel viewModel = new MainViewModel(ServiceLocator.Current.GetInstance<IImportItem>());
            MainWindow = new MainWindow(viewModel);
        }
    }
}
