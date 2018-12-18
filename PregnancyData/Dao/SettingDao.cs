using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class SettingDao
	{
		PregnancyEntity connect = null;
		public SettingDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_setting> GetListItem()
		{
			return connect.preg_setting;
		}

		public preg_setting GetItemByID(int id)
		{
			return connect.preg_setting.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_setting> GetItemsByParams(preg_setting data)
		{
			IEnumerable<preg_setting> result = connect.preg_setting;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "reminders" && propertyValue != null)
				{
					result = result.Where(c => c.reminders == Convert.ToBoolean(propertyValue));
				}
				else if (propertyName == "length_units" && propertyValue != null)
				{
					result = result.Where(c => c.length_units == Convert.ToBoolean(propertyValue));
				}
				else if (propertyName == "weight_unit" && propertyValue != null)
				{
					result = result.Where(c => c.weight_unit == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "revoke_consent" && propertyValue != null)
				{
					result = result.Where(c => c.revoke_consent == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_setting item)
		{
			connect.preg_setting.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_setting item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_setting item = GetItemByID(id);
			connect.preg_setting.Remove(item);
			connect.SaveChanges();
		}
	}
}