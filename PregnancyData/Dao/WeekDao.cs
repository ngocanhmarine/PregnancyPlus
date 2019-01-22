using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class WeekDao
	{
		PregnancyEntity connect = null;
		public WeekDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_week> GetListItem()
		{
			return connect.preg_week;
		}

		public preg_week GetItemByID(int id)
		{
			return connect.preg_week.Where(c => c.id == id).FirstOrDefault();
		}

		public IEnumerable<preg_week> GetItemsByParams(preg_week data)
		{
			IEnumerable<preg_week> result = connect.preg_week;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "length" && propertyValue != null)
				{
					result = result.Where(c => c.length == (double)(propertyValue));
				}
				else if (propertyName == "weight" && propertyValue != null)
				{
					result = result.Where(c => c.weight == (double)(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.title) > 0);
				}
				else if (propertyName == "short_description" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.short_description) > 0);
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.description) > 0);
				}
				else if (propertyName == "daily_relation" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.daily_relation) > 0);
				}
			}
			return result;
		}

		public void InsertData(preg_week item)
		{
			connect.preg_week.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_week item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_week item = GetItemByID(id);
			connect.preg_week.Remove(item);
			connect.SaveChanges();
		}
	}
}