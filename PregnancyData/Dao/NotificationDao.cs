using PregnancyData.Entity;
using System;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace PregnancyData.Dao
{
	public class NotificationDao
	{
		PregnancyEntity connect = null;
		public NotificationDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_notification> GetListItem()
		{
			return connect.preg_notification;
		}

		public IQueryable<preg_notification> GetItemByID(int id)
		{
			return connect.preg_notification.Where(c => c.id == id);
		}
		public IQueryable<preg_notification> GetItemsByParams(preg_notification data)
		{
			IQueryable<preg_notification> result = connect.preg_notification;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "week_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.week_id == (int)(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.title) > 0);
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.content) > 0);
				}
				else if (propertyName == "time_created" && propertyValue != null)
				{
					result = result.Where(c => c.time_created == (DateTime)(propertyValue));
				}
				else if (propertyName == "time_last_push" && propertyValue != null)
				{
					result = result.Where(c => c.time_last_push == (DateTime)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_notification item)
		{
			connect.preg_notification.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_notification item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_notification item)
		{
			connect.preg_notification.Remove(item);
			connect.SaveChanges();
		}

	}
}