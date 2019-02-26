using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{
	public class UserProfileController : ApiController
	{
		UserDao userdao = new UserDao();
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
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			return UpdateData(user_id.ToString(), dataUpdate);
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
					if (!DeleteReferenceData(user_id))
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_DELETE_FAIL);
					}
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

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/userprofile/reset")]
		[HttpDelete]
		public HttpResponseMessage Reset()
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!DeleteReferenceData(user_id))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_RESET_FAIL);
				}

				//preg_user item = userdao.GetUserByID(user_id).FirstOrDefault();
				//item.you_are_the = null;
				//item.location = null;
				//item.status = null;
				//userdao.UpdateData(item);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_RESET_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string user_id, [FromBody]preg_user dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_user()))
				{
					preg_user user = new preg_user();
					user = userdao.GetUserByID(Convert.ToInt32(user_id)).FirstOrDefault();
					if (user == null)
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
					if (dataUpdate.avatar != null)
					{
						user.avatar = dataUpdate.avatar;
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
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public bool DeleteReferenceData(int user_id)
		{
			try
			{
				PregnancyEntity connect = new PregnancyEntity();
				preg_user user = connect.preg_user.Where(c => c.id == user_id).FirstOrDefault();

				while (user.preg_answer.Count() > 0)
				{
					connect.preg_answer.Remove(user.preg_answer.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_appointment.Count() > 0)
				{
					preg_appointment appointment = user.preg_appointment.FirstOrDefault();
					while (appointment.preg_appointment_measurement.Count() > 0)
					{
						connect.preg_appointment_measurement.Remove(appointment.preg_appointment_measurement.FirstOrDefault());
						connect.SaveChanges();
					}
					connect.preg_appointment.Remove(user.preg_appointment.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_auth.Count() > 0)
				{
					connect.preg_auth.Remove(user.preg_auth.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_contact_us.Count() > 0)
				{
					connect.preg_contact_us.Remove(user.preg_contact_us.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_contraction.Count() > 0)
				{
					connect.preg_contraction.Remove(user.preg_contraction.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_customer_response.Count() > 0)
				{
					connect.preg_customer_response.Remove(user.preg_customer_response.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_customer_response1.Count() > 0)
				{
					connect.preg_customer_response.Remove(user.preg_customer_response1.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_daily_interact.Count() > 0)
				{
					connect.preg_daily_interact.Remove(user.preg_daily_interact.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_my_birth_plan.Count() > 0)
				{
					connect.preg_my_birth_plan.Remove(user.preg_my_birth_plan.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_my_birth_plan_item.Count() > 0)
				{
					connect.preg_my_birth_plan_item.Remove(user.preg_my_birth_plan_item.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_my_weight.Count() > 0)
				{
					connect.preg_my_weight.Remove(user.preg_my_weight.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_phone.Count() > 0)
				{
					connect.preg_phone.Remove(user.preg_phone.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_pregnancy.Count() > 0)
				{
					connect.preg_pregnancy.Remove(user.preg_pregnancy.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_profession.Count() > 0)
				{
					connect.preg_profession.Remove(user.preg_profession.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_question.Count() > 0)
				{
					connect.preg_question.Remove(user.preg_question.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_setting.Count() > 0)
				{
					connect.preg_setting.Remove(user.preg_setting.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_upgrade.Count() > 0)
				{
					connect.preg_upgrade.Remove(user.preg_upgrade.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_weekly_interact.Count() > 0)
				{
					connect.preg_weekly_interact.Remove(user.preg_weekly_interact.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_user_baby_name.Count() > 0)
				{
					connect.preg_user_baby_name.Remove(user.preg_user_baby_name.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_user_hospital_bag_item.Count() > 0)
				{
					connect.preg_user_hospital_bag_item.Remove(user.preg_user_hospital_bag_item.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_hospital_bag_item.Count() > 0)
				{
					connect.preg_hospital_bag_item.Remove(user.preg_hospital_bag_item.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_my_belly.Count() > 0)
				{
					connect.preg_my_belly.Remove(user.preg_my_belly.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_user_kick_history.Count() > 0)
				{
					connect.preg_user_kick_history.Remove(user.preg_user_kick_history.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_user_medical_service_package.Count() > 0)
				{
					connect.preg_user_medical_service_package.Remove(user.preg_user_medical_service_package.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_user_shopping_cart.Count() > 0)
				{
					connect.preg_user_shopping_cart.Remove(user.preg_user_shopping_cart.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_shopping_item.Count() > 0)
				{
					connect.preg_shopping_item.Remove(user.preg_shopping_item.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_user_todo.Count() > 0)
				{
					connect.preg_user_todo.Remove(user.preg_user_todo.FirstOrDefault());
					connect.SaveChanges();
				}
				while (user.preg_todo.Count() > 0)
				{
					connect.preg_todo.Remove(user.preg_todo.FirstOrDefault());
					connect.SaveChanges();
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		#region Upload avarta
		[Authorize]
		[Route("api/userprofile/upload")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload()
		{
			try
			{
				// Get current user_id
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);

				string dir = "/Files/Upload/Users/" + user_id.ToString() + "/Avatar/";
				string dirRoot = HttpContext.Current.Server.MapPath(dir);
				// Check if request contains multipart/form-data
				if (!Request.Content.IsMimeMultipartContent())
				{
					throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
				}
				// Check if directory folder created
				if (!Directory.Exists(dirRoot))
				{
					Directory.CreateDirectory(dirRoot);
				}
				// Check if image filetype
				for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
				{
					HttpPostedFile file = HttpContext.Current.Request.Files[i];
					if (!SysConst.imgOnlyExtensions.Any(x => x.Equals(Path.GetExtension(file.FileName.ToLower()), StringComparison.OrdinalIgnoreCase)))
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.INVALID_FILE_TYPE);
					}
					// Check if exist file
					else if (File.Exists(dirRoot + "/" + file.FileName))
					{
						File.Delete(dirRoot + "/" + file.FileName);
					}
				}

				CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(dirRoot);

				List<string> files = new List<string>();

				try
				{
					// Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
					await Request.Content.ReadAsMultipartAsync(provider);

					// Update to database
					preg_user updateRow = new preg_user();
					foreach (MultipartFileData file in provider.FileData)
					{
						string path = dir + "/" + HttpUtility.UrlPathEncode(Path.GetFileName(file.LocalFileName));
						files.Add(path);
						updateRow.avatar = path;
					}
					UpdateData(user_id.ToString(), updateRow);

					return Request.CreateResponse(HttpStatusCode.Created, files);
				}
				catch (System.Exception ex)
				{
					return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
			}
		}

		#endregion

	}
}
