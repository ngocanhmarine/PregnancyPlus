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

	public class SizeGuidesController : ApiController
	{
		SizeGuideDao dao = new SizeGuideDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_size_guide data)
		{
			try
			{
				if (!data.DeepEquals(new preg_size_guide()))
				{
					IEnumerable<preg_size_guide> result = dao.GetItemsByParams(data);
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
					IEnumerable<preg_size_guide> result = dao.GetListItem();
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
		[Authorize]
		[Route("api/sizeguides/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_size_guide data = dao.GetItemByWeekID(Convert.ToInt32(id)).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_size_guide data)
		{
			try
			{
				if (!data.DeepEquals(new preg_size_guide()))
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
		[Route("api/sizeguides/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_size_guide dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}
		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/sizeguides/{id}")]
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

		public HttpResponseMessage UpdateData(string id, preg_size_guide dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_size_guide()))
				{
					preg_size_guide size_guide = new preg_size_guide();
					size_guide = dao.GetItemByWeekID(Convert.ToInt32(id)).FirstOrDefault();
					if (size_guide == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.image != null)
					{
						size_guide.image = dataUpdate.image;
					}
					if (dataUpdate.title != null)
					{
						size_guide.title = dataUpdate.title;
					}
					if (dataUpdate.description != null)
					{
						size_guide.description = dataUpdate.description;
					}
					if (dataUpdate.week_id != null)
					{
						size_guide.week_id = dataUpdate.week_id;
					}
					if (dataUpdate.length != null)
					{
						size_guide.length = dataUpdate.length;
					}
					if (dataUpdate.weight != null)
					{
						size_guide.weight = dataUpdate.weight;
					}
					if (dataUpdate.type != null)
					{
						size_guide.type = dataUpdate.type;
					}

					dao.UpdateData(size_guide);
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
		[Route("api/sizeguides/{size_guide_id}/upload")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload(string size_guide_id)
		{
			// Check daily_id exist
			preg_size_guide checkItem = dao.GetItemByWeekID(Convert.ToInt32(size_guide_id)).FirstOrDefault();
			if (checkItem == null)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format(SysConst.ITEM_ID_NOT_EXIST, size_guide_id));
			}
			// Get current user_id
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			string dir = "/Files/SizeGuides/" + size_guide_id.ToString();
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
				preg_size_guide updateRow = new preg_size_guide();
				foreach (MultipartFileData file in provider.FileData)
				{
					string path = dir + "/" + HttpUtility.UrlPathEncode(Path.GetFileName(file.LocalFileName));
					files.Add(path);
					updateRow.image = path;
				}
				UpdateData(size_guide_id, updateRow);

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
