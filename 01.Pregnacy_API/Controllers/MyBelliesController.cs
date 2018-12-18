using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using PregnancyData.Entity;
using System.IO;
using System.Security.Claims;
using PregnancyData.Dao;
using System.Threading.Tasks;

namespace _01.Pregnacy_API.Controllers
{
	public class MyBelliesController : ApiController
	{
		MyBellyDao dao = new MyBellyDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromBody]preg_my_belly data)
		{
			try
			{
				IEnumerable<preg_my_belly> result;
				if (data != null)
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

		// GET api/values/5
		[Authorize]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_my_belly data = dao.GetItemByID(Convert.ToInt32(id));
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
		public HttpResponseMessage Post([FromBody]preg_my_belly data)
		{
			try
			{
				if (data != null)
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
		public HttpResponseMessage Put(string id, [FromBody]preg_my_belly dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Delete(string id)
		{
			//lstStrings[id] = value;
			try
			{
				preg_my_belly item = dao.GetItemByID(Convert.ToInt32(id));
				if (item == null)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
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

		public HttpResponseMessage UpdateData(string id, preg_my_belly dataUpdate)
		{
			//lstStrings[id] = value;
			try
			{
				if (dataUpdate != null)
				{
					preg_my_belly phone = new preg_my_belly();
					phone = dao.GetItemByID(Convert.ToInt32(id));
					if (phone == null)
					{
						return Request.CreateResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.image != null)
					{
						phone.image = dataUpdate.image;
					}
					if (dataUpdate.my_belly_type_id != null)
					{
						phone.my_belly_type_id = dataUpdate.my_belly_type_id;
					}
					if (dataUpdate.month != null)
					{
						phone.month = dataUpdate.month;
					}
					if (dataUpdate.user_id != null)
					{
						phone.user_id = dataUpdate.user_id;
					}

					dao.UpdateData(phone);
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
		[Route("api/mybellies/{month}/upload")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload(int month)
		{
			// Get current user_id
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			string dir = "~/Files/Uploads/Users/" + user_id.ToString() + "/MyBellies/" + month.ToString();
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
				preg_my_belly updateRow = new preg_my_belly();
				updateRow.month = month;
				updateRow.user_id = user_id;
				if (dao.GetItemsByParams(updateRow).Count() == 0)
				{
					dao.InsertData(updateRow);
				}
				updateRow = dao.GetItemsByParams(updateRow).FirstOrDefault();
				foreach (MultipartFileData file in provider.FileData)
				{
					string path = dir + "/" + Path.GetFileName(file.LocalFileName);
					files.Add(path);
					updateRow.image = path;
				}
				UpdateData(updateRow.id.ToString(), updateRow);
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
