using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Text;
using System.Security.Claims;

namespace _01.Pregnacy_API.Controllers
{
	public class FilesController : ApiController
	{


		[HttpGet]
		[Authorize]
		public async Task<HttpResponseMessage> GetFile([FromBody] Files file)
		{
			try
			{

				string FullPath = HttpContext.Current.Server.MapPath(file.file_path);
				//Check file exist
				if (!File.Exists(FullPath))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format(SysConst.FILE_NOT_EXIST, file.file_path));
				}
				string returnLink = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + file.file_path.Substring(1);
				if (Path.GetExtension(file.file_path).ToLower().Equals(".html"))
				{
					StreamReader strReader = new StreamReader(FullPath);
					string returnHTML = strReader.ReadToEnd();
					strReader.Close();
					return Request.CreateResponse(HttpStatusCode.OK, returnHTML);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.OK, returnLink);
				}
			}

			catch (System.Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}
		[HttpPost]
		[Authorize]
		public async Task<HttpResponseMessage> Upload()
		{
			string dir = "~/Files/Uploads";
			string dirRoot = HttpContext.Current.Server.MapPath(dir);
			//Get  current user_id
			int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
			// Check if the request contains multipart/form-data.
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

				foreach (MultipartFileData file in provider.FileData)
				{
					files.Add(dir + "/" + Path.GetFileName(file.LocalFileName));
				}
				return Request.CreateResponse(HttpStatusCode.Created, files);
			}
			catch (System.Exception e)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}
		}

	}

	public class Files
	{
		public string file_path;
	}
}

