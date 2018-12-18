using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class UserHospitalBagItemDao
	{
		PregnancyEntity connect = null;
		public UserHospitalBagItemDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_user_hospital_bag_item> GetListItem()
		{
			return connect.preg_user_hospital_bag_item;
		}

		public preg_user_hospital_bag_item GetItemByID(int user_id, int hospital_bag_item_id)
		{
			return connect.preg_user_hospital_bag_item.Where(c => c.user_id == user_id && c.hospital_bag_item_id == hospital_bag_item_id).FirstOrDefault();
		}

		public IEnumerable<preg_user_hospital_bag_item> GetItemByUserID(int user_id)
		{
			return connect.preg_user_hospital_bag_item.Where(c => c.user_id == user_id);
		}

		public IEnumerable<preg_user_hospital_bag_item> GetItemByParams(preg_user_hospital_bag_item data)
		{
			IEnumerable<preg_user_hospital_bag_item> result = connect.preg_user_hospital_bag_item;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "hospital_bag_item_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.hospital_bag_item_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "status" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public bool InsertData(preg_user_hospital_bag_item item)
		{
			IEnumerable<preg_user_hospital_bag_item> result = GetItemByParams(item);
			if (result.Count() > 0)
			{
				return false;
			}
			else
			{
				connect.preg_user_hospital_bag_item.Add(item);
				connect.SaveChanges();
				return true;
			}
		}

		public void UpdateData(preg_user_hospital_bag_item item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int user_id, int hospital_bag_item_id)
		{
			preg_user_hospital_bag_item item = GetItemByID(user_id, hospital_bag_item_id);
			connect.preg_user_hospital_bag_item.Remove(item);
			connect.SaveChanges();
		}
	}
}