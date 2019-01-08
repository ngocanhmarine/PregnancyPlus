using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class UserDao
	{
		PregnancyEntity connect = null;
		public UserDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_user> GetListUser()
		{
			return connect.preg_user;
		}

		public preg_user GetUserByID(int id)
		{
			return connect.preg_user.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_user> GetUsersByParams(preg_user data)
		{
			IEnumerable<preg_user> result = connect.preg_user;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "password" && propertyValue != null)
				{
					result = result.Where(c => c.password == propertyValue.ToString());
				}
				else if (propertyName == "phone" && propertyValue != null)
				{
					result = result.Where(c => c.phone == propertyValue.ToString());
				}
				else if (propertyName == "social_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.social_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "first_name" && propertyValue != null)
				{
					result = result.Where(c => c.first_name == propertyValue.ToString());
				}
				else if (propertyName == "last_name" && propertyValue != null)
				{
					result = result.Where(c => c.last_name == propertyValue.ToString());
				}
				else if (propertyName == "you_are_the" && propertyValue != null)
				{
					result = result.Where(c => c.you_are_the == propertyValue.ToString());
				}
				else if (propertyName == "location" && propertyValue != null)
				{
					result = result.Where(c => c.location == propertyValue.ToString());
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == propertyValue.ToString());
				}
				else if (propertyName == "avarta" && propertyValue != null)
				{
					result = result.Where(c => c.avarta == propertyValue.ToString());
				}
				else if (propertyName == "email" && propertyValue != null)
				{
					result = result.Where(c => c.email == propertyValue.ToString());
				}
			}
			return result;
		}

		public bool InsertData(preg_user item)
		{
			IEnumerable<preg_user> result = GetUsersByParams(new preg_user() { email = item.email, social_type_id = item.social_type_id });
			if (result.Count() > 0)
			{
				return false;
			}
			else
			{
				connect.preg_user.Add(item);
				connect.SaveChanges();
				return true;
			}
		}

		public void UpdateData(preg_user item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_user user = GetUserByID(id);
			connect.preg_user.Remove(user);
			connect.SaveChanges();
		}
	}
}