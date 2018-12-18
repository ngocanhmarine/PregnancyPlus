using PregnancyData.Entity;
using System;
using System.Collections.Generic;
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

        public IEnumerable<preg_hospital_bag_item> GetListItem()
        {
            return connect.preg_hospital_bag_item;
        }

        public preg_hospital_bag_item GetItemByID(int id)
        {
            return connect.preg_hospital_bag_item.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_hospital_bag_item> GetItemsByParams(preg_hospital_bag_item data)
		{
			IEnumerable<preg_hospital_bag_item> result = connect.preg_hospital_bag_item;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					result = result.Where(c => c.name == propertyValue.ToString());
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => c.type == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "custom_item_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_item_by_user_id == Convert.ToInt32(propertyValue));
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