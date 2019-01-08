using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class WeeklyInteractDao
	{
		PregnancyEntity connect = null;
		public WeeklyInteractDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_weekly_interact> GetListItem()
		{
			return connect.preg_weekly_interact;
		}

		public preg_weekly_interact GetItemByID(int week_id, int user_id)
		{
			return connect.preg_weekly_interact.Where(c => c.week_id == week_id & c.user_id == user_id).FirstOrDefault();
		}

		public IEnumerable<preg_weekly_interact> GetItemByUserID(int user_id)
		{
			return connect.preg_weekly_interact.Where(c => c.user_id == user_id);
		}

		public IEnumerable<preg_weekly_interact> GetItemsByParams(preg_weekly_interact data)
		{
			IEnumerable<preg_weekly_interact> result = connect.preg_weekly_interact;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "week_id" && propertyValue != null)
				{
					result = result.Where(c => c.week_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
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
				else if (propertyName == "photo" && propertyValue != null)
				{
					result = result.Where(c => c.photo == propertyValue.ToString());
				}
				else if (propertyName == "share" && propertyValue != null)
				{
					result = result.Where(c => c.share == propertyValue.ToString());
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

		public void InsertData(preg_weekly_interact item)
		{
			connect.preg_weekly_interact.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_weekly_interact item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int week_id, int user_id)
		{
			preg_weekly_interact item = GetItemByID(week_id, user_id);
			connect.preg_weekly_interact.Remove(item);
			connect.SaveChanges();
		}
	}
}