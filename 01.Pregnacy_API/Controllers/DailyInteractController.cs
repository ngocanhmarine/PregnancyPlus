using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
				IEnumerable<preg_daily_interact> result;
				if (!data.DeepEquals(new preg_daily_interact()))
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
		[Route("api/dailyinteract/{user_id}")]
		public HttpResponseMessage Get(string user_id)
		{
			try
			{
				preg_daily_interact data = dao.GetItemByUserID(Convert.ToInt32(user_id));
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
		public HttpResponseMessage Post([FromBody]preg_daily_interact data)
		{
			try
			{
				if (!data.DeepEquals(new preg_daily_interact()))
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
		[Route("api/dailyinteract/{daily_id}/{user_id}")]
		public HttpResponseMessage Put(string daily_id, string user_id, [FromBody]preg_daily_interact dataUpdate)
		{
			return UpdateData(daily_id, user_id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/dailyinteract/{daily_id}/{user_id}")]
		public HttpResponseMessage Delete(string daily_id, string user_id)
		{
			//lstStrings[id] = value;
			try
			{
				preg_daily_interact daily_interact = dao.GetItemByID(Convert.ToInt32(daily_id), Convert.ToInt32(user_id));
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
					daily = dao.GetItemByID(Convert.ToInt32(daily_id), Convert.ToInt32(user_id));
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