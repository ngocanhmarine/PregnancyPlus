using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PregnancyData.Entity;
using PregnancyData.Dao;
using System.Security.Claims;
using System.Linq;

namespace _01.Pregnacy_API.Controllers
{
	public class UserProfileController : ApiController
	{
		UserDao userdao = new UserDao();
		PregnancyEntity connect = null;
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
					IQueryable<preg_user> result = userdao.GetUserByID(user_id);
					return Request.CreateResponse(HttpStatusCode.OK, userdao.FilterJoin(result, user_id));
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

		// PUT api/values/5
		[Authorize(Roles = "dev, admin")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody]preg_user dataUpdate)
		{
			return UpdateData(dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[HttpDelete]
		public HttpResponseMessage Delete()
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (user_id > 0)
				{
					userdao.DeleteData(Convert.ToInt32(user_id));
					return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
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
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData([FromBody]preg_user dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (user_id > 0)
				{
					if (dataUpdate != null && dataUpdate.email == null)
					{
						preg_user user = new preg_user();
						user = userdao.GetUserByID(user_id).ToList()[0];
						if (user != null)
						{
							return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
						}
						if (dataUpdate.password != null)
						{
							user.password = SysMethod.MD5Hash(dataUpdate.password);
						}
						if (dataUpdate.email != null)
						{
							user.email = dataUpdate.email;
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

						userdao.UpdateData(user);
						return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_UPDATE_SUCCESS);
					}
					else
					{
						HttpError err = new HttpError(SysConst.DATA_NOT_EMPTY);
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
					}
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
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

	}
}
