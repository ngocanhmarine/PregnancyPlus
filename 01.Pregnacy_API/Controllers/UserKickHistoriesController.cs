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
	public class UserKickHistoriesController : ApiController
	{
		UserKickHistoryDao dao = new UserKickHistoryDao();
		// GET api/values
		[Authorize(Roles = "dev, admin")]
		[HttpGet]
		public HttpResponseMessage Get([FromUri]preg_user_kick_history data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_user_kick_history()))
				{
					data.user_id = user_id;
					IEnumerable<preg_user_kick_history> result = dao.GetItemByParams(data);
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
					IEnumerable<preg_user_kick_history> result = dao.GetListItem().Where(c => c.user_id == user_id);
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

		// POST api/values
		[Authorize(Roles = "dev, admin")]
		[HttpPost]
		public HttpResponseMessage Post([FromBody]preg_user_kick_history data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (data.kick_result_id != 0)
				{
					data.user_id = user_id;
					//Check exist
					preg_user_kick_history checkExist = dao.GetItemByParams(new preg_user_kick_history() { user_id = data.user_id, kick_result_id = data.kick_result_id }).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
					}

					if (dao.InsertData(data))
					{
						return Request.CreateResponse(HttpStatusCode.Created, SysConst.DATA_INSERT_SUCCESS);
					}
					else
					{
						HttpError err = new HttpError(SysConst.DATA_EXIST);
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
					}
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
		[HttpPut]
		[Route("api/userbabyname/{kick_result_id}")]
		public HttpResponseMessage Put(string kick_result_id, [FromBody]preg_user_kick_history dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (dataUpdate.kick_result_id != 0)
				{
					preg_user_kick_history user = new preg_user_kick_history();
					user = dao.GetItemByParams(new preg_user_kick_history() { user_id = user_id, kick_result_id = Convert.ToInt32(kick_result_id) }).FirstOrDefault();
					if (user == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}

					if (dataUpdate.kick_date != null)
					{
						user.kick_date = dataUpdate.kick_date;
					}
					if (dataUpdate.duration != null)
					{
						user.duration = dataUpdate.duration;
					}

					dao.UpdateData(user);
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
		[HttpDelete]
		[Route("api/userkickhistories/{kick_result_id}")]
		public HttpResponseMessage Delete(string kick_result_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_user_kick_history item = dao.GetItemByParams(new preg_user_kick_history() { user_id = user_id, kick_result_id = Convert.ToInt32(kick_result_id) }).FirstOrDefault();
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
