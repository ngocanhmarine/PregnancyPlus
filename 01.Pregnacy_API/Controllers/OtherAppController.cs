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
	public class OtherAppController : ApiController
	{
		OtherAppDao dao = new OtherAppDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_other_app data)
		{
			try
			{
				if (!data.DeepEquals(new preg_other_app()))
				{
					IEnumerable<preg_other_app> result = dao.GetItemsByParams(data);
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
					IEnumerable<preg_other_app> result = dao.GetListItem();
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
		public HttpResponseMessage Post([FromBody]preg_other_app data)
		{
			try
			{
				if (!data.DeepEquals(new preg_other_app()))
				{
					data.time_created = DateTime.Now;
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
		[Route("api/otherapp/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_other_app dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/otherapp/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_other_app item = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
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

		public HttpResponseMessage UpdateData(string id, preg_other_app dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_other_app()))
				{
					preg_other_app other_app = new preg_other_app();
					other_app = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (other_app == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.name != null)
					{
						other_app.name = dataUpdate.name;
					}
					if (dataUpdate.google_play != null)
					{
						other_app.google_play = dataUpdate.google_play;
					}
					if (dataUpdate.app_store != null)
					{
						other_app.app_store = dataUpdate.app_store;
					}
					if (dataUpdate.time_created != null)
					{
						other_app.time_created = dataUpdate.time_created;
					}
					if (dataUpdate.time_last_update != null)
					{
						other_app.time_last_update = dataUpdate.time_last_update;
					}

					dao.UpdateData(other_app);
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
