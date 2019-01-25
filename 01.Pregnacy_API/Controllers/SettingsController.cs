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
	public class SettingsController : ApiController
	{
		SettingDao dao = new SettingDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_setting data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_setting()))
				{
					data.user_id = user_id;
					IEnumerable<preg_setting> result = dao.GetItemsByParams(data);
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
					IEnumerable<preg_setting> result = dao.GetListItem().Where(c => c.user_id == user_id);
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
		public HttpResponseMessage Post([FromBody]preg_setting data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_setting()))
				{
					//Check if data exist
					preg_setting checkExist = dao.GetItemsByParams(new preg_setting() { user_id = user_id }).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
					}

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
		[Authorize(Roles = "dev, admin")]
		[Route("api/settings/update")]
		public HttpResponseMessage Put([FromBody]preg_setting dataUpdate)
		{
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			return UpdateData(user_id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/settings/delete")]
		public HttpResponseMessage Delete()
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_setting item = dao.GetListItem().Where(c => c.user_id == user_id).FirstOrDefault();
				dao.DeleteData(item);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(int user_id, [FromBody]preg_setting dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_setting()))
				{
					preg_setting setting = new preg_setting();
					setting = dao.GetItemsByParams(new preg_setting() { user_id = user_id }).FirstOrDefault();
					if (setting == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.reminders != null)
					{
						setting.reminders = dataUpdate.reminders;
					}
					if (dataUpdate.length_units != null)
					{
						setting.length_units = dataUpdate.length_units;
					}
					if (dataUpdate.weight_unit != null)
					{
						setting.weight_unit = dataUpdate.weight_unit;
					}
					if (dataUpdate.revoke_consent != null)
					{
						setting.revoke_consent = dataUpdate.revoke_consent;
					}

					dao.UpdateData(setting);
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