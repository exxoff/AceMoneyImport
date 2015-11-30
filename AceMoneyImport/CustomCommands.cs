using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AceMoneyImport.Commands
{
    public static class CustomCommands
    {

        public static RoutedUICommand CreateCsv = new RoutedUICommand("CreateCsv", "CreateCsv", typeof(CustomCommands));
    }
}
