using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class HospitalBagItemDao
	{
		PregnancyEntity connect = null;
		public HospitalBagItemDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_hospital_bag_item> GetListItem()
		{
			return connect.preg_hospital_bag_item;
		}

		public IQueryable<preg_hospital_bag_item> GetItemByID(int id)
		{
			return connect.preg_hospital_bag_item.Where(c => c.id == id);
		}
		public IQueryable<preg_hospital_bag_item> GetItemsByParams(preg_hospital_bag_item data)
		{
			IQueryable<preg_hospital_bag_item> result = connect.preg_hospital_bag_item;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.name) > 0);
				}
				else if (propertyName == "type" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.type == (int)(propertyValue));
				}
				else if (propertyName == "custom_item_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_item_by_user_id == (int)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_hospital_bag_item item)
		{
			connect.preg_hospital_bag_item.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_hospital_bag_item item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_hospital_bag_item item)
		{
			connect.preg_hospital_bag_item.Remove(item);
			connect.SaveChanges();
		}
	}
}