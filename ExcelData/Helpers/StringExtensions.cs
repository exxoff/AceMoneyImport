using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExcelData
{
    public static class StringExtensions
    {

        public static string FormatText(this string InputText)
        {
            string _output = InputText.DoSubstitution();

            _output = _output.SetCase();

            return _output;

        }
        private static string SetCase(this string InputText)
        {

            TextInfo _textInfo = new CultureInfo(CultureInfo.CurrentCulture.ToString(), false).TextInfo;

            string _output = _textInfo.ToTitleCase(InputText.ToLower());

            return _output;
        }


        private static string DoSubstitution(this string InputText)
        {
            string _output = InputText;

            Dictionary<string, string> _SubstitutionRules = new Dictionary<string, string>();

            XmlDocument _doc = new XmlDocument();
            _doc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\CharacterReplacements.xml");



            foreach (XmlNode _node in _doc.DocumentElement.ChildNodes)
            {
                if (_node.NodeType == XmlNodeType.Element && _node.Name == "replace")
                {
                    _SubstitutionRules.Add(_node.Attributes["oldchar"].Value, _node.Attributes["newchar"].Value);
                }
            }



            foreach (var key in _SubstitutionRules)
            {
                _output = _output.Replace(key.Key, key.Value);
            }

            return _output;
        }


    }
}
