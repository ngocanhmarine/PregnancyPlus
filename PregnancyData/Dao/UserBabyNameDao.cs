using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class UserBabyNameDao
	{
		PregnancyEntity connect = null;
		public UserBabyNameDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_user_baby_name> GetListItem()
		{
			return connect.preg_user_baby_name;
		}

		public preg_user_baby_name GetItemByID(int user_id, int baby_name_id)
		{
			return connect.preg_user_baby_name.Where(c => c.user_id == user_id && c.baby_name_id == baby_name_id).FirstOrDefault();
		}

		public IEnumerable<preg_user_baby_name> GetItemByUserID(int user_id)
		{
			return connect.preg_user_baby_name.Where(c => c.user_id == user_id);
		}

		public IEnumerable<preg_user_baby_name> GetItemByParams(preg_user_baby_name data)
		{
			IEnumerable<preg_user_baby_name> result = connect.preg_user_baby_name;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "baby_name_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.baby_name_id == (int)(propertyValue));
				}
			}
			return result;
		}

		public bool InsertData(preg_user_baby_name item)
		{
			IEnumerable<preg_user_baby_name> result = GetItemByParams(item);
			if (result.Count() > 0)
			{
				return false;
			}
			else
			{
				connect.preg_user_baby_name.Add(item);
				connect.SaveChanges();
				return true;
			}
		}

		public void UpdateData(preg_user_baby_name item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int user_id, int baby_name_id)
		{
			preg_user_baby_name item = GetItemByID(user_id, baby_name_id);
			connect.preg_user_baby_name.Remove(item);
			connect.SaveChanges();
		}
	}
}