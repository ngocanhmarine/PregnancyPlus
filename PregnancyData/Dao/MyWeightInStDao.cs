using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyWeightInStDao
	{
		PregnancyEntity connect = null;
		public MyWeightInStDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_weight_in_st> GetListItem()
		{
			return connect.preg_my_weight_in_sts;
		}

		public preg_my_weight_in_st GetItemByID(int id)
		{
			return connect.preg_my_weight_in_sts.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_my_weight_in_st> GetItemsByParams(preg_my_weight_in_st data)
		{
			IEnumerable<preg_my_weight_in_st> result = connect.preg_my_weight_in_sts;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "position" && propertyValue != null)
				{
					result = result.Where(c => c.position == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "value" && propertyValue != null)
				{
					result = result.Where(c => c.value == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_my_weight_in_st item)
		{
			connect.preg_my_weight_in_sts.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_weight_in_st item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_my_weight_in_st item)
		{
			
			connect.preg_my_weight_in_sts.Remove(item);
			connect.SaveChanges();
		}

	}
}