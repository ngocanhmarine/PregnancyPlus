using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class BabyNameDao
	{
		PregnancyEntity connect = null;
		public BabyNameDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_baby_name> GetListItem()
		{
			return connect.preg_baby_name;
		}

		public preg_baby_name GetItemByID(int id)
		{
			return connect.preg_baby_name.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_baby_name> GetItemsByParams(preg_baby_name data)
		{
			IEnumerable<preg_baby_name> result = connect.preg_baby_name;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "country_id" && propertyValue != null)
				{
					result = result.Where(c => c.country_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "gender_id" && propertyValue != null)
				{
					result = result.Where(c => c.gender_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					result = result.Where(c => c.name == propertyValue.ToString());
				}
				else if (propertyName == "custom_baby_name_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_baby_name_by_user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_baby_name item)
		{
			connect.preg_baby_name.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_baby_name item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_baby_name item)
		{
			connect.preg_baby_name.Remove(item);
			connect.SaveChanges();
		}

	}
}