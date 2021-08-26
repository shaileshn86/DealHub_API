using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealHubAPI.Models;
using DealHub_Domain.Authentication;
using DealHub_Service.Implemantations.APIServices;
using DealHub_Domain.Helpers;
using DealHubAPI.Utility;
using DealHub_Domain.Enum;
using DealHub_Domain.Authentication;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Threading.Tasks;
using DealHub_Domain.MenuBinding;
using System.Configuration;
using System.Web;
using System.IO;
using DealHub_Domain.DashBoard;
using DealHub_Dal.OBF;
using DealHubAPI.CommonFunctions;
using DealHub_Service.Implemantations.ErrorLog;
using OfficeOpenXml;
using System.Data;
using DealHub_Domain.Migration;

namespace DealHubAPI.Controllers
{
    [RoutePrefix("Api/Migration")]
    public class MigrationUploadController : BaseApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("UploadMigrationFile")]
        public HttpResponseMessage UploadMigrationFile()
        {
            string imageName = null;
            HttpResponseMessage msg = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            string DocsPathMain = ConfigurationManager.AppSettings["APIURL"];//"http://localhost:52229";
            string docpath = "";
            var postedFilenew = httpRequest.Files;
            string filepathdetails = "";
            try
            {
                foreach (string fileName in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[fileName];
                    // var postedFile = httpRequest.Files["Image"];
                    //Create custom filename
                    if (postedFile != null)
                    {
                        IFileExtensionValidation _ValidateExtension = new FileExtensionValidation();
                        if (!_ValidateExtension.ValidateObfUploadedExtension(Path.GetExtension(postedFile.FileName), ','))
                        {
                            //throw new Exception("File Extension not allowed for upload");
                            msg = Request.CreateResponse(HttpStatusCode.NotAcceptable, "File not uploaded : File format not supported");
                            return msg;
                        }

                        //imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                        imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-");
                        imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                        string urlpath = string.Format("/DealHubFiles/{0}/{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.Hour.ToString().PadLeft(2, '0')); // "~/Images/" + DateTime.Now.ToString("yymmssfff") + "/" + cDateTime.Now.Hour.ToString().PadLeft(2, '0');
                        docpath = DocsPathMain + urlpath + "/" + imageName; ;
                        string folderpath = string.Format("~/DealHubFiles/{0}/{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.Hour.ToString().PadLeft(2, '0')); // "~/Images/" + DateTime.Now.ToString("yymmssfff") + "/" + cDateTime.Now.Hour.ToString().PadLeft(2, '0');



                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(folderpath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderpath));
                        }

                        var filePath = HttpContext.Current.Server.MapPath(folderpath + "/" + imageName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);

                        }
                        else
                        {
                            postedFile.SaveAs(filePath);
                            bool IsValidFile = ValidateFileType(filePath, "obf");
                            // IsValidFile == false then delete save file
                            if (!IsValidFile)
                            {
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "File not uploaded : " + imageName + ", because file format is not proper");
                                }
                            }
                        }
                        filepathdetails += docpath.ToString() + ",";


                        msg = Request.CreateResponse(HttpStatusCode.OK, filePath);
                    }
                    else
                    {
                        msg = Request.CreateResponse(HttpStatusCode.BadRequest, "File not uploaded : " + imageName);
                    }
                }

            }
            catch (Exception ex)
            {
                msg = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            return msg;

        }

        public static string getbatchno()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1000, 9999);
            int randomNumber2 = rand.Next(1000, 9999);
            //string key = "$!$030!m0l0l" + randomNumber.ToString()+ randomNumber2.ToString();
            string key = Guid.NewGuid().ToString().Replace("-", "!").Substring(0, 12) + randomNumber.ToString() + randomNumber2.ToString();
            return key;
        }

        public static DataSet ReadExcel(string filepath)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                string batchno = getbatchno();
                TruncateMigrationDataParameters model = new TruncateMigrationDataParameters();
                model._batch_no = batchno;
                model._user_code = "25006085";
                model._FileName = filepath;
                model._TotalRecords = 0;

                DataSet Ds = MigrationService.truncatemigrationdata(model);
                FileInfo existingFile = new FileInfo(filepath);
             
                

                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                   
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Existing OBF & PPL"];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    int startrow=3;
                    for (int row = startrow; row <= rowCount; row++)
                    {
                        DataRow Dr = Ds.Tables[0].NewRow();

                        for (int col = 0; col < Ds.Tables[0].Columns.Count-1; col++)
                        {
                            if (worksheet.Cells[row, col + 1].Value!=null)
                            {
                                Dr[col] = worksheet.Cells[row, col + 1].Value.ToString();
                            }
                            
                        }
                        Dr["Stage_HeaderId"] = Ds.Tables[1].Rows[0]["HeaderId"].ToString();
                        Ds.Tables[0].Rows.Add(Dr);
                    }
                }
                return Ds;
            }
            catch(Exception ex)
            {
                ErrorService.writeloginfile(ex.ToString());
                return null;
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("TestUploadObfFile")]
        public HttpResponseMessage TestUploadObfFile()
        {
            HttpResponseMessage msg = new HttpResponseMessage();
            try
            {
              DataSet Rds=  ReadExcel(@"E:\Shailesh\DbRelated\Migration\Shabbarfile.xlsx");
              if (Rds !=null)
                {
                    string batchno = Rds.Tables[1].Rows[0]["batchno"].ToString();
                    Rds.Tables[0].Columns.Add("Remark");
                    if (MigrationService.UpdateMigrationData(Rds) == "Data Upload Sucess")
                    {
                        MigrationParameters model = new MigrationParameters();
                        model._batch_no = batchno;
                        //model._user_code = "25006085"; //user of scm
                        model._user_code = "23145259"; //user of EM;

                        List<commanmessges> validate = new List<commanmessges>();

                        validate = MigrationService.ValidateMigratedData(model);

                        if (validate[0].status!= "success")
                        {
                            msg = Request.CreateResponse(HttpStatusCode.BadRequest, validate[0].status);
                        }
                        else
                        {
                            List<commanmessges> insertion = new List<commanmessges>();

                            insertion = MigrationService.insertmigratedData(model);
                            if (insertion[0].status != "success")
                            {
                                msg = Request.CreateResponse(HttpStatusCode.BadRequest, insertion[0].status);
                            }
                            else
                            {
                                msg = Request.CreateResponse(HttpStatusCode.OK, validate[0].status);
                            }
                        }


                    }
                    else
                    {
                        msg = Request.CreateResponse(HttpStatusCode.BadRequest, "Failure");
                    }
                }
                else
                {
                    msg = Request.CreateResponse(HttpStatusCode.BadRequest, "Failure");
                }

                
            }
            catch(Exception ex)
            {
                ErrorService.writeloginfile(ex.ToString());
                msg = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            return msg;
        }
    }
}
