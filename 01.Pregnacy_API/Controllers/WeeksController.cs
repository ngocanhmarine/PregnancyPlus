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

	public class WeeksController : ApiController
	{
		WeekDao dao = new WeekDao();
		[HttpGet]
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_week data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_week()))
				{
					IQueryable<preg_week> result = dao.GetItemsByParams(data);
					if (result.Any())
					{
						return Request.CreateResponse(HttpStatusCode.OK, dao.FilterJoin(result, user_id));
					}
					else
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
				}
				else
				{
					IQueryable<preg_week> result = dao.GetListItem();
					if (result.Any())
					{
						return Request.CreateResponse(HttpStatusCode.OK, dao.FilterJoin(result, user_id));
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

		[Authorize]
		[Route("api/weeks/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_week> data = dao.GetItemByID(Convert.ToInt32(id));
				if (data.Any())
				{
					return Request.CreateResponse(HttpStatusCode.OK, dao.FilterJoin(data, user_id));
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
		[HttpPost]
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Post([FromBody]preg_week data)
		{
			try
			{
				if (!data.DeepEquals(new preg_week()))
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
		[HttpPut]
		[Authorize(Roles = "dev, admin")]
		[Route("api/weeks/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_week dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[HttpDelete]
		[Authorize(Roles = "dev, admin")]
		[Route("api/weeks/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				dao.DeleteData(Convert.ToInt32(id));
				return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(string id, [FromBody]preg_week dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_week()))
				{
					preg_week week = new preg_week();
					week = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (week == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.length != null)
					{
						week.length = dataUpdate.length;
					}
					if (dataUpdate.weight != null)
					{
						week.weight = dataUpdate.weight;
					}
					if (dataUpdate.title != null)
					{
						week.title = dataUpdate.title;
					}
					if (dataUpdate.highline_image != null)
					{
						week.highline_image = dataUpdate.highline_image;
					}
					if (dataUpdate.short_description != null)
					{
						week.short_description = dataUpdate.short_description;
					}
					if (dataUpdate.description != null)
					{
						week.description = dataUpdate.description;
					}
					if (dataUpdate.daily_relation != null)
					{
						week.daily_relation = dataUpdate.daily_relation;
					}
					if (dataUpdate.meta_description != null)
					{
						week.meta_description = dataUpdate.meta_description;
					}

					dao.UpdateData(week);
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
		[Route("api/weeks/{id}/upload")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload(string id)
		{
			// Check weekly_id exist
			preg_week checkItem = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
			if (checkItem == null)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format(SysConst.ITEM_ID_NOT_EXIST, id));
			}

			// Get current user_id
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			string dir = "/Files/Weekly/" + id.ToString();
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
				preg_week updateRow = new preg_week();
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
				UpdateData(id, updateRow);

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
