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
	public class PregnancysController : ApiController
	{
		PregnancyDao dao = new PregnancyDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromBody]preg_pregnancy data)
		{
			try
			{
				if (data != null)
				{
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
					IEnumerable<preg_pregnancy> result = dao.GetListItem();
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
		[Authorize]
		[Route("api/pregnancys/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_pregnancy data = dao.GetItemByID(Convert.ToInt32(id));
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
		public HttpResponseMessage Post([FromBody]preg_pregnancy data)
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
		[Route("api/pregnancys/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_pregnancy dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/pregnancys/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				dao.DeleteData(Convert.ToInt32(id));
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string id, [FromBody]preg_pregnancy dataUpdate)
		{
			try
			{
				if (dataUpdate != null)
				{
					preg_pregnancy pregnancy = new preg_pregnancy();
					pregnancy = dao.GetItemByID(Convert.ToInt32(id));
					if (pregnancy == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.user_id != null)
					{
						pregnancy.user_id = dataUpdate.user_id;
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