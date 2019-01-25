using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{
	public class AnswersController : ApiController
	{
		AnswerDao dao = new AnswerDao();
		// GET api/values
		[Authorize]
		[HttpGet]
		public async Task<HttpResponseMessage> Get([FromUri]preg_answer data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_answer> result;
				if (!data.DeepEquals(new preg_answer()))
				{
					data.user_id = user_id;
					result = dao.GetItemsByParams(data);
				}
				else
				{
					result = dao.GetListItem().Where(c => c.user_id == user_id);
				}
				if (result.ToList().Any())
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
		[Route("api/answers/{question_id}")]
		[HttpGet]
		public HttpResponseMessage Get(string question_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_answer data = dao.GetItemByID(user_id, Convert.ToInt32(question_id)).FirstOrDefault();
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
		[Authorize(Roles = "dev")]
		[HttpPost]
		public HttpResponseMessage Post([FromBody]preg_answer data)
		{
			try
			{
				if (!data.DeepEquals(new preg_answer()))
				{
					int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
					data.user_id = user_id;
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
		[Route("api/answers/{question_id}")]
		[HttpPut]
		public HttpResponseMessage Put(string question_id, [FromBody]preg_answer dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_answer()))
				{
					preg_answer answer = dao.GetItemByID(user_id, Convert.ToInt32(question_id)).FirstOrDefault();
					if (answer == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
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
		[Route("api/answers/{question_id}")]
		[HttpDelete]
		public HttpResponseMessage Delete(string question_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_answer item = dao.GetItemByID(user_id, Convert.ToInt32(question_id)).FirstOrDefault();
				if (item == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
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