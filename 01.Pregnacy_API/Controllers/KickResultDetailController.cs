using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{

	public class KickResultDetailController : ApiController
	{
		KickResultDetailDao dao = new KickResultDetailDao();
		// GET api/values
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Get([FromUri]preg_kick_result_detail data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_kick_result_detail> result;
				if (!data.DeepEquals(new preg_kick_result_detail()))
				{
					result = dao.GetItemsByParams(data);
				}
				else
				{
					result = dao.GetListItem();
				}
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
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
			}
		}

		// GET api/values/5
		[Authorize(Roles = "dev, admin")]
		[Route("api/kickresultdetail/{kick_result_id}")]
		public HttpResponseMessage Get(string kick_result_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_kick_result_detail> data = dao.GetItemByID(Convert.ToInt32(kick_result_id));
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
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Post([FromBody]preg_kick_result_detail data)
		{
			try
			{
				if (data.kick_result_id != 0 && data.kick_order != 0)
				{
					//Check exist
					preg_kick_result_detail checkExist = dao.GetListItem().Where(c => c.kick_result_id == data.kick_result_id && c.kick_order == data.kick_order).FirstOrDefault();
					if (checkExist != null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_EXIST);
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
		[Route("api/kickresultdetail/{kick_result_id}/{kick_order}")]
		public HttpResponseMessage Put(string kick_result_id, string kick_order, [FromBody]preg_kick_result_detail dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_kick_result_detail()))
				{
					preg_kick_result_detail kick_result = new preg_kick_result_detail();
					int intKickResultId = Convert.ToInt32(kick_result_id);
					int intKickOrder = Convert.ToInt32(kick_order);
					kick_result = dao.GetListItem().Where(c => c.kick_result_id == intKickResultId && c.kick_order == intKickOrder).FirstOrDefault();
					if (kick_result == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.kick_time != null)
					{
						kick_result.kick_time = dataUpdate.kick_time;
					}
					if (dataUpdate.elapsed_time != null)
					{
						kick_result.elapsed_time = dataUpdate.elapsed_time;
					}

					dao.UpdateData(kick_result);
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
		[Route("api/kickresultdetail/{kick_result_id}/{kick_order}")]
		public HttpResponseMessage Delete(string kick_result_id, string kick_order)
		{
			try
			{
				int intKickResultId = Convert.ToInt32(kick_result_id);
				int intKickOrder = Convert.ToInt32(kick_order);
				preg_kick_result_detail item = dao.GetListItem().Where(c => c.kick_result_id == intKickResultId && c.kick_order == intKickOrder).FirstOrDefault();
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