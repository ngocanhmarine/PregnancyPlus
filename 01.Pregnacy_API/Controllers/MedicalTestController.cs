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
	public class MedicalTestController : ApiController
	{
		MedicalTestDao dao = new MedicalTestDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_medical_test data)
		{
			try
			{
				IEnumerable<preg_medical_test> result;
				if (!data.DeepEquals(new preg_medical_test()))
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
		[Route("api/medicaltest/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_medical_test data = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_medical_test data)
		{
			try
			{
				if (!data.DeepEquals(new preg_medical_test()))
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
		[Route("api/medicaltest/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_medical_test dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/medicaltest/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_medical_test item = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
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

		public HttpResponseMessage UpdateData(string id, preg_medical_test dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_medical_test()))
				{
					preg_medical_test medical_service_package = new preg_medical_test();
					medical_service_package = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (medical_service_package == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.title != null)
					{
						medical_service_package.title = dataUpdate.title;
					}
					if (dataUpdate.type != null)
					{
						medical_service_package.type = dataUpdate.type;
					}
					if (dataUpdate.price != null)
					{
						medical_service_package.price = dataUpdate.price;
					}

					dao.UpdateData(medical_service_package);
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