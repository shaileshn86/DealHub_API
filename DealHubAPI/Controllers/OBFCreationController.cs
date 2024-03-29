﻿using DealHub_Domain.DashBoard;
using DealHub_Domain.Enum;
using DealHub_Domain.Helpers;
using DealHub_Service.Implemantations.APIServices;
using DealHubAPI.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DealHubAPI.Controllers
{
    [RoutePrefix("Api/Manage_OBF")]
    public class OBFCreationController : BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateOBF")]
        public HttpResponseMessage CreateOBF(ObfCreationParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<ObfCreationDetailsParameters> _ObfCreationDetailsParameters = ObfServices.ObfCreation(model);

                if (_ObfCreationDetailsParameters != null)
                {
                    if (_ObfCreationDetailsParameters.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_ObfCreationDetailsParameters));
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

            return null;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("EditCustomerCodeandIo")]
        public HttpResponseMessage Edit_CustomerCode_and_io(ObfCreationParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<ObfCreationDetailsParameters> _ObfCreationDetailsParameters = ObfServices.editcustomercodeandio(model);

                if (_ObfCreationDetailsParameters != null)
                {
                    if (_ObfCreationDetailsParameters.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_ObfCreationDetailsParameters));
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

            return null;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("SaveServiceSolutionSector")]
        public HttpResponseMessage SaveServiceSolution(SaveServiceSolutionParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = ObfServices.SaveServiceSolutionSector(model);

                if (_SaveAttachementDetailsParameters != null)
                {
                    if (_SaveAttachementDetailsParameters.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_SaveAttachementDetailsParameters));
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

            return null;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("SubmitOBF")]
        public HttpResponseMessage SubmitOBF(SubmitOBFParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = ObfServices.submit_dh_headers(model);

                if (_SaveAttachementDetailsParameters != null)
                {
                    if (_SaveAttachementDetailsParameters.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_SaveAttachementDetailsParameters));
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

            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetMasterOBF")]
        public HttpResponseMessage GetMasterOBF(string userid)
        {
            string json = ObfServices.GetMastersOBFCreation(userid);
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


        [HttpGet]
        [AllowAnonymous]
        [Route("getmastersolutions")]
        public HttpResponseMessage get_master_solutions(string userid)
        {
            List<SolutionCategory> _SolutionCategory = ObfServices.get_master_solutions(userid);
            if (_SolutionCategory != null)
            {
                if (_SolutionCategory.Count != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_SolutionCategory));
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

        [HttpPost]
        [AllowAnonymous]
        [Route("geteditobfdata")]
        public HttpResponseMessage get_editobf(editobfarguement model)
        {
            EditObfParameters _editobf = ObfServices.get_edit_obf(model);
            if (_editobf != null)
            {
                
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_editobf));
                
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }



        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ApproveRejectObf")]
        public HttpResponseMessage ApproveRejectObf(ApproveRejectOBFParameter model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = ObfServices.ApproveRejectObf(model);

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

            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SaveAttachmentDetails")]
        public HttpResponseMessage SaveAttachmentDetails(List<SaveAttachmentParameter> model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<SaveAttachementDetailsParameters> _commanmessges = ObfServices.SaveAttachment(model);

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
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetOBFSummaryDataVersionWise")]
        public HttpResponseMessage GetOBFSummaryDataVersionWise(string dh_id,string dh_header_id)
        {
            string json = ObfServices.GetOBFSummaryDataVersionWise(Convert.ToInt32(dh_id), Convert.ToInt32(dh_header_id));
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


        [HttpGet]
        [AllowAnonymous]
        [Route("GetAttachmentDocument")]
        public HttpResponseMessage GetAttachmentDocument(string dh_id,string dh_header_id)
        {
            string json = ObfServices.GetAttachmentDocument(Convert.ToInt32(dh_id), Convert.ToInt32(dh_header_id));
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



    }



}
