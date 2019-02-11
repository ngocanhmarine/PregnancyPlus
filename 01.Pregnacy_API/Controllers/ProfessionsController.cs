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
	public class ProfessionsController : ApiController
	{
		ProfessionDao dao = new ProfessionDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_profession data)
		{
			try
			{
				if (!data.DeepEquals(new preg_profession()))
				{
					IEnumerable<preg_profession> result = dao.GetItemsByParams(data);
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
					IEnumerable<preg_profession> result = dao.GetListItem();
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
		[Route("api/professions/{user_id}")]
		public HttpResponseMessage Get(string user_id)
		{
			try
			{
				IEnumerable<preg_profession> data = dao.GetItemsByUserID(Convert.ToInt32(user_id));
				if (data.Count() > 0)
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
		[Authorize]
		[Route("api/professions/{user_id}/{profession_type_id}")]
		public HttpResponseMessage Get(string user_id, string profession_type_id)
		{
			try
			{
				preg_profession data = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(profession_type_id)).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_profession data)
		{
			try
			{
				if (data.user_id != 0 && data.profession_type_id != 0)
				{
					//Check user & profession type exist
					using (PregnancyEntity connect = new PregnancyEntity())
					{
						preg_user checkUserExist = connect.preg_user.Where(c => c.id == data.user_id).FirstOrDefault();
						preg_profession_type checkPTypeExist = connect.preg_profession_type.Where(c => c.id == data.profession_type_id).FirstOrDefault();
						if (checkUserExist == null || checkPTypeExist == null)
						{
							return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
						}
					}
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
		[Route("api/professions/{user_id}/{profession_type_id}")]
		public HttpResponseMessage Put(string user_id, string profession_type_id, [FromBody]preg_profession dataUpdate)
		{
			return UpdateData(user_id, profession_type_id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/professions/{user_id}/{profession_type_id}")]
		public HttpResponseMessage Delete(string user_id, string profession_type_id)
		{
			try
			{
				dao.DeleteData(Convert.ToInt32(user_id), Convert.ToInt32(profession_type_id));
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string user_id, string profession_type_id, [FromBody]preg_profession dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_profession()))
				{
					preg_profession profession = new preg_profession();
					profession = dao.GetItemByID(Convert.ToInt32(user_id), Convert.ToInt32(profession_type_id)).FirstOrDefault();
					if (profession == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.status != null)
					{
						profession.status = dataUpdate.status;
					}

					dao.UpdateData(profession);
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