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
	public class PregnancysController : ApiController
	{
		PregnancyDao dao = new PregnancyDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_pregnancy data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_pregnancy()))
				{
					data.user_id = user_id;
					IEnumerable<preg_pregnancy> result = dao.GetItemsByParams(data);
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
					IEnumerable<preg_pregnancy> result = dao.GetListItem().Where(c => c.user_id == user_id);
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
		public HttpResponseMessage Post([FromBody]preg_pregnancy data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				// Check data exist
				preg_pregnancy checkData = dao.GetItemsByParams(new preg_pregnancy() { user_id = user_id }).FirstOrDefault();
				if (checkData != null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
				}
				// Check data null
				if (!data.DeepEquals(new preg_pregnancy()))
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
		[Authorize(Roles = "dev, admin")]
		[Route("api/pregnancys/update")]
		public HttpResponseMessage Put([FromBody]preg_pregnancy dataUpdate)
		{
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			return UpdateData(user_id.ToString(), dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/pregnancys/delete")]
		public HttpResponseMessage Delete()
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				dao.DeleteData(user_id);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string user_id, [FromBody]preg_pregnancy dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_pregnancy()))
				{
					preg_pregnancy pregnancy = new preg_pregnancy();
					pregnancy = dao.GetItemsByParams(new preg_pregnancy() { user_id = Convert.ToInt32(user_id) }).FirstOrDefault();
					if (pregnancy == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.baby_gender != null)
					{
						pregnancy.baby_gender = dataUpdate.baby_gender;
					}
					if (dataUpdate.due_date != null)
					{
						pregnancy.due_date = dataUpdate.due_date;
					}
					if (dataUpdate.show_week != null)
					{
						pregnancy.show_week = dataUpdate.show_week;
					}
					if (dataUpdate.pregnancy_loss != null)
					{
						pregnancy.pregnancy_loss = dataUpdate.pregnancy_loss;
					}
					if (dataUpdate.baby_already_born != null)
					{
						pregnancy.baby_already_born = dataUpdate.baby_already_born;
					}
					if (dataUpdate.date_of_birth != null)
					{
						pregnancy.date_of_birth = dataUpdate.date_of_birth;
					}
					if (dataUpdate.weeks_pregnant != null)
					{
						pregnancy.weeks_pregnant = dataUpdate.weeks_pregnant;
					}
					if (dataUpdate.start_date != null)
					{
						pregnancy.start_date = dataUpdate.start_date;
					}

					dao.UpdateData(pregnancy);
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