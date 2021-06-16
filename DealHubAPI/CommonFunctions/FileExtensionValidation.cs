using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DealHubAPI.CommonFunctions
{
    public class FileExtensionValidation : IFileExtensionValidation
    {
        public bool ValidateUploadedExtension(string extension, char seperator)
        {
            string[] allowedExtension = ConfigurationManager.AppSettings["uploadallowedextensions"].ToString().Split(seperator);

            for(int i=0;i< allowedExtension.Length;i++)
            {
                if (allowedExtension[i]==extension)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ValidateMagicNumber(string filepath)
        {
            return false;
        }
    }

    

    public interface IFileExtensionValidation
    {
         bool ValidateUploadedExtension(string extension,char seperator);
        bool ValidateMagicNumber(string filepath);
    }
}