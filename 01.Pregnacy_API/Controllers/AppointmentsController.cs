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

	public class AppointmentsController : ApiController
	{
		AppointmentDao dao = new AppointmentDao();
		// GET api/values
		[HttpGet]
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_appointment data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_appointment> result;
				if (!data.DeepEquals(new preg_appointment()))
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
		[HttpPost]
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Post([FromBody]preg_appointment data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				//Check if user already have pregnancy data
				preg_appointment checkExist = dao.GetItemsByParams(new preg_appointment() { user_id = user_id }).FirstOrDefault();
				if (checkExist != null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
				}

				if (!data.DeepEquals(new preg_appointment()))
				{
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

		// PUT api/values/5
		[HttpPut]
		[Authorize(Roles = "dev, admin")]
		[Route("api/appointments/update")]
		public HttpResponseMessage Put([FromBody]preg_appointment dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_appointment()))
				{
					preg_appointment appointment = new preg_appointment();
					appointment = dao.GetItemsByParams(new preg_appointment() { user_id = user_id }).FirstOrDefault();
					if (appointment == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.name != null)
					{
						appointment.name = dataUpdate.name;
					}
					if (dataUpdate.contact_name != null)
					{
						appointment.contact_name = dataUpdate.contact_name;
					}
					if (dataUpdate.profession_id != null)
					{
						appointment.profession_id = dataUpdate.profession_id;
					}
					if (dataUpdate.appointment_date != null)
					{
						appointment.appointment_date = dataUpdate.appointment_date;
					}
					if (dataUpdate.appointment_time != null)
					{
						appointment.appointment_time = dataUpdate.appointment_time;
					}
					if (dataUpdate.my_weight_type_id != null)
					{
						appointment.my_weight_type_id = dataUpdate.my_weight_type_id;
					}
					if (dataUpdate.weight_in_st != null)
					{
						appointment.weight_in_st = dataUpdate.weight_in_st;
					}
					if (dataUpdate.sync_to_calendar != null)
					{
						appointment.sync_to_calendar = dataUpdate.sync_to_calendar;
					}
					if (dataUpdate.note != null)
					{
						appointment.note = dataUpdate.note;
					}
					if (dataUpdate.appointment_type_id != null)
					{
						appointment.appointment_type_id = dataUpdate.appointment_type_id;
					}

					dao.UpdateData(appointment);
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
		[Route("api/appointments/delete")]
		public HttpResponseMessage Delete()
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_appointment appointment = dao.GetItemsByParams(new preg_appointment() { user_id = user_id }).FirstOrDefault();
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