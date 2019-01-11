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
using System.Security.Cryptography;
using PregnancyData.Dao;

namespace _01.Pregnacy_API.Controllers
{

	public class AppointmentMeasurementController : ApiController
	{
		AppointmentMeasurementDao dao = new AppointmentMeasurementDao();
		// GET api/values
		[HttpGet]
		[Authorize]
		public HttpResponseMessage Get([FromBody]preg_appointment_measurement data)
		{
			try
			{
				IEnumerable<preg_appointment_measurement> result;
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
		[HttpGet]
		[Authorize]
		[Route("api/appointmentmeasurement/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_appointment_measurement data = dao.GetItemByID(Convert.ToInt32(id));
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
		[HttpPost]
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Post([FromBody]preg_appointment_measurement data)
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
		[HttpPut]
		[Authorize(Roles = "dev, admin")]
		[Route("api/appointmentmeasurement/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_appointment_measurement dataUpdate)
		{
			try
			{
				if (dataUpdate != null)
				{
					preg_appointment_measurement appointment_measurement = new preg_appointment_measurement();
					appointment_measurement = dao.GetItemByID(Convert.ToInt32(id));
					if (appointment_measurement == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.appointment_id != null)
					{
						appointment_measurement.appointment_id = dataUpdate.appointment_id;
					}
					if (dataUpdate.bp_dia != null)
					{
						appointment_measurement.bp_dia = dataUpdate.bp_dia;
					}
					if (dataUpdate.bp_sys != null)
					{
						appointment_measurement.bp_sys = dataUpdate.bp_sys;
					}
					if (dataUpdate.baby_heart != null)
					{
						appointment_measurement.baby_heart = dataUpdate.baby_heart;
					}

					dao.UpdateData(appointment_measurement);
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
		[HttpDelete]
		[Authorize(Roles = "dev, admin")]
		[Route("api/appointmentmeasurement/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_appointment_measurement appointment = dao.GetItemByID(Convert.ToInt32(id));
				if (appointment == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}
				dao.DeleteData(appointment);
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
