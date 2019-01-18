using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{
	public class BabyNamesController : ApiController
	{
		BabyNameDao dao = new BabyNameDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_baby_name data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_baby_name> result;
				if (!data.DeepEquals(new preg_baby_name()))
				{
					result = dao.GetItemsByParams(data);
				}
				else
				{
					result = dao.GetListItem();
				}
				if (result.Any())
				{
					return Request.CreateResponse(HttpStatusCode.OK, dao.FilterUserID(result, user_id));
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
		[Route("api/babynames/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst("id").Value);
				IQueryable<preg_baby_name> data = dao.GetItemByID(Convert.ToInt32(id));
				if (data.Any())
				{
					return Request.CreateResponse(HttpStatusCode.OK, dao.FilterUserID(data, user_id));
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
		public HttpResponseMessage Post([FromBody]preg_baby_name data)
		{
			try
			{
				if (!data.DeepEquals(new preg_baby_name()))
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
		[Route("api/babynames/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_baby_name dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_baby_name()))
				{
					preg_baby_name baby_name = new preg_baby_name();
					baby_name = dao.GetItemByID(Convert.ToInt32(id)).ToList()[0];
					if (baby_name == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.country_id != null)
					{
						baby_name.country_id = dataUpdate.country_id;
					}
					if (dataUpdate.gender_id != null)
					{
						baby_name.gender_id = dataUpdate.gender_id;
					}
					if (dataUpdate.name != null)
					{
						baby_name.name = dataUpdate.name;
					}
					if (dataUpdate.custom_baby_name_by_user_id != null)
					{
						baby_name.custom_baby_name_by_user_id = dataUpdate.custom_baby_name_by_user_id;
					}
					if (dataUpdate.order != null)
					{
						baby_name.order = dataUpdate.order;
					}

					dao.UpdateData(baby_name);
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
		[Route("api/babynames/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_baby_name item = dao.GetItemByID(Convert.ToInt32(id)).ToList()[0];
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