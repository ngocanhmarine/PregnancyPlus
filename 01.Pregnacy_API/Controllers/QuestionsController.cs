using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{
	public class QuestionsController : ApiController
	{
		QuestionDao dao = new QuestionDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_question data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_question()))
				{
					IEnumerable<preg_question> result = dao.GetItemsByParams(data).Where(c => c.custom_question_by_user_id == null || c.custom_question_by_user_id == user_id);
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
				else
				{
					IEnumerable<preg_question> result = dao.GetListItem().Where(c => c.custom_question_by_user_id == null || c.custom_question_by_user_id == user_id);
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
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
			}
		}

		// GET api/values/5
		[Authorize]
		[Route("api/questions/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_question data = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.custom_question_by_user_id == null || c.custom_question_by_user_id == user_id).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_question data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_question()))
				{
					if (data.custom_question_by_user_id != null)
					{
						data.custom_question_by_user_id = user_id;
					}
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
		[Route("api/questions/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_question dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/questions/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_question item = dao.GetItemsByParams(new preg_question() { id = Convert.ToInt32(id) }).Where(c => c.custom_question_by_user_id == null || c.custom_question_by_user_id == user_id).FirstOrDefault();
				dao.DeleteData(item);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string id, [FromBody]preg_question dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_question()))
				{
					preg_question question = new preg_question();
					question = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.custom_question_by_user_id == null || c.custom_question_by_user_id == user_id).FirstOrDefault();
					if (question == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.question_type_id != null)
					{
						question.question_type_id = dataUpdate.question_type_id;
					}
					if (dataUpdate.content != null)
					{
						question.content = dataUpdate.content;
					}
					if (dataUpdate.custom_question_by_user_id != null)
					{
						question.custom_question_by_user_id = user_id;
					}

					dao.UpdateData(question);
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
	}
}