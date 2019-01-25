using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{
	public class AuthsController : ApiController
	{
		AuthDao dao = new AuthDao();
		// GET api/values
		[Authorize(Roles = "dev, admin")]
		public async Task<HttpResponseMessage> Get([FromUri]preg_auth data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_auth> result;
				if (!data.DeepEquals(new preg_auth()))
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


		// GET api/values
		[Authorize(Roles = "admin")]
		[Route("api/auths/getall")]
		public async Task<HttpResponseMessage> GetAll([FromUri]preg_auth data)
		{
			try
			{
				IEnumerable<preg_auth> result;
				if (!data.DeepEquals(new preg_auth()))
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

		// POST api/values
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Post([FromBody]preg_auth data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_auth()))
				{
					//Check exist
					preg_auth checkExist = dao.GetItemsByParams(new preg_auth() { user_id = user_id }).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
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
		[Route("api/auths/update")]
		public HttpResponseMessage Put([FromBody]preg_auth dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_auth()))
				{
					preg_auth auth = new preg_auth();
					auth = dao.GetListItem().Where(c => c.user_id == user_id).FirstOrDefault();
					if (auth == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.token != null)
					{
						auth.token = dataUpdate.token;
					}
					if (dataUpdate.valid_to != null)
					{
						auth.valid_to = dataUpdate.valid_to;
					}

					dao.UpdateData(auth);
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
		[Authorize(Roles = "dev, admin")]
		[Route("api/auths/delete")]
		public HttpResponseMessage Delete()
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_auth auth = dao.GetListItem().Where(c => c.user_id == user_id).FirstOrDefault();
				if (auth == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}

				dao.DeleteData(auth);
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