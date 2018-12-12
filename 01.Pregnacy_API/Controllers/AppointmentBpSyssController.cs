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
	public class AppointmentBpSyssController : ApiController
	{
		AppointmentBpSysDao dao = new AppointmentBpSysDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromBody]preg_appointment_bp_sys data)
		{
			try
			{
                IEnumerable<preg_appointment_bp_sys> result;
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
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_appointment_bp_sys data = dao.GetItemByID(Convert.ToInt32(id));
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
		public HttpResponseMessage Post([FromBody]preg_appointment_bp_sys data)
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
					HttpError err = new HttpError(SysConst.DATA_EMPTY);
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
		public HttpResponseMessage Put(string id, [FromBody]preg_appointment_bp_sys dataUpdate)
		{

			try
			{
				if (dataUpdate != null)
				{
					preg_appointment_bp_sys appointment_bp_sys = new preg_appointment_bp_sys();
					appointment_bp_sys = dao.GetItemByID(Convert.ToInt32(id));
                    if (appointment_bp_sys == null)
                    {

                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
                    }
					appointment_bp_sys.value = dataUpdate.value;

					dao.UpdateData(appointment_bp_sys);
					return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_UPDATE_SUCCESS);
				}
				else
				{
					HttpError err = new HttpError(SysConst.DATA_EMPTY);
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
		public HttpResponseMessage Delete(string id)
		{
			//lstStrings[id] = value;
			try
			{
                preg_appointment_bp_sys appointment_bp_sys = dao.GetItemByID(Convert.ToInt32(id));
                if (appointment_bp_sys == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
                }
                dao.DeleteData(appointment_bp_sys);
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