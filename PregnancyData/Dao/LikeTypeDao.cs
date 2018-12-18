using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class LikeTypeDao
	{
		PregnancyEntity connect = null;
		public LikeTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_like_type> GetListItem()
		{
			return connect.preg_like_type;
		}

		public preg_like_type GetItemByID(int id)
		{
			return connect.preg_like_type.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_like_type> GetItemsByParams(preg_like_type data)
		{
			IEnumerable<preg_like_type> result = connect.preg_like_type;
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
		public void InsertData(preg_like_type item)
		{
			connect.preg_like_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_like_type item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_like_type item)
		{
			
			connect.preg_like_type.Remove(item);
			connect.SaveChanges();
		}

	}
}