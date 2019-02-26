using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PregnancyData.Entity;
using PregnancyData.Dao;
using System.Security.Claims;

namespace _01.Pregnacy_API.Controllers
{

	public class KickResultsController : ApiController
	{
		KickResultDao dao = new KickResultDao();
		// GET api/values
		[Authorize(Roles = "dev, admin")]
		public HttpResponseMessage Get([FromUri]preg_kick_result data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_kick_result> result;
				if (!data.DeepEquals(new preg_kick_result()))
				{
					result = dao.GetItemsByParams(data);
				}
				else
				{
					result = dao.GetListItem();
				}
				result = dao.FilterByUserID(result, user_id);
				if (result.Any())
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
		[Authorize(Roles = "dev, admin")]
		[Route("api/kickresults/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IQueryable<preg_kick_result> data = dao.GetItemByID(Convert.ToInt32(id));
				data = dao.FilterByUserID(data, user_id);
				if (data.Any())
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
		public HttpResponseMessage Post([FromBody]preg_kick_result data)
		{
			try
			{
				if (!data.DeepEquals(new preg_kick_result()))
				{
					dao.InsertData(data);
					//Insert to UserKickHistories
					int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
					preg_user_kick_history userKickHistory = new preg_user_kick_history() { user_id = user_id, kick_result_id = data.id };
					using (PregnancyEntity connect = new PregnancyEntity())
					{
						connect.preg_user_kick_history.Add(userKickHistory);
						connect.SaveChanges();
					}
					return Request.CreateResponse(HttpStatusCode.Created, data);
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
		[Route("api/kickresults/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_kick_result dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_kick_result()))
				{
					preg_kick_result kick_result = new preg_kick_result();
					kick_result = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (kick_result == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.kick_date != null)
					{
						kick_result.kick_date = dataUpdate.kick_date;
					}
					if (dataUpdate.duration != null)
					{
						kick_result.duration = dataUpdate.duration;
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
		[Route("api/kickresults/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				//Delete reference
				using (PregnancyEntity connect = new PregnancyEntity())
				{
					preg_kick_result item = dao.GetItemByID(Convert.ToInt32(id)).FirstOrDefault();
					if (item == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (!DeleteReferenceData(Convert.ToInt32(id)))
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, SysConst.DATA_DELETE_FAIL);
					}

					dao.DeleteData(item);
					return Request.CreateResponse(HttpStatusCode.Accepted, SysConst.DATA_DELETE_SUCCESS);
				}
			}
			catch (Exception ex)
			{
				HttpError err = new HttpError(ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
			}
		}

		public bool DeleteReferenceData(int kick_result_id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				PregnancyEntity connect = new PregnancyEntity();
				IEnumerable<preg_kick_result_detail> kickResultDetailDel = connect.preg_kick_result_detail.Where(c => c.kick_result_id == kick_result_id);
				while (kickResultDetailDel.Count() > 0)
				{
					connect.preg_kick_result_detail.Remove(kickResultDetailDel.FirstOrDefault());
					connect.SaveChanges();
				}
				IEnumerable<preg_user_kick_history> userKickHistoryDel = connect.preg_user_kick_history.Where(c => c.user_id == user_id && c.kick_result_id == kick_result_id);
				while (userKickHistoryDel.Count() > 0)
				{
					connect.preg_user_kick_history.Remove(userKickHistoryDel.FirstOrDefault());
					connect.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}