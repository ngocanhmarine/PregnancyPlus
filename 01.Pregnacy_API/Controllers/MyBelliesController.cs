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
	public class MyBelliesController : ApiController
	{
		MyBellyDao dao = new MyBellyDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_my_belly data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_my_belly> result;
				if (!data.DeepEquals(new preg_my_belly()))
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

		// GET api/values/5
		[Authorize]
		[Route("api/mybellies/{month}")]
		public HttpResponseMessage Get(string month)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_my_belly> data = dao.GetItemsByParams(new preg_my_belly() { user_id = user_id, month = Convert.ToInt32(month) });
				if (data.Count() > 0)
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

		// GET api/values/5
		[Authorize]
		[HttpGet]
		[Route("api/mybellies/template")]
		public HttpResponseMessage GetTemplates()
		{
			try
			{
				IEnumerable<preg_my_belly> data = dao.GetListItem().Where(c => c.user_id == null);
				if (data.Count() > 0)
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
		// GET api/values/5
		[Authorize]
		[HttpGet]
		[Route("api/mybellies/template/{month}")]
		public HttpResponseMessage GetTemplate(string month)
		{
			try
			{
				IEnumerable<preg_my_belly> data = dao.GetItemsByParams(new preg_my_belly() { month = Convert.ToInt32(month) }).Where(c => c.user_id == null);
				if (data.Count() > 0)
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
		public HttpResponseMessage Post([FromBody]preg_my_belly data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (data.month != null)
				{
					data.user_id = user_id;
					// Check if exist
					preg_my_belly checkExist = dao.GetItemsByParams(new preg_my_belly() { user_id = user_id, month = data.month }).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
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
		[Route("api/mybellies/{month}")]
		public HttpResponseMessage Put(string month, [FromBody]preg_my_belly dataUpdate)
		{
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			return UpdateData(user_id, month, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/mybellies/{month}")]
		public HttpResponseMessage Delete(int month)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_my_belly item = dao.GetItemsByParams(new preg_my_belly { user_id = user_id, month = month }).FirstOrDefault();
				if (item != null)
				{
					dao.DeleteData(item);
					return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
				}
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public HttpResponseMessage UpdateData(int user_id, string month, preg_my_belly dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_my_belly()))
				{
					preg_my_belly my_belly = new preg_my_belly() { user_id = user_id, month = Convert.ToInt32(month) };
					my_belly = dao.GetItemsByParams(my_belly).FirstOrDefault();
					if (my_belly == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.image != null)
					{
						my_belly.image = dataUpdate.image;
					}

					dao.UpdateData(my_belly);
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
		[Route("api/mybellies/{month}")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload(string month)
		{
			// Get current user_id
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);

			string dir = "/Files/Uploads/Users/" + user_id.ToString() + "/MyBellies/" + month;
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
				preg_my_belly updateRow = new preg_my_belly();
				updateRow.month = Convert.ToInt32(month);
				updateRow.user_id = user_id;
				if (dao.GetItemsByParams(updateRow).Count() == 0)
				{
					dao.InsertData(updateRow);
				}
				updateRow = dao.GetItemsByParams(updateRow).FirstOrDefault();
				foreach (MultipartFileData file in provider.FileData)
				{
					string path = dir + "/" + HttpUtility.UrlPathEncode(Path.GetFileName(file.LocalFileName));
					files.Add(path);
					updateRow.image = path;
				}
				UpdateData(user_id, month, updateRow);
				return Request.CreateResponse(HttpStatusCode.Created, files);
			}
			catch (System.Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
			}
		}

		[Authorize]
		[Route("api/mybellies/{month}/template")]
		[HttpPost]
		public async Task<HttpResponseMessage> UploadRoot(int month)
		{
			string dir = "/Files/MyBellies/" + month.ToString();
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
				preg_my_belly updateRow = new preg_my_belly();
				updateRow.month = month;
				updateRow.user_id = null;
				using (PregnancyEntity connect = new PregnancyEntity())
				{
					preg_my_belly chkRowExist = connect.preg_my_belly.Where(c => c.month == month).FirstOrDefault();
					if (chkRowExist == null)
					{
						dao.InsertData(updateRow);
					}
				}

				updateRow = dao.GetItemsByParams(updateRow).FirstOrDefault();
				foreach (MultipartFileData file in provider.FileData)
				{
					string path = dir + "/" + HttpUtility.UrlPathEncode(Path.GetFileName(file.LocalFileName));
					files.Add(path);
					updateRow.image = path;
				}
				dao.UpdateData(updateRow);
				//UpdateData(updateRow.id, updateRow.month.ToString(), updateRow);
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
