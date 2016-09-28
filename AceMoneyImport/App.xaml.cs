
using AceMoneyImport.ViewModels;
using ExcelData.Helpers;
using ExcelData.Interfaces;
using ExcelData.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.IO;
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
            SimpleIoc.Default.Register<ICsvFileWriter, CsvFileWriter>();
            
                       
            MainViewModel viewModel = new MainViewModel(ServiceLocator.Current.GetInstance<IImportItem>());
            MainWindow = new MainWindow(viewModel);
        }
    }
}
