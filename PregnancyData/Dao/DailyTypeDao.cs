using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class DailyTypeDao
	{
		PregnancyEntity connect = null;
		public DailyTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_daily_type> GetListItem()
		{
			return connect.preg_daily_type;
		}

		public preg_daily_type GetItemByID(int id)
		{
			return connect.preg_daily_type.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_daily_type> GetItemsByParams(preg_daily_type data)
		{
			IEnumerable<preg_daily_type> result = connect.preg_daily_type;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => c.type == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_daily_type item)
		{
			connect.preg_daily_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_daily_type item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_daily_type item)
		{
			connect.preg_daily_type.Remove(item);
			connect.SaveChanges();
		}
	}
}