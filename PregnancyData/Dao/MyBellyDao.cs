using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyBellyDao
	{
		PregnancyEntity connect = null;
		public MyBellyDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_belly> GetListItem()
		{
			return connect.preg_my_bellys;
		}

		public preg_my_belly GetItemByID(int id)
		{
			return connect.preg_my_bellys.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_my_belly> GetItemsByParams(preg_my_belly data)
		{
			IEnumerable<preg_my_belly> result = connect.preg_my_bellys;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "image" && propertyValue != null)
				{
					result = result.Where(c => c.image == propertyValue.ToString());
				}
				else if (propertyName == "my_belly_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.my_belly_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "month" && propertyValue != null)
				{
					result = result.Where(c => c.month == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_my_belly item)
		{
			connect.preg_my_bellys.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_belly item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_my_belly item)
		{
			
			connect.preg_my_bellys.Remove(item);
			connect.SaveChanges();
		}

	}
}