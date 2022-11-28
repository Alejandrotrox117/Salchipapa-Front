using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Front.Utilities
{
    public class Validations
    {
        public static void TextBox_ValidateNum(object sender, KeyEventArgs e)
        {

            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key==Key.OemComma || e.Key==Key.OemPeriod)
                    e.Handled = false;
                else
                    e.Handled = true;
            
        }


    }
}
