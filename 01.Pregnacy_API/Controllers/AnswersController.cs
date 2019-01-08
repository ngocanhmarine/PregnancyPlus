﻿using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace _01.Pregnacy_API.Controllers
{
	public class AnswersController : ApiController
	{
		AnswerDao dao = new AnswerDao();
		// GET api/values
		[Authorize]
		[HttpGet]
		public async Task<HttpResponseMessage> Get([FromBody]preg_answer data)
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
		[Route("api/answers/{user_id}/{question_id}")]
		[HttpGet]
		public HttpResponseMessage Get(string user_id, string question_id)
		{
			try
			{
				preg_answer data = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(question_id));
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

		// GET api/values/5
		[Authorize]
		[Route("api/answers/{user_id}")]
		[HttpGet]
		public HttpResponseMessage Get(string user_id)
		{
			try
			{
				IEnumerable<preg_answer> data = dao.GetItemByUserID(Convert.ToInt32(user_id));
				if (data.Count() > 0)
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
		[Authorize(Roles = "dev")]
		[HttpPost]
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
					HttpError err = new HttpError(SysConst.DATA_NOT_EMPTY);
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
		[Route("api/answers/{user_id}/{question_id}")]
		[HttpPut]
		public HttpResponseMessage Put(string user_id, string question_id, [FromBody]preg_answer dataUpdate)
		{
			try
			{
				if (dataUpdate != null)
				{
					preg_answer answer = new preg_answer();
					answer = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(question_id));
					if (answer == null)
					{
						return Request.CreateResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.questiondate != null)
					{
						answer.questiondate = dataUpdate.questiondate;
					}
					if (dataUpdate.title != null)
					{
						answer.title = dataUpdate.title;
					}
					if (dataUpdate.content != null)
					{
						answer.content = dataUpdate.content;
					}

					dao.UpdateData(answer);
					return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_UPDATE_SUCCESS);
				}
				else
				{
					HttpError err = new HttpError(SysConst.DATA_NOT_EMPTY);
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
		[Route("api/answers/{user_id}/{question_id}")]
		[HttpDelete]
		public HttpResponseMessage Delete(string user_id, string question_id)
		{
			try
			{
				preg_answer item = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(question_id));
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