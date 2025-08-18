using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFHelpers
{
    public static class WPFHelper
    {
        public static bool ValidateInput(TextBox textbox, string format, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            string input = textbox.Text;
            if (string.IsNullOrWhiteSpace(input)) 
            {
                ErrorMessage = "Input Cannot be Empty";
                return false;
            }
            if (!string.IsNullOrEmpty(format) && !Regex.IsMatch(input, format))
            {
                ErrorMessage = "Must not be negative";
                return false;
            }
            return true;
        }
               
    }
}
