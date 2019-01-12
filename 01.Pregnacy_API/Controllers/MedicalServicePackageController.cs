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
	public class MedicalServicePackageController : ApiController
	{
		MedicalServicePackageDao dao = new MedicalServicePackageDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_medical_service_package data)
		{
			try
			{
				IEnumerable<preg_medical_service_package> result;
				if (data != null)
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
		[Route("api/medicalservicepackage/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_medical_service_package data = dao.GetItemByID(Convert.ToInt32(id));
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
		public HttpResponseMessage Post([FromBody]preg_medical_service_package data)
		{
			try
			{
				if (data != null)
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
		[Route("api/medicalservicepackage/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_medical_service_package dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/medicalservicepackage/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_medical_service_package item = dao.GetItemByID(Convert.ToInt32(id));
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

		public HttpResponseMessage UpdateData(string id, preg_medical_service_package dataUpdate)
		{
			try
			{
				if (dataUpdate != null)
				{
					preg_medical_service_package medical_service_package = new preg_medical_service_package();
					medical_service_package = dao.GetItemByID(Convert.ToInt32(id));
					if (medical_service_package == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.title != null)
					{
						medical_service_package.title = dataUpdate.title;
					}
					if (dataUpdate.description != null)
					{
						medical_service_package.description = dataUpdate.description;
					}
					if (dataUpdate.content != null)
					{
						medical_service_package.content = dataUpdate.content;
					}
					if (dataUpdate.discount != null)
					{
						medical_service_package.discount = dataUpdate.discount;
					}
					if (dataUpdate.execution_department != null)
					{
						medical_service_package.execution_department = dataUpdate.execution_department;
					}
					if (dataUpdate.address != null)
					{
						medical_service_package.address = dataUpdate.address;
					}
					if (dataUpdate.execution_time != null)
					{
						medical_service_package.execution_time = dataUpdate.execution_time;
					}
					if (dataUpdate.place != null)
					{
						medical_service_package.place = dataUpdate.place;
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
