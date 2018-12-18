using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyBellyTypeDao
	{
		PregnancyEntity connect = null;
		public MyBellyTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_belly_type> GetListItem()
		{
			return connect.preg_my_belly_type;
		}

		public preg_my_belly_type GetItemByID(int id)
		{
			return connect.preg_my_belly_type.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_my_belly_type> GetItemsByParams(preg_my_belly_type data)
		{
			IEnumerable<preg_my_belly_type> result = connect.preg_my_belly_type;
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
					result = result.Where(c => c.type == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_my_belly_type item)
		{
			connect.preg_my_belly_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_belly_type item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_my_belly_type item)
		{
			connect.preg_my_belly_type.Remove(item);
			connect.SaveChanges();
		}

	}
}