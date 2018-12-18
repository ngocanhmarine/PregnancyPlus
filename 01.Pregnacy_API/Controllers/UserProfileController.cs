using System;
using System.Web;
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
using PregnancyData.Dao;

using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;

namespace _01.Pregnacy_API.Controllers
{
	public class UserProfileController : ApiController
	{
		UserDao userdao = new UserDao();
		// GET api/values
		[Authorize]
		[HttpGet]
		public HttpResponseMessage Get()
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (user_id > 0)
				{
					preg_user result = userdao.GetUserByID(user_id);
					return Request.CreateResponse(HttpStatusCode.OK, result);
				}
				else
				{
					HttpError err = new HttpError(SysConst.ADMIN_DONT_HAVE_PROFILE);
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
				}
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
			}
		}

		//[Authorize]
		//[HttpGet]
		//[Route("api/userprofile/question")]
		//public HttpResponseMessage GetUserProfileQuestion()
		//{
		//	try
		//	{
		//		int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
		//		if (user_id > 0)
		//		{
		//			using (PregnancyEntity connectQuestion = new PregnancyEntity())
		//			{
		//				var result = (from a in connectQuestion.preg_answer
		//							  join q in connectQuestion.preg_question on a.question_id equals q.id
		//							  where a.user_id == user_id
		//							  select new { a, q }).ToList();

		//				return Request.CreateResponse(HttpStatusCode.OK, result);
		//			}

		//		}
		//		else
		//		{
		//			HttpError err = new HttpError(SysConst.ADMIN_DONT_HAVE_PROFILE);
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HttpError err = new HttpError(ex.Message);
		//		return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
		//	}
		//}


		// POST api/values
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Post([FromBody]preg_user data)
		{
			try
			{
				if (data.phone != null && data.password != null)
				{
					data.password = SysMethod.MD5Hash(data.password);
					if (userdao.InsertData(data))
					{
						return Request.CreateResponse(HttpStatusCode.Created, SysConst.DATA_INSERT_SUCCESS);
					}
					else
					{
						HttpError err = new HttpError(SysConst.PHONE_EXIST);
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
					}
				}
				else
				{
					HttpError err = new HttpError(SysConst.PHONE_PASSWORD_NOT_NULL);
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
		public HttpResponseMessage Put(string id, [FromBody]preg_user dataUpdate)
		{
			//lstStrings[id] = value;
			try
			{
				if (dataUpdate != null)
				{
					preg_user user = new preg_user();
					user = userdao.GetUserByID(Convert.ToInt32(id));
					user.password = SysMethod.MD5Hash(dataUpdate.password);
					user.phone = dataUpdate.phone;
					user.social_type_id = dataUpdate.social_type_id;
					user.first_name = dataUpdate.first_name;
					user.last_name = dataUpdate.last_name;
					user.you_are_the = dataUpdate.you_are_the;
					user.location = dataUpdate.location;
					user.status = dataUpdate.status;
					user.avarta = dataUpdate.avarta;
					user.email = dataUpdate.email;

					userdao.UpdateData(user);
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
		public HttpResponseMessage Delete(string id)
		{
			//lstStrings[id] = value;
			try
			{
				userdao.DeleteData(Convert.ToInt32(id));
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
