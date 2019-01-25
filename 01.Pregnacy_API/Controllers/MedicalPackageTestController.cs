using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using PregnancyData.Entity;
using System.Text;
using PregnancyData.Dao;

namespace _01.Pregnacy_API.Controllers
{
	public class MedicalPackageTestController : ApiController
	{
		MedicalPackageTestDao dao = new MedicalPackageTestDao();
		// GET api/values
		[Authorize(Roles = "dev, admin")]
		[HttpGet]
		public HttpResponseMessage Get([FromUri]preg_medical_package_test data)
		{
			try
			{
				if (!data.DeepEquals(new preg_medical_package_test()))
				{
					IEnumerable<preg_medical_package_test> result = dao.GetItemsByParams(data);
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
					IEnumerable<preg_medical_package_test> result = dao.GetListItem();
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
		[Authorize(Roles = "dev, admin")]
		[HttpGet]
		[Route("api/medicalpackagetest/{medical_service_package_id}")]
		public HttpResponseMessage Get(string medical_service_package_id)
		{
			try
			{
				preg_medical_package_test data = dao.GetItemByPackageID(Convert.ToInt32(medical_service_package_id)).FirstOrDefault();
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
		[Authorize(Roles = "dev, admin")]
		[HttpGet]
		[Route("api/medicalpackagetest/{medical_service_package_id}/{medical_test_id}")]
		public HttpResponseMessage Get(string medical_service_package_id, string medical_test_id)
		{
			try
			{
				preg_medical_package_test data = dao.GetItemByID(Convert.ToInt32(medical_service_package_id), Convert.ToInt32(medical_test_id)).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_medical_package_test data)
		{
			try
			{
				if (data.medical_service_package_id != 0 && data.medical_test_id != 0)
				{
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



		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[HttpDelete]
		[Route("api/medicalpackagetest/{medical_service_package_id}/{medical_test_id}")]
		public HttpResponseMessage Delete(string medical_service_package_id, string medical_test_id)
		{
			try
			{
				preg_medical_package_test data = dao.GetItemByID(Convert.ToInt32(medical_service_package_id), Convert.ToInt32(medical_test_id)).FirstOrDefault();
				dao.DeleteData(data);
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