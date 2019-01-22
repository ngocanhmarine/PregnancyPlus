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
	public class MyBirthPlanTypesController : ApiController
	{
		MyBirthPlanTypeDao dao = new MyBirthPlanTypeDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_my_birth_plan_type data)
		{
			try
			{
				IEnumerable<preg_my_birth_plan_type> result;
				if (!data.DeepEquals(new preg_my_birth_plan_type()))
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
		[Route("api/mybirthplantypes/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_my_birth_plan_type data = dao.GetItemByID(Convert.ToInt32(id));
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
		public HttpResponseMessage Post([FromBody]preg_my_birth_plan_type data)
		{
			try
			{
				if (!data.DeepEquals(new preg_my_birth_plan_type()))
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
		[Route("api/mybirthplantypes/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_my_birth_plan_type dataUpdate)
		{
			return UpdateData(id, dataUpdate);
		}

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/mybirthplantypes/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_my_birth_plan_type item = dao.GetItemByID(Convert.ToInt32(id));
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

		public HttpResponseMessage UpdateData(string id, [FromBody]preg_my_birth_plan_type dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_my_birth_plan_type()))
				{
					preg_my_birth_plan_type myBirthPlanType = new preg_my_birth_plan_type();
					myBirthPlanType = dao.GetItemByID(Convert.ToInt32(id));
					if (myBirthPlanType == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.type != null)
					{
						myBirthPlanType.type = dataUpdate.type;
					}
					if (dataUpdate.type_icon != null)
					{
						myBirthPlanType.type_icon = dataUpdate.type_icon;
					}

					dao.UpdateData(myBirthPlanType);
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
		[Authorize(Roles = "dev, admin")]
		[Route("api/mybirthplantypes/{id}/upload")]
		[HttpPost]
		public async Task<HttpResponseMessage> Upload(string id)
		{
			// Check my_birth_plan_type exist
			preg_my_birth_plan_type checkItem = dao.GetItemByID(Convert.ToInt32(id));
			if (checkItem == null)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format(SysConst.ITEM_ID_NOT_EXIST, id));
			}

			string dir = "/Files/MyBirthPlanTypes/" + id.ToString();
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
				preg_my_birth_plan_type updateRow = new preg_my_birth_plan_type();
				foreach (MultipartFileData file in provider.FileData)
				{
					string path = dir + "/" + HttpUtility.UrlPathEncode(Path.GetFileName(file.LocalFileName));
					files.Add(path);
					updateRow.type_icon = path;
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