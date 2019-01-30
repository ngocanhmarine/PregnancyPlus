using PregnancyData.Entity;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

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

		public IQueryable<preg_weekly_interact> GetListItem()
		{
			return connect.preg_weekly_interact;
		}

		public IQueryable<preg_weekly_interact> GetItemByID(int week_id, int user_id)
		{
			return connect.preg_weekly_interact.Where(c => c.week_id == week_id & c.user_id == user_id);
		}

		public IQueryable<preg_weekly_interact> GetItemByUserID(int user_id)
		{
			return connect.preg_weekly_interact.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_weekly_interact> GetItemsByParams(preg_weekly_interact data)
		{
			IQueryable<preg_weekly_interact> result = connect.preg_weekly_interact;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "week_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.week_id == (int)(propertyValue));
				}
				else if (propertyName == "user_id" && (int)propertyValue != 0)
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
				else if (propertyName == "photo" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.photo) > 0);
				}
				else if (propertyName == "share" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.share) > 0);
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

		public void InsertData(preg_weekly_interact item)
		{
			try
			{
				connect.preg_weekly_interact.Add(item);
				connect.SaveChanges();
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
		}

		public void UpdateData(preg_weekly_interact item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_weekly_interact item)
		{
			connect.preg_weekly_interact.Remove(item);
			connect.SaveChanges();
		}
	}
}