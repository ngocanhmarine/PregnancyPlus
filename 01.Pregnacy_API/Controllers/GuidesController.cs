using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{

	public class GuidesController : ApiController
	{
		GuidesDao dao = new GuidesDao();
		// GET api/values

		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_guides data)
		{
			try
			{
				IQueryable<preg_guides> result;
				if (!data.DeepEquals(new preg_guides()))
				{
					result = dao.GetItemsByParams(data);
				}
				else
				{
					result = dao.GetListItem();
				}
				if (result.Count() > 0)
				{
					return Request.CreateResponse(HttpStatusCode.OK, dao.FilterJoin(result));
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
		[Route("api/guides/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				IQueryable<preg_guides> data = dao.GetItemByID(Convert.ToInt32(id));
				if (data != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK, dao.FilterJoin(data));
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
		public HttpResponseMessage Post([FromBody]preg_guides data)
		{
			try
			{
				if (data.page_id != 0 && data.guides_type_id != 0)
				{
					//Check exist
					preg_guides checkExist = dao.GetItemsByParams(new preg_guides() { page_id = data.page_id, guides_type_id = data.guides_type_id }).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
					}

					//Check page & guide type exist
					using (PregnancyEntity connect = new PregnancyEntity())
					{
						preg_page checkPageExist = connect.preg_page.Where(c => c.id == data.page_id).FirstOrDefault();
						preg_guides_type checkGuideTypeExist = connect.preg_guides_type.Where(c => c.id == data.guides_type_id).FirstOrDefault();
						if (checkPageExist == null || checkGuideTypeExist == null)
						{
							return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
						}
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
		[Route("api/guides/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_guides dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_guides()))
				{
					preg_guides guides = new preg_guides();
					guides = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (guides == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.page_id != null)
					{
						guides.page_id = dataUpdate.page_id;
					}
					if (dataUpdate.guides_type_id != null)
					{
						guides.guides_type_id = dataUpdate.guides_type_id;
					}

					dao.UpdateData(guides);
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

		// DELETE api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/guides/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				preg_guides item = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
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
	}
}
