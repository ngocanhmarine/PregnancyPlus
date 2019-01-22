using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PregnancyData.Entity;
using PregnancyData.Dao;

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
				IEnumerable<preg_my_weight> result;
				if (!data.DeepEquals(new preg_my_weight()))
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
		[Route("api/myweights/{id}")]
		public HttpResponseMessage Get(string id)
		{
			try
			{
				preg_my_weight data = dao.GetItemByID(Convert.ToInt32(id));
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
				if (!data.DeepEquals(new preg_my_weight()))
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
		[Route("api/myweights/{id}")]
		public HttpResponseMessage Put(string id, [FromBody]preg_my_weight dataUpdate)
		{
			try
			{
				if (!dataUpdate.DeepEquals(new preg_my_weight()))
				{
					preg_my_weight my_weight = new preg_my_weight();
					my_weight = dao.GetItemByID(Convert.ToInt32(id));
					if (my_weight == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, SysConst.DATA_NOT_FOUND);
					}
					if (dataUpdate.user_id != null)
					{
						my_weight.user_id = dataUpdate.user_id;
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
				preg_my_weight item = dao.GetItemByID(Convert.ToInt32(id));
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
