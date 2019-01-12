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
	public class UsersController : ApiController
	{
		UserDao dao = new UserDao();
		// GET api/users
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Get([FromUri]preg_user data)
		{
			try
			{
				if (data != null && data.password == null)
				{
					IEnumerable<preg_user> results = dao.GetUsersByParams(data);
					if (results.Count() > 0)
					{
						foreach (var result in results)
						{
							result.password = null;
						}
						return Request.CreateResponse(HttpStatusCode.OK, results);
					}
					else
					{
						HttpError err = new HttpError(SysConst.DATA_NOT_FOUND);
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
					}
				}
				else
				{
					IEnumerable<preg_user> result = dao.GetListUser();
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
		[Route("api/users/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_user data = dao.GetUserByID(Convert.ToInt32(id));
				if (data != null)
				{
					data.password = null;
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
		[AllowAnonymous]
		public HttpResponseMessage Post([FromBody]preg_user data)
		{
			try
			{
				if (data.email != null && data.password != null)
				{

					data.password = SysMethod.MD5Hash(data.password);
					if (dao.InsertData(data))
					{
						preg_user createdUser = dao.GetUsersByParams(data).FirstOrDefault();
						createdUser.password = null;
						return Request.CreateResponse(HttpStatusCode.Created, createdUser);
					}
					else
					{
						HttpError err = new HttpError(SysConst.EMAIL_EXIST);
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
					}
				}
				else
				{
					HttpError err = new HttpError(SysConst.EMAIL_PASSWORD_NOT_NULL);
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
		[Route("api/users/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_user dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/users/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				dao.DeleteData(Convert.ToInt32(id));
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(String.Format(SysConst.ITEM_ID_NOT_EXIST, id));
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string id, [FromBody]preg_user dataUpdate)
		{
			try
			{
				if (dataUpdate != null)
				{
					preg_user user = new preg_user();
					user = dao.GetUserByID(Convert.ToInt32(id));
					if (user == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.password != null)
					{
						user.password = SysMethod.MD5Hash(dataUpdate.password);
					}
					if (dataUpdate.phone != null)
					{
						user.phone = dataUpdate.phone;
					}
					if (dataUpdate.social_type_id != null)
					{
						user.social_type_id = dataUpdate.social_type_id;
					}
					if (dataUpdate.first_name != null)
					{
						user.first_name = dataUpdate.first_name;
					}
					if (dataUpdate.last_name != null)
					{
						user.last_name = dataUpdate.last_name;
					}
					if (dataUpdate.you_are_the != null)
					{
						user.you_are_the = dataUpdate.you_are_the;
					}
					if (dataUpdate.location != null)
					{
						user.location = dataUpdate.location;
					}
					if (dataUpdate.status != null)
					{
						user.status = dataUpdate.status;
					}
					if (dataUpdate.avarta != null)
					{
						user.avarta = dataUpdate.avarta;
					}

					dao.UpdateData(user);
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
