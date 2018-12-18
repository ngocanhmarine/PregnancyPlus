using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class AuthDao
	{
		PregnancyEntity connect = null;
		public AuthDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_auth> GetListItem()
		{
			return connect.preg_auth;
		}

		public preg_auth GetItemByID(int id)
		{
			return connect.preg_auth.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_auth> GetItemsByParams(preg_auth data)
		{
			IEnumerable<preg_auth> result = connect.preg_auth;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "token" && propertyValue != null)
				{
					result = result.Where(c => c.token == propertyValue.ToString());
				}
				else if (propertyName == "valid_to" && propertyValue != null)
				{
					result = result.Where(c => c.valid_to == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_auth item)
		{
			connect.preg_auth.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_auth item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_auth item)
		{

            connect.preg_auth.Remove(item);
			connect.SaveChanges();
		}

	}
}