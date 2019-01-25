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

	public class GendersController : ApiController
	{
		GenderDao dao = new GenderDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_gender data)
		{
			try
			{
				IEnumerable<preg_gender> result;
				if (!data.DeepEquals(new preg_gender()))
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
		[Route("api/genders/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_gender data = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_gender data)
		{
			try
			{
				if (!data.DeepEquals(new preg_gender()))
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
		[Route("api/genders/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_gender dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_gender()))
				{
					preg_gender gender = new preg_gender();
					gender = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (gender == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.gender != null)
					{
						gender.gender = dataUpdate.gender;
					}

					dao.UpdateData(gender);
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
		[Route("api/genders/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_gender gender = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
				if (gender == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}
				dao.DeleteData(gender);
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
