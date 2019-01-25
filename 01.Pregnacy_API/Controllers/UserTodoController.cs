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
	public class UserTodoController : ApiController
	{
		UserTodoDao dao = new UserTodoDao();
		// GET api/values
		[Authorize(Roles = "dev, admin")]
		[HttpGet]
		public HttpResponseMessage Get([FromUri]preg_user_todo data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_user_todo()))
				{
					data.user_id = user_id;
					IEnumerable<preg_user_todo> result = dao.GetItemByParams(data);
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
					IEnumerable<preg_user_todo> result = dao.GetListItem().Where(c => c.user_id == user_id); ;
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
		[Route("api/usertodo/{todo_id}")]
		public HttpResponseMessage Get(string todo_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_user_todo data = dao.GetItemByID(user_id, Convert.ToInt32(todo_id)).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_user_todo data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (data.todo_id != 0)
				{
					//Check exist
					preg_user_todo checkExist = dao.GetItemByParams(new preg_user_todo() { user_id = user_id, todo_id = data.todo_id }).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
					}

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

		// PUT api/values/5
		[Authorize(Roles = "dev, admin")]
		[HttpPut]
		[Route("api/usertodo/{todo_id}")]
		public HttpResponseMessage Put(string todo_id, [FromBody]preg_user_todo dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_user_todo()))
				{
					preg_user_todo user = new preg_user_todo();
					user = dao.GetItemByID(user_id, Convert.ToInt32(todo_id)).FirstOrDefault();
					if (user == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}

					if (dataUpdate.status != null)
					{
						user.status = dataUpdate.status;
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

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[HttpDelete]
		[Route("api/usertodo/{todo_id}")]
		public HttpResponseMessage Delete(string todo_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_user_todo item = dao.GetItemByID(user_id, Convert.ToInt32(todo_id)).FirstOrDefault();
				if (item == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}

				dao.DeleteData(item);
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