﻿using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{
	public class AnswersController : ApiController
	{
		AnswerDao dao = new AnswerDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromBody]preg_answer data)
		{
			try
			{
                IEnumerable<preg_answer> result;
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
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		// GET api/values/5
		[Authorize]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_answer data = dao.GetItemByID(Convert.ToInt32(id));
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		// POST api/values
		[Authorize(Roles ="dev")]
		public HttpResponseMessage Post([FromBody]preg_answer data)
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
		public HttpResponseMessage Put(string id, [FromBody]preg_answer dataUpdate)
		{

			try
			{
				if (dataUpdate != null)
				{
					preg_answer answer = new preg_answer();
					answer = dao.GetItemByID(Convert.ToInt32(id));
                    if (answer== null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
                    }
					answer.answerdate = dataUpdate.answerdate;
					answer.content = dataUpdate.content;
					answer.preg_user = dataUpdate.preg_user;
					answer.question_id = dataUpdate.question_id;
					answer.title = dataUpdate.title;
					answer.user_id = dataUpdate.user_id;

					dao.UpdateData(answer);
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
                preg_answer item = dao.GetItemByID(Convert.ToInt32(id));
                if (item == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
                }
                dao.DeleteData(item);
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