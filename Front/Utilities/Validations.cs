using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Front.Utilities
{
    public class Validations : ValidationRule
    {
        

        private bool ValidateFormat(string value)
        {
            Regex rgx = new Regex(@"/^[a-zA-Z ]+$/");
             Match match = rgx.Match(value);
            return match.Success;
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string valor = value as string;
            //if (string.IsNullOrEmpty(valor))                
            //    return new ValidationResult(false, "El campo no puede estar vacio");
            if (ValidateFormat(valor))
                return new ValidationResult(false, "El campo no puede contener caracteres especiales");
            else
                return ValidationResult.ValidResult;
            
            
        
        }
    }
}
