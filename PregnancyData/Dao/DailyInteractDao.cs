using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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
				if (propertyName == "daily_id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.daily_id == (int)(propertyValue));
				}
				else if (propertyName == "user_id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "like" && propertyValue != null)
				{
					result = result.Where(c => c.like == (int)(propertyValue));
				}
				else if (propertyName == "comment" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.comment) > 0);
				}
				else if (propertyName == "share" && propertyValue != null)
				{
					result = result.Where(c => c.share == (int)(propertyValue));
				}
				else if (propertyName == "notification" && propertyValue != null)
				{
					result = result.Where(c => c.notification == (int)(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == (int)(propertyValue));
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