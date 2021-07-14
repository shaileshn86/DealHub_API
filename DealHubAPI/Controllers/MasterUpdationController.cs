using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealHub_Domain.DashBoard;
using DealHub_Domain.Helpers;
using DealHub_Service.Implemantations.APIServices;
using DealHub_Domain.Enum;
using Newtonsoft.Json;
using DealHub_Service.Implemantations;
using DealHub_Domain.Masters;
using DealHubAPI.Utility;

namespace DealHubAPI.Controllers
{
    
    [RoutePrefix("Api/MasterUpdation")]
    public class MasterUpdationController : ApiController
    {
        public ReponseMessage result = new ReponseMessage();

        [HttpPost]
        [Route("GetMstDomains")]
        public HttpResponseMessage GetMstDomains(Mstcommonparameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                string json = MstDomainService.GetMstDomains(model);
                if (json == "" || json == "error")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }

        [HttpPost]
        [Route("Update_Mst_Domains")]
        public HttpResponseMessage Update_Mst_Domains(MstDomainParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = MstDomainService.Update_Mst_Domains(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }


        [HttpPost]
        [Route("GetMstFunctions")]
        public HttpResponseMessage GetMstFunctions(Mstcommonparameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                string json = MstFunctionService.GetMstFunctions(model);
                if (json == "" || json == "error")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }

        [HttpPost]
        [Route("UpdateMstFunctions")]
        public HttpResponseMessage Update_Mst_Domains(MstFunctionParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = MstFunctionService.UpdateMstFunctions(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }


        [HttpPost]
        [Route("GetMstBranch")]
        public HttpResponseMessage GetMstBranch(Mstcommonparameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                string json = MstBranchService.GetMstBranch(model);
                if (json == "" || json == "error")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }

        [HttpPost]
        [Route("Update_Mst_Branch")]
        public HttpResponseMessage Update_Mst_Domains(MstBranchParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = MstBranchService.Update_Mst_Branch(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }


        [HttpPost]
        [Route("GetMstForms")]
        public HttpResponseMessage GetMstForms(Mstcommonparameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                string json = MstFormServices.GetMstForms(model);
                if (json == "" || json == "error")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }

        [HttpPost]
        [Route("Update_Mst_Forms")]
        public HttpResponseMessage Update_Mst_Forms(MstFormParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = MstFormServices.Update_Mst_Forms(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }



        [HttpPost]
        [Route("GetMstSector")]
        public HttpResponseMessage GetMstSector(Mstcommonparameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                string json = MstSectorService.GetMstSector(model);
                if (json == "" || json == "error")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }

        [HttpPost]
        [Route("UpdateMstSector")]
        public HttpResponseMessage UpdateMstSector(MstSectorParameter model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = MstSectorService.UpdateMstSector(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }


        [HttpPost]
        [Route("GetMstSolutionCategory")]
        public HttpResponseMessage GetMstSolutionCategory(Mstcommonparameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                string json = MstSolutionCategoryService.GetMstSolutionCategory(model);
                if (json == "" || json == "error")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }

        [HttpPost]
        [Route("UpdateMstSolutionCategory")]
        public HttpResponseMessage UpdateMstSolutionCategory(MstSolutionCategoryParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = MstSolutionCategoryService.UpdateMstSolutionCategory(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }


        [HttpPost]
        [Route("GetMstCommentType")]
        public HttpResponseMessage GetMstCommentType(Mstcommonparameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                string json = MstCommentTypeService.GetMstCommentType(model);
                if (json == "" || json == "error")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }

        [HttpPost]
        [Route("Update_Mst_CommentType")]
        public HttpResponseMessage Update_Mst_CommentType(MstCommentTypeParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = MstCommentTypeService.Update_Mst_CommentType(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Field Validation Error Occured");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

        }
    }
}
