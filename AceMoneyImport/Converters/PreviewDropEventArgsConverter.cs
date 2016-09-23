using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AceMoneyImport.Converters
{
    class PreviewDropEventArgsConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            DragEventArgs args = value as DragEventArgs;
            return args;
        }
    }
}
