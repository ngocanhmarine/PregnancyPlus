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
	public class DailyInteractController : ApiController
	{

		DailyInteractDao dao = new DailyInteractDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_daily_interact data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_daily_interact> result;
				if (!data.DeepEquals(new preg_daily_interact()))
				{
					data.user_id = user_id;
					result = dao.GetItemsByParams(data);
				}
				else
				{
					result = dao.GetListItem().Where(c => c.user_id == user_id);
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

		// POST api/values
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Post([FromBody]preg_daily_interact data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (data.daily_id != 0)
				{
					//Check exist
					preg_daily_interact checkExist = dao.GetItemByID(data.daily_id, user_id).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
					}
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
		[Route("api/dailyinteract/{daily_id}")]
		public HttpResponseMessage Put(string daily_id, [FromBody]preg_daily_interact dataUpdate)
		{
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			return UpdateData(daily_id, user_id.ToString(), dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/dailyinteract/{daily_id}")]
		public HttpResponseMessage Delete(string daily_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_daily_interact daily_interact = dao.GetItemByID(Convert.ToInt32(daily_id), user_id).FirstOrDefault();
				if (daily_interact == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}
				dao.DeleteData(daily_interact);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		protected HttpResponseMessage UpdateData(string daily_id, string user_id, preg_daily_interact dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_daily_interact()))
				{
					preg_daily_interact daily = new preg_daily_interact();
					daily = dao.GetItemByID(Convert.ToInt32(daily_id), Convert.ToInt32(user_id)).FirstOrDefault();
					if (daily == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.like != null)
					{
						daily.like = dataUpdate.like;
					}
					if (dataUpdate.comment != null)
					{
						daily.comment = dataUpdate.comment;
					}
					if (dataUpdate.share != null)
					{
						daily.share = dataUpdate.share;
					}
					if (dataUpdate.notification != null)
					{
						daily.notification = dataUpdate.notification;
					}
					if (dataUpdate.status != null)
					{
						daily.status = dataUpdate.status;
					}

					dao.UpdateData(daily);
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