using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Helpers
{
    public class MaxLengthTrim : ValidationAttribute
    {

        private readonly int length;

        public MaxLengthTrim(int _length)

        : base("The field {0} must be a string or array type with a maximum length of '" + _length.ToString() + "'.")

        {

            length = _length;

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {

            if (value != null)

            {



                int mlen = value.ToString().Trim().Length;

                if (mlen > length)

                {

                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);

                    return new ValidationResult(errorMessage);

                }



            }

            return ValidationResult.Success;

        }
    }

    public class IntNull : ValidationAttribute
    {
        public IntNull()

        : base("The field {0} int value must be null or '' or 0 ")

        {



        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {

            if (value == null)

            {





                //  var errorMessage = FormatErrorMessage(validationContext.DisplayName);

                // return new ValidationResult(errorMessage);
                return ValidationResult.Success;




            }

            return ValidationResult.Success;

        }
    }
}
