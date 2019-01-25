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
	public class ContractionController : ApiController
	{
		ContractionDao dao = new ContractionDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_contraction data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_contraction> result;
				if (!data.DeepEquals(new preg_contraction()))
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
		public HttpResponseMessage Post([FromBody]preg_contraction data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_contraction()))
				{
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
		[Route("api/contraction/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_contraction dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_contraction()))
				{
					preg_contraction contraction = new preg_contraction();
					contraction = dao.GetItemsByParams(new preg_contraction() { id = Convert.ToInt32(id), user_id = user_id }).FirstOrDefault();
					if (contraction == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.date_time != null)
					{
						contraction.date_time = dataUpdate.date_time;
					}
					if (dataUpdate.duration != null)
					{
						contraction.duration = dataUpdate.duration;
					}
					if (dataUpdate.interval != null)
					{
						contraction.interval = dataUpdate.interval;
					}

					dao.UpdateData(contraction);
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
		[Route("api/contraction/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_contraction contraction = dao.GetItemsByParams(new preg_contraction() { id = Convert.ToInt32(id), user_id = user_id }).FirstOrDefault();
				if (contraction == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}
				dao.DeleteData(contraction);
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