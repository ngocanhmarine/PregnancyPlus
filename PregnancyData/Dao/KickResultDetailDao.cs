using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class KickResultDetailDao
	{
		PregnancyEntity connect = null;
		public KickResultDetailDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_kick_result_detail> GetListItem()
		{
			return connect.preg_kick_result_detail;
		}

		public IQueryable<preg_kick_result_detail> GetItemByID(int kick_result_id)
		{
			return connect.preg_kick_result_detail.Where(c => c.kick_result_id == kick_result_id);
		}
		public IQueryable<preg_kick_result_detail> GetItemsByParams(preg_kick_result_detail data)
		{
			IQueryable<preg_kick_result_detail> result = connect.preg_kick_result_detail;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "kick_result_id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.kick_result_id == (int)(propertyValue));
				}
				else if (propertyName == "kick_order" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.kick_order == (int)(propertyValue));
				}
				else if (propertyName == "kick_time" && propertyValue != null)
				{
					result = result.Where(c => c.kick_time == (DateTime)(propertyValue));
				}
				else if (propertyName == "elapsed_time" && propertyValue != null)
				{
					result = result.Where(c => c.elapsed_time == (float)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_kick_result_detail item)
		{
			connect.preg_kick_result_detail.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_kick_result_detail item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_kick_result_detail item)
		{

			connect.preg_kick_result_detail.Remove(item);
			connect.SaveChanges();
		}

		public IQueryable FilterJoin(IQueryable<preg_kick_result_detail> items, int user_id)
		{
			IQueryable query = (from krd in items
								join kr in connect.preg_kick_result on krd.kick_result_id equals kr.id
								join ukh in connect.preg_user_kick_history on new { kr.id, user_id } equals new { id = ukh.kick_result_id, ukh.user_id }
								select krd);
			return query;
		}
	}
}