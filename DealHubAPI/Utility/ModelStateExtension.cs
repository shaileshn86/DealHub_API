using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace DealHubAPI.Utility
{
    public static class ModelStateExtension
    {
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            //if (!modelState.IsValid)
            //{
            //    return modelState.ToDictionary(kvp => kvp.Key,
            //        kvp => kvp.Value.Errors
            //                        .Select(e => e.ErrorMessage).ToArray())
            //                        .Where(m => m.Value.Any());
            //}

            return modelState.ToDictionary(
                        kvp => kvp.Key.Replace(kvp.Key.Substring(0, kvp.Key.IndexOf(".") + 1), ""),
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

        }

        public static IEnumerable<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            List<ValidationError> result = new List<ValidationError>();

            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors.Select(error => new ValidationError(fieldKey, error.ErrorMessage + (error.Exception != null ? error.Exception.Message : "")));

                result.AddRange(fieldErrors);
            }


            for (int n = 0; n < result.Count; n++)
                // result[n].Key= result[n].Key.Replace("model.Record.", "");
                result[n].Key = result[n].Key.Substring(result[n].Key.LastIndexOf(".") + 1);
            return result;


        }

        public static IEnumerable<ValidationErrorList> ErrorList(this ModelStateDictionary modelState)
        {

            List<ValidationError> result = new List<ValidationError>();
            List<ValidationErrorList> resultlist = new List<ValidationErrorList>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors.Select(error => new ValidationError(fieldKey, error.ErrorMessage));

                result.AddRange(fieldErrors);
            }

            var lst = (from err in result select new { key = err.Key.Substring(0, err.Key.LastIndexOf(".")) });
            foreach (var item in lst.Distinct())
            {
                ValidationErrorList _validationErrorList = new ValidationErrorList();
                List<ValidationError> lst1 = (from err in result where err.Key.Contains(item.key) select err).ToList();

                _validationErrorList.Record = lst1;
                resultlist.Add(_validationErrorList);
            }
            for (int n = 0; n < result.Count; n++)
                result[n].Key = result[n].Key.Substring(result[n].Key.LastIndexOf(".") + 1);

            //result[n].Key = result[n].Key.Replace("model.Record.", "");
            return resultlist;


        }


        public static string ReturnErrors(this ModelStateDictionary modelState)
        {
            List<ValidationError> result = new List<ValidationError>();

            StringBuilder errormessages = new StringBuilder();

            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors.Select(error => new ValidationError(fieldKey, error.ErrorMessage + (error.Exception != null ? error.Exception.Message : "")));

                

                result.AddRange(fieldErrors);
            }


            for (int n = 0; n < result.Count; n++)
            {
                // result[n].Key = result[n].Key.Substring(result[n].Key.LastIndexOf(".") + 1);
                if (errormessages.ToString() == "")
                {
                    errormessages.Append(result[n].Message);
                }
                else
                {
                    errormessages.Append("\n");
                    errormessages.Append(result[n].Message);
                }
            }
            //return result;

            return errormessages.ToString();


        }

    }

    public static class HttpStatusCodeExt
    {
        public static string ToCode(this HttpStatusCode StatusCode)
        {
            return ((int)StatusCode).ToString();
        }
    }

    public class ValidationError
    {
        public ValidationError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }

    public class ValidationErrorList
    {
        public List<ValidationError> Record { get; set; }
    }
}