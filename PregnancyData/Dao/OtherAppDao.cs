using PregnancyData.Entity;
using System;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace PregnancyData.Dao
{
	public class OtherAppDao
	{
		PregnancyEntity connect = null;
		public OtherAppDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_other_app> GetListItem()
		{
			return connect.preg_other_app;
		}

		public IQueryable<preg_other_app> GetItemByID(int id)
		{
			return connect.preg_other_app.Where(c => c.id == id);
		}
		public IQueryable<preg_other_app> GetItemsByParams(preg_other_app data)
		{
			IQueryable<preg_other_app> result = connect.preg_other_app;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "name" && (int)propertyValue != 0)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.name) > 0);
				}
				else if (propertyName == "google_play" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.google_play) > 0);
				}
				else if (propertyName == "app_store" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.app_store) > 0);
				}
				else if (propertyName == "time_created" && propertyValue != null)
				{
					result = result.Where(c => c.time_created == (DateTime)(propertyValue));
				}
				else if (propertyName == "time_last_update" && propertyValue != null)
				{
					result = result.Where(c => c.time_last_update == (DateTime)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_other_app item)
		{
			connect.preg_other_app.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_other_app item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_other_app item)
		{
			connect.preg_other_app.Remove(item);
			connect.SaveChanges();
		}

	}
}