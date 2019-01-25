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
	public class ShoppingItemsController : ApiController
	{
		ShoppingItemDao dao = new ShoppingItemDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_shopping_item data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_shopping_item()))
				{
					IEnumerable<preg_shopping_item> result = dao.GetItemsByParams(data).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id);
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
					IEnumerable<preg_shopping_item> result = dao.GetListItem().Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id);
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

		// GET api/values/5
		[Authorize]
		[Route("api/shoppingitems/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_shopping_item data = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_shopping_item data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_shopping_item()))
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
		[Route("api/shoppingitems/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_shopping_item dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/shoppingitems/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_shopping_item item = dao.GetItemsByParams(new preg_shopping_item() { id = Convert.ToInt32(id) }).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id).FirstOrDefault();
				if (item == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
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

		public HttpResponseMessage UpdateData(string id, [FromBody]preg_shopping_item dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_shopping_item()))
				{
					preg_shopping_item shopping_item = new preg_shopping_item();
					shopping_item = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.custom_item_by_user_id == null || c.custom_item_by_user_id == user_id).FirstOrDefault();
					if (shopping_item == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.item_name != null)
					{
						shopping_item.item_name = dataUpdate.item_name;
					}
					if (dataUpdate.custom_item_by_user_id != null)
					{
						shopping_item.custom_item_by_user_id = user_id;
					}
					if (dataUpdate.category_id != null)
					{
						shopping_item.category_id = dataUpdate.category_id;
					}
					if (dataUpdate.status != null)
					{
						shopping_item.status = dataUpdate.status;
					}

					dao.UpdateData(shopping_item);
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