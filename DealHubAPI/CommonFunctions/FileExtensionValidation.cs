﻿using System;
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

        public bool ValidateObfUploadedExtension(string extension, char seperator)
        {
            string[] allowedExtension = { ".xlsx", ".xls" };

            for (int i = 0; i < allowedExtension.Length; i++)
            {
                if (allowedExtension[i] == extension)
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

    public class CommonKeyClass
    {
        public static string Key = "0c24f9de!b855915";

        //public static string getkeywithrandomnumber()
        //{
        //    Random rnd = new Random();
        //    int randomnum = rnd.Next(110000, 999999);
        //    string Keynew = "0c24f9de!b";
        //    Keynew = Keynew + randomnum;
        //    return Keynew;
        //}
    }

    public interface IFileExtensionValidation
    {
         bool ValidateUploadedExtension(string extension,char seperator);

        bool ValidateObfUploadedExtension(string extension, char seperator);
        bool ValidateMagicNumber(string filepath);
    }
}