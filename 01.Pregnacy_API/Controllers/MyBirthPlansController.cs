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
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_my_birth_plan> result;
				if (!data.DeepEquals(new preg_my_birth_plan()))
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
		[HttpPost]
		public HttpResponseMessage Post([FromBody]preg_my_birth_plan data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (data.my_birth_plan_item_id != 0)
				{
					//Check exist
					preg_my_birth_plan checkExist = dao.GetItemByID(user_id, data.my_birth_plan_item_id).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
					}
					//Check My Birth Plan Item Exist
					using (PregnancyEntity connect = new PregnancyEntity())
					{
						preg_my_birth_plan_item checkMyBPIExist = connect.preg_my_birth_plan_item.Where(c => c.id == data.my_birth_plan_item_id).FirstOrDefault();
						if (checkMyBPIExist == null)
						{
							return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
						}
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
		[Route("api/mybirthplans/{my_birth_plan_item_id}")]
		public HttpResponseMessage Delete(string my_birth_plan_item_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_my_birth_plan item = dao.GetItemByID(user_id, Convert.ToInt32(my_birth_plan_item_id)).FirstOrDefault();
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