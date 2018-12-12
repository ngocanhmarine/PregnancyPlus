﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using PregnancyData.Entity;
using System.Text;
using System.Security.Cryptography;
using PregnancyData.Dao;

namespace _01.Pregnacy_API.Controllers
{

    public class ImagesController : ApiController
    {
        ImageDao dao = new ImageDao();
        // GET api/values
        [Authorize]
        public HttpResponseMessage Get([FromBody]preg_image data)
        {
            try
            {
                IEnumerable<preg_image> result;
                if (data != null)
                {
                    result = dao.GetItemsByParams(data);

                }
                else
                {
                    result = dao.GetListItem();

                }
                if (result.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    HttpError err = new HttpError(SysConst.DATA_NOT_FOUND);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                }
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
            }
        }

        // GET api/values/5
        [Authorize]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                preg_image data = dao.GetItemByID(Convert.ToInt32(id));
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    HttpError err = new HttpError(SysConst.DATA_NOT_FOUND);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                }
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
            }
        }

        // POST api/values
        [Authorize(Roles = "dev, admin")]
        public HttpResponseMessage Post([FromBody]preg_image data)
        {
            try
            {
                if (data != null)
                {
                    dao.InsertData(data);
                    return Request.CreateResponse(HttpStatusCode.Created, SysConst.DATA_INSERT_SUCCESS);
                }
                else
                {
                    HttpError err = new HttpError(SysConst.DATA_EMPTY);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
                }
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

        // PUT api/values/5
        [Authorize(Roles = "dev, admin")]
        public HttpResponseMessage Put(string id, [FromBody]preg_image dataUpdate)
        {
            //lstStrings[id] = value;
            try
            {
                if (dataUpdate != null)
                {
                    preg_image image = new preg_image();
                    image = dao.GetItemByID(Convert.ToInt32(id));
                    if (image == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
                    }
                    image.image = dataUpdate.image;
                    image.image_type_id = dataUpdate.image_type_id;
                    image.week_id = dataUpdate.week_id;
                    dao.UpdateData(image);
                    return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_UPDATE_SUCCESS);
                }
                else
                {
                    HttpError err = new HttpError(SysConst.DATA_EMPTY);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
                }
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

        // DELETE api/values/5
        [Authorize(Roles = "dev, admin")]
        public HttpResponseMessage Delete(string id)
        {
            //lstStrings[id] = value;
            try
            {
                preg_image image = dao.GetItemByID(Convert.ToInt32(id));
                if (image == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
                }
                dao.DeleteData(image);
                return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }
    }
}
