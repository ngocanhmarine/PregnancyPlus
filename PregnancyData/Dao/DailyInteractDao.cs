using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class DailyInteractDao
	{
		PregnancyEntity connect = null;
		public DailyInteractDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_daily_interact> GetListItem()
		{
			return connect.preg_daily_interact;
		}

		public preg_daily_interact GetItemByID(int daily_id, int user_id)
		{
			return connect.preg_daily_interact.Where(c => c.daily_id == daily_id & c.user_id == user_id).FirstOrDefault();
		}

		public preg_daily_interact GetItemByUserID(int user_id)
		{
			return connect.preg_daily_interact.Where(c => c.user_id == user_id).FirstOrDefault();
		}

		public IEnumerable<preg_daily_interact> GetItemsByParams(preg_daily_interact data)
		{
			IEnumerable<preg_daily_interact> result = connect.preg_daily_interact;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "daily_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.daily_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "like" && propertyValue != null)
				{
					result = result.Where(c => c.like == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "comment" && propertyValue != null)
				{
					result = result.Where(c => c.comment == propertyValue.ToString());
				}
				else if (propertyName == "share" && propertyValue != null)
				{
					result = result.Where(c => c.share == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "notification" && propertyValue != null)
				{
					result = result.Where(c => c.notification == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_daily_interact item)
		{
			connect.preg_daily_interact.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_daily_interact item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_daily_interact item)
		{

			connect.preg_daily_interact.Remove(item);
			connect.SaveChanges();
		}

	}
}