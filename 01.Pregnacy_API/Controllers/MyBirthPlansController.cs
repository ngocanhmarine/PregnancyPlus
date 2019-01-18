using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PregnancyData.Entity;
using PregnancyData.Dao;

namespace _01.Pregnacy_API.Controllers
{

	public class MyBirthPlansController : ApiController
	{
		MyBirthPlanDao dao = new MyBirthPlanDao();
		// GET api/values
		[Authorize]
		[HttpGet]
		public HttpResponseMessage Get([FromUri]preg_my_birth_plan data)
		{
			try
			{
				IEnumerable<preg_my_birth_plan> result;
				if (!data.DeepEquals(new preg_my_birth_plan()))
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
		[HttpGet]
		[Route("api/mybirthplans/{user_id}/{my_birth_plan_item_id}")]
		public HttpResponseMessage Get(string user_id, string my_birth_plan_item_id)
		{
			try
			{
				preg_my_birth_plan data = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(my_birth_plan_item_id));
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

		// GET api/values/5
		[Authorize]
		[HttpGet]
		[Route("api/mybirthplans/{user_id}")]
		public HttpResponseMessage Get(string user_id)
		{
			try
			{
				IEnumerable<preg_my_birth_plan> data = dao.GetItemByUserID(Convert.ToInt32(user_id));
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
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
			}
		}

		// POST api/values
		[Authorize(Roles = "dev, admin")]
		[HttpPost]
		public HttpResponseMessage Post([FromBody]preg_my_birth_plan data)
		{
			try
			{
				if (!data.DeepEquals(new preg_my_birth_plan()))
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

		//// PUT api/values/5
		//[Authorize(Roles = "dev, admin")]
		//[HttpPut]
		//[Route("api/mybirthplans/{user_id}/{my_birth_plan_item_id}")]
		//public HttpResponseMessage Put(string user_id, string my_birth_plan_item_id, [FromBody]preg_my_birth_plan dataUpdate)
		//{
		//	//lstStrings[id] = value;
		//	try
		//	{
		//		if (dataUpdate != null)
		//		{
		//			preg_my_birth_plan my_birth_plan = new preg_my_birth_plan();
		//			my_birth_plan = dao.GetItemByID(Convert.ToInt32(user_id),Convert.ToInt32(my_birth_plan_item_id));
		//                  if (my_birth_plan == null)
		//                  {
		//                      return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
		//                  }
		//			my_birth_plan.user_id = dataUpdate.user_id;
		//			my_birth_plan.my_birth_plan_item_id = dataUpdate.my_birth_plan_item_id;

		//			dao.UpdateData(my_birth_plan);
		//			return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_UPDATE_SUCCESS);
		//		}
		//		else
		//		{
		//			HttpError err = new HttpError(SysConst.DATA_EMPTY);
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
		[Route("api/mybirthplans/{user_id}/{my_birth_plan_item_id}")]
		public HttpResponseMessage Delete(string user_id, string my_birth_plan_item_id)
		{
			try
			{
				preg_my_birth_plan item = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(my_birth_plan_item_id));
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