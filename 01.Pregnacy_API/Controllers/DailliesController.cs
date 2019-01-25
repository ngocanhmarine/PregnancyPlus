using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IO;

namespace _01.Pregnacy_API.Controllers
{
	public class DailliesController : ApiController
	{
		DailyDao dao = new DailyDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_daily data)
		{
			try
			{
				IQueryable<preg_daily> result;
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_daily()))
				{
					result = dao.GetItemsByParams(data);
				}
				else
				{
					result = dao.GetListItem();
				}
				if (result.Count() > 0)
				{
					return Request.CreateResponse(HttpStatusCode.OK, dao.FilterJoin(result, user_id));
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
		[Route("api/daillies/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_daily> data = dao.GetItemByID(Convert.ToInt32(id));
				if (data != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK,dao.FilterJoin( data,user_id));
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
		public HttpResponseMessage Post([FromBody]preg_daily data)
		{
			try
			{
				if (!data.DeepEquals(new preg_daily()))
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
		[Route("api/daillies/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_daily dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/daillies/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_daily daily = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
				if (daily == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
				}
				dao.DeleteData(daily);
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		protected HttpResponseMessage UpdateData(string id, preg_daily dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_daily()))
				{
					preg_daily daily = new preg_daily();
					daily = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (daily == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.title != null)
					{
						daily.title = dataUpdate.title;
					}
					if (dataUpdate.highline_image != null)
					{
						daily.highline_image = dataUpdate.highline_image;
					}
					if (dataUpdate.short_description != null)
					{
						daily.short_description = dataUpdate.short_description;
					}
					if (dataUpdate.description != null)
					{
						daily.description = dataUpdate.description;
					}
					if (dataUpdate.daily_blog != null)
					{
						daily.daily_blog = dataUpdate.daily_blog;
					}

					dao.UpdateData(daily);
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
		[Route("api/daillies/{daily_id}/upload")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload(string daily_id)
		{
			// Check daily_id exist
			preg_daily checkItem = dao.GetItemByID(Convert.ToInt32(daily_id)).FirstOrDefault();
			if (checkItem == null)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format(SysConst.ITEM_ID_NOT_EXIST, daily_id));
			}
			// Get current user_id
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			string dir = "/Files/Daillies/" + daily_id.ToString();
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
				if (!SysConst.imgHtmlExtensions.Any(x => x.Equals(Path.GetExtension(file.FileName.ToLower()), StringComparison.OrdinalIgnoreCase)))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.INVALID_FILE_TYPE);
				}
				else if (File.Exists(dirRoot + "/" + file.FileName))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format(SysConst.FILE_EXIST, file.FileName));
				}
			}

			CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(dirRoot);

			List<string> files = new List<string>();

			try
			{
				// Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
				await Request.Content.ReadAsMultipartAsync(provider);

				// Update to database
				preg_daily updateRow = new preg_daily();
				foreach (MultipartFileData file in provider.FileData)
				{
					string path = dir + "/" + HttpUtility.UrlPathEncode(Path.GetFileName(file.LocalFileName));
					files.Add(path);
					if (Path.GetExtension(file.LocalFileName).ToLower().Equals(".html"))
					{
						updateRow.description = path;
					}
					else
					{
						updateRow.highline_image = path;
					}
				}
				UpdateData(daily_id, updateRow);

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