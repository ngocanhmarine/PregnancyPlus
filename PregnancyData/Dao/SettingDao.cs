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

		public IQueryable<preg_setting> GetListItem()
		{
			return connect.preg_setting;
		}

		public IQueryable<preg_setting> GetItemByID(int id)
		{
			return connect.preg_setting.Where(c => c.id == id);
		}
		public IQueryable<preg_setting> GetItemsByParams(preg_setting data)
		{
			IQueryable<preg_setting> result = connect.preg_setting;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
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
					result = result.Where(c => c.weight_unit == (int)(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "revoke_consent" && propertyValue != null)
				{
					result = result.Where(c => c.revoke_consent == (int)(propertyValue));
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

		public void DeleteData(preg_setting item)
		{
			connect.preg_setting.Remove(item);
			connect.SaveChanges();
		}
	}
}