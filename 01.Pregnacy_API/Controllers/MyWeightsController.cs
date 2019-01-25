using PregnancyData.Dao;
using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace _01.Pregnacy_API.Controllers
{

	public class MyWeightsController : ApiController
	{
		MyWeightDao dao = new MyWeightDao();
		// GET api/values
		[Authorize]
		public HttpResponseMessage Get([FromUri]preg_my_weight data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				IEnumerable<preg_my_weight> result;
				if (!data.DeepEquals(new preg_my_weight()))
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
		[Route("api/myweights/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_my_weight data = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.user_id == user_id).FirstOrDefault();
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
		public HttpResponseMessage Post([FromBody]preg_my_weight data)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!data.DeepEquals(new preg_my_weight()))
				{
					data.user_id = user_id;
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
		[Route("api/myweights/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_my_weight dataUpdate)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				if (!dataUpdate.DeepEquals(new preg_my_weight()))
				{
					preg_my_weight my_weight = new preg_my_weight();
					my_weight = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.user_id == user_id).FirstOrDefault();
					if (my_weight == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.my_weight_type_id != null)
					{
						my_weight.my_weight_type_id = dataUpdate.my_weight_type_id;
					}
					if (dataUpdate.start_date != null)
					{
						my_weight.start_date = dataUpdate.start_date;
					}
					if (dataUpdate.pre_pregnancy_weight != null)
					{
						my_weight.pre_pregnancy_weight = dataUpdate.pre_pregnancy_weight;
					}
					if (dataUpdate.current_weight != null)
					{
						my_weight.current_weight = dataUpdate.current_weight;
					}
					if (dataUpdate.week_id != null)
					{
						my_weight.week_id = dataUpdate.week_id;
					}
					if (dataUpdate.current_date != null)
					{
						my_weight.current_date = dataUpdate.current_date;
					}

					dao.UpdateData(my_weight);
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
		[Route("api/myweights/{id}")]
		public HttpResponseMessage Delete(string id)
		{
			try
			{
				int user_id = Convert.ToInt32(((ClaimsIdentity)(User.Identity)).FindFirst("id").Value);
				preg_my_weight item = dao.GetItemByID(Convert.ToInt32(id)).Where(c => c.user_id == user_id).FirstOrDefault();
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
