using DealHub_Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.ServiceModel.Channels;

namespace DealHubAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public ReponseMessage result = new ReponseMessage();

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetRequestURL()
        {
            string baseUrl = Request.RequestUri.AbsoluteUri;//.GetLeftPart(UriPartial.Authority)+ Request.AbsolutePath;
            return baseUrl;
        }
        //Test for Git

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUserIp()
        {
            return GetClientIp();
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            try
            {


                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)this.Request.Properties[RemoteEndpointMessageProperty.Name];
                    return prop.Address;
                }
                else if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {

                return "";
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool ValidateFileType(string FilePath,string source)//HttpPostedFileBase file ,
        {
            bool IsValid = false;
            List<FileHeaderType> listFileHeaderType = new List<FileHeaderType>();
            if (source == "obf")
            {
                listFileHeaderType.Add(new FileHeaderType { HexaSignature = "50-4B-03-04", Filetype = ".XLSX,.DOCX,.PPTX" });
                listFileHeaderType.Add(new FileHeaderType { HexaSignature = "D0-CF-11-E0", Filetype = ".XLS,.DOC,PPT (97-2003),Excel CSV" });
            }
            else { 
            listFileHeaderType.Add(new FileHeaderType { HexaSignature = "4C-6F-63-61", Filetype= ".CSV" });
            listFileHeaderType.Add(new FileHeaderType { HexaSignature = "50-4B-03-04", Filetype = ".XLSX,.DOCX,.PPTX" });
            listFileHeaderType.Add(new FileHeaderType { HexaSignature = "D0-CF-11-E0", Filetype = ".XLS,.DOC,PPT (97-2003),Excel CSV,.msg" });
            listFileHeaderType.Add(new FileHeaderType { HexaSignature = "89-50-4E-47", Filetype = ".PNG" });
            listFileHeaderType.Add(new FileHeaderType { HexaSignature = "FF-D8-FF-E0", Filetype = ".JPEG,.JPG" });
          //  listFileHeaderType.Add(new FileHeaderType { HexaSignature = "D0 CF 11 E0 A1 B1 1A E1", Filetype = ".msg" });
            }




            //  if (file != null && file.ContentLength > 0)
            // {

            /*
            string str = System.IO.Path.GetFileName(file.FileName);
            string FilePath = System.IO.Path.Combine((HttpContext.Current.Server.MapPath("~/Images")), System.IO.Path.GetFileName(file.FileName));
            file.SaveAs(path);
            */

            System.IO.BinaryReader reader = new System.IO.BinaryReader(new System.IO.FileStream(Convert.ToString(FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None));

                reader.BaseStream.Position = 0x0; // The offset you are reading the data from    
                byte[] data = reader.ReadBytes(0x10); // Read 16 bytes into an array    
                string data_as_hex = BitConverter.ToString(data);
                reader.Close();

                // substring to select first 11 characters from hexadecimal array    
                string my = data_as_hex.Substring(0, 11);
                string output = null;
                 FileHeaderType outfiletype = listFileHeaderType.Where(f => f.HexaSignature == my).FirstOrDefault();
            if (outfiletype != null)
            {
                IsValid = true;

            }

            return IsValid;
             
        }

    }

    public class FileHeaderType
    {
        public string HexaSignature { get; set; }
        public string Filetype { get; set; }

    }
}
