using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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

		public IQueryable<preg_auth> GetListItem()
		{
			return connect.preg_auth;
		}

		public IQueryable<preg_auth> GetItemByID(int id)
		{
			return connect.preg_auth.Where(c => c.id == id);
		}
		public IQueryable<preg_auth> GetItemsByParams(preg_auth data)
		{
			IQueryable<preg_auth> result = connect.preg_auth;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "token" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.token) > 0);
				}
				else if (propertyName == "valid_to" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.valid_to) > 0);
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