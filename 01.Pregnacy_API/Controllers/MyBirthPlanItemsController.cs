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

	public class MyBirthPlanItemsController : ApiController
	{
		MyBirthPlanItemDao dao = new MyBirthPlanItemDao();
		// GET api/values
		[Authorize]
		[HttpGet]
		public HttpResponseMessage Get([FromUri]preg_my_birth_plan_item data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_my_birth_plan_item> result;
				if (!data.DeepEquals(new preg_my_birth_plan_item()))
				{
					result = dao.GetItemsByParams(data).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id);
				}
				else
				{
					result = dao.GetListItem().Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id);
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
		[Route("api/mybirthplanitems/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_my_birth_plan_item data = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id).FirstOrDefault();
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
		[HttpPost]
		public HttpResponseMessage Post([FromBody]preg_my_birth_plan_item data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_my_birth_plan_item()))
				{
					if (data.custom_item_by_user_id != null)
					{
						data.custom_item_by_user_id = user_id;
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
		[HttpPut]
		[Route("api/mybirthplanitems/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_my_birth_plan_item dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_my_birth_plan_item()))
				{
					preg_my_birth_plan_item my_birth_plan_item = new preg_my_birth_plan_item();
					my_birth_plan_item = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id).FirstOrDefault();
					if (my_birth_plan_item == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.my_birth_plan_type_id != null)
					{
						my_birth_plan_item.my_birth_plan_type_id = dataUpdate.my_birth_plan_type_id;
					}
					if (dataUpdate.item_content != null)
					{
						my_birth_plan_item.item_content = dataUpdate.item_content;
					}
					if (dataUpdate.custom_item_by_user_id != null)
					{
						my_birth_plan_item.custom_item_by_user_id = user_id;
					}

					dao.UpdateData(my_birth_plan_item);
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
		[Route("api/mybirthplanitems/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_my_birth_plan_item item = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id).FirstOrDefault();
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
