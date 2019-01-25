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
	public class UserBabyNameController : ApiController
	{
		UserBabyNameDao dao = new UserBabyNameDao();
		// GET api/values
		[Authorize(Roles = "dev, admin")]
		[HttpGet]
		public HttpResponseMessage Get([FromUri]preg_user_baby_name data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_user_baby_name()))
				{
					data.user_id = user_id;
					IEnumerable<preg_user_baby_name> result = dao.GetItemByParams(data);
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
					IEnumerable<preg_user_baby_name> result = dao.GetListItem().Where(c => c.user_id == user_id);
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
		public HttpResponseMessage Post([FromBody]preg_user_baby_name data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_user_baby_name()))
				{
					data.user_id = user_id;
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

		//// PUT api/values/5
		//[Authorize(Roles = "dev, admin")]
		//[HttpPut]
		//[Route("api/userbabyname/{user_id}/{baby_name_id}")]
		//public HttpResponseMessage Put(string user_id, string baby_name_id, [FromBody]preg_user_baby_name dataUpdate)
		//{
		//	//lstStrings[id] = value;
		//	try
		//	{
		//		if (dataUpdate != null)
		//		{
		//			preg_user_baby_name user = new preg_user_baby_name();
		//			user = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(baby_name_id));
		//			user.user_id = dataUpdate.user_id;
		//			user.baby_name_id = dataUpdate.baby_name_id;

		//			dao.UpdateData(user);
		//			return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_UPDATE_SUCCESS);
		//		}
		//		else
		//		{
		//			HttpError err = new HttpError(SysConst.DATA_NOT_EMPTY);
		//			return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HttpError err = new HttpError(ex.Message);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
		//	}
		//}


		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[HttpDelete]
		[Route("api/userbabyname/{baby_name_id}")]
		public HttpResponseMessage Delete(string baby_name_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_user_baby_name item = dao.GetItemByID(user_id, Convert.ToInt32(baby_name_id)).FirstOrDefault();
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
