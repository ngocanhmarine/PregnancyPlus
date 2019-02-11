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

	public class WeeklyInteractController : ApiController
	{
		WeeklyInteractDao dao = new WeeklyInteractDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_weekly_interact data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_weekly_interact()))
				{
					data.user_id = user_id;
					IQueryable<preg_weekly_interact> result = dao.GetItemsByParams(data);
					if (result.Any())
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
					IQueryable<preg_weekly_interact> result = dao.GetListItem().Where(c => c.user_id == user_id);
					if (result.Any())
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
		public HttpResponseMessage Post([FromBody]preg_weekly_interact data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (data.week_id != 0)
				{
					//Check exist
					preg_weekly_interact checkExist = dao.GetItemsByParams(new preg_weekly_interact() { user_id = user_id, week_id = data.week_id }).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
					}

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
		[Route("api/weeklyinteract/{week_id}")]
		public HttpResponseMessage Put(string week_id, [FromBody]preg_weekly_interact dataUpdate)
		{
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			return UpdateData(week_id, user_id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/weeklyinteract/{week_id}")]
		public HttpResponseMessage Delete(string week_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_weekly_interact item = dao.GetItemByID(Convert.ToInt32(week_id), user_id).FirstOrDefault();
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

		public HttpResponseMessage UpdateData(string week_id, int user_id, [FromBody]preg_weekly_interact dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_weekly_interact()))
				{
					preg_weekly_interact weekly_interact = new preg_weekly_interact();
					weekly_interact = dao.GetItemByID(Convert.ToInt32(week_id), user_id).FirstOrDefault();

					if (weekly_interact == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.like != null)
					{
						weekly_interact.like = dataUpdate.like;
					}
					if (dataUpdate.comment != null)
					{
						weekly_interact.comment = dataUpdate.comment;
					}
					if (dataUpdate.photo != null)
					{
						weekly_interact.photo = dataUpdate.photo;
					}
					if (dataUpdate.share != null)
					{
						weekly_interact.share = dataUpdate.share;
					}
					if (dataUpdate.notification != null)
					{
						weekly_interact.notification = dataUpdate.notification;
					}
					if (dataUpdate.status != null)
					{
						weekly_interact.status = dataUpdate.status;
					}

					dao.UpdateData(weekly_interact);
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

		#region Upload files
		[Authorize]
		[Route("api/weeklyinteract/{week_id}/upload")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload(string week_id)
		{
			// Get current user_id
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			// Check preg_weekly_interact exist
			preg_weekly_interact checkItem = dao.GetItemByID(Convert.ToInt32(week_id), Convert.ToInt32(user_id)).FirstOrDefault();
			if (checkItem == null)
			{
				preg_weekly_interact insertItem = new preg_weekly_interact() { week_id = Convert.ToInt32(week_id), user_id = Convert.ToInt32(user_id) };
				dao.InsertData(insertItem);
			}

			string dir = "/Files/WeeklyInteract/" + week_id.ToString() + "/" + user_id.ToString();
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
			// Check if image and html filetype
			for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
			{
				HttpPostedFile file = HttpContext.Current.Request.Files[i];
				if (!SysConst.imgOnlyExtensions.Any(x => x.Equals(Path.GetExtension(file.FileName.ToLower()), StringComparison.OrdinalIgnoreCase)))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.INVALID_FILE_TYPE);
				}
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
				preg_weekly_interact updateRow = new preg_weekly_interact();
				foreach (MultipartFileData file in provider.FileData)
				{
					string path = dir + "/" + HttpUtility.UrlPathEncode(Path.GetFileName(file.LocalFileName));
					files.Add(path);
					updateRow.photo = path;
				}
				UpdateData(week_id, user_id, updateRow);

				return Request.CreateResponse(HttpStatusCode.Created, files);
			}
			catch (System.Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
			}
		}
		#endregion
	}
}
