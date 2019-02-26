using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace _01.Pregnacy_API.Controllers
{
	public class UsersController : ApiController
	{
		UserDao dao = new UserDao();
		// GET api/users
		[Authorize(Roles = "dev, admin")]
		[HttpGet]
		public HttpResponseMessage Get([FromUri]preg_user data)
		{
			try
			{
				if (!data.DeepEquals(new preg_user()) && data.password == null)
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
					IEnumerable<preg_user> results = dao.GetListUser();
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
		[HttpGet]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_user data = dao.GetUserByID(Convert.ToInt32(id)).FirstOrDefault();
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

		// GET api/values/5
		[AllowAnonymous]
		[Route("api/users/checkphone")]
		[HttpGet]
		public HttpResponseMessage CheckPhone([FromUri]preg_user data)
		{
			try
			{
				if (data.phone != null)
				{
					preg_user dataCheck = new preg_user() { phone = data.phone };
					dataCheck = dao.GetUsersByParams(dataCheck).FirstOrDefault();
					if (dataCheck == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					return Request.CreateResponse(HttpStatusCode.OK, SysConst.DATA_EXIST);
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
		[HttpPost]
		public HttpResponseMessage Post([FromBody]preg_user data)
		{
			try
			{
				if (data.phone != null && data.password != null)
				{
					if (data.password.Length >= 6)
					{
						data.password = SysMethod.MD5Hash(data.password);
						data.time_created = DateTime.Now;
						data.social_type_id = null;
						data.uid = null;
						if (dao.InsertData(data))
						{
							SysMethod.createAccountNop(data);
							dao.UpdateData(data);
							data.password = null;
							return Request.CreateResponse(HttpStatusCode.Created, data);
						}
						else
						{
							HttpError err = new HttpError(SysConst.PHONE_EXIST);
							return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
						}
					}
					else
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.PASSWORD_LENGTH);
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

		// POST api/values
		[AllowAnonymous]
		[HttpPost]
		[Route("api/users/createguestaccount")]
		public HttpResponseMessage CreateGuestAccount()
		{
			try
			{
				preg_user newGuestAccount = new preg_user();
				preg_user checkExist = new preg_user();
				while (checkExist != null)
				{
					newGuestAccount = RandomGuestAccount();
					//Check account exist
					checkExist = dao.GetListUser().Where(c => c.phone == newGuestAccount.phone).FirstOrDefault();
				}
				string password = newGuestAccount.password;
				newGuestAccount.password = SysMethod.MD5Hash(newGuestAccount.password);
				newGuestAccount.time_created = DateTime.Now;
				newGuestAccount.social_type_id = null;
				newGuestAccount.uid = null;
				if (dao.InsertData(newGuestAccount))
				{
					SysMethod.createAccountNop(newGuestAccount);
					return Request.CreateResponse(HttpStatusCode.Created, new { newGuestAccount.phone, password });
				}
				else
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_INSERT_FAIL);
				}
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		// PUT api/values/5
		[AllowAnonymous]
		[Route("api/users/forgotpassword/{phone}")]
		[HttpPut]
		public HttpResponseMessage ForgotPassword(string phone, [FromBody]preg_user passwordUpdate)
		{
			try
			{
				preg_user user = new preg_user() { phone = phone };
				user = dao.GetUsersByParams(user).FirstOrDefault();
				if (user == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}
				if (passwordUpdate.password.Length < 6)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.PASSWORD_LENGTH);
				}

				string strPass = passwordUpdate.password;

				user.password = SysMethod.MD5Hash(strPass);

				dao.UpdateData(user);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_UPDATE_SUCCESS);
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
		[HttpPut]
		public HttpResponseMessage Put(string id, [FromBody]preg_user dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/users/{id}")]
		[HttpDelete]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				if (!DeleteReferenceData(Convert.ToInt32(id)))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_DELETE_FAIL);
				}

				dao.DeleteData(Convert.ToInt32(id));
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(String.Format(SysConst.ITEM_ID_NOT_EXIST, id));
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		// DELETE api/values/5
		[Authorize(Roles = "admin")]
		[Route("api/users/all")]
		[HttpDelete]
		public HttpResponseMessage DeleteAll()
		{
			try
			{
				PregnancyEntity connect = new PregnancyEntity();
				IEnumerable<preg_user> users = connect.preg_user;
				bool chkFlag = true;
				List<int> listID = new List<int>();
				foreach (preg_user user in users)
				{
					int id = user.id;
					listID.Add(id);
				}
				foreach (int id in listID)
				{
					if (id == 4 || id == 409 || id == 130)
					{
						continue;
					}
					if (!DeleteReferenceData(Convert.ToInt32(id)))
					{
						chkFlag = false;
					}

					dao.DeleteData(Convert.ToInt32(id));
				}
				if (!chkFlag)
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_DELETE_FAIL);
				}
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_DELETE_FAIL);
			}
		}


		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/users/{id}/reset")]
		[HttpDelete]
		public HttpResponseMessage Reset(string id)
		{
			try
			{
				if (!DeleteReferenceData(Convert.ToInt32(id)))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_RESET_FAIL);
				}

				//preg_user item = dao.GetUserByID(Convert.ToInt32(id)).FirstOrDefault();
				//item.you_are_the = null;
				//item.location = null;
				//item.status = null;
				//dao.UpdateData(item);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_RESET_SUCCESS);
			}
			catch (Exception)
			{
				HttpError err = new HttpError(String.Format(SysConst.ITEM_ID_NOT_EXIST, id));
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string id, [FromBody]preg_user dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_user()))
				{
					preg_user user = new preg_user();
					user = dao.GetUserByID(Convert.ToInt32(id)).FirstOrDefault();
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

		protected string RandomString(int length)
		{
			var chars = "0123456789";
			var chars2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[length];
			var random = new Random();
			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}
			return new String(stringChars);
		}
		protected preg_user RandomGuestAccount()
		{
			var chars = "0123456789";
			var chars2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringUsername = new char[8];
			var random = new Random();
			for (int i = 0; i < stringUsername.Length; i++)
			{
				stringUsername[i] = chars[random.Next(chars.Length)];
			}
			string username = "guest" + new string(stringUsername);
			var stringPassword = new char[8];
			var random2 = new Random();
			for (int i = 0; i < stringPassword.Length; i++)
			{
				stringPassword[i] = chars2[random.Next(chars2.Length)];
			}
			string password = new string(stringPassword);
			return new preg_user() { phone = username, password = password };
		}
	}
}