using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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

		public IQueryable<preg_user> GetListUser()
		{
			return connect.preg_user;
		}

		public IQueryable<preg_user> GetUserByID(int id)
		{
			return connect.preg_user.Where(c => c.id == id);
		}

		public IQueryable<preg_user> GetUsersByParams(preg_user data)
		{
			IQueryable<preg_user> result = connect.preg_user;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;

				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);

				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)propertyValue);
				}
				else if (propertyName == "password" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.password) > 0);
				}
				else if (propertyName == "phone" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.phone) > 0);
				}
				else if (propertyName == "social_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.social_type_id == (int)(propertyValue));
				}
				else if (propertyName == "first_name" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.first_name) > 0);
				}
				else if (propertyName == "last_name" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.last_name) > 0);
				}
				else if (propertyName == "you_are_the" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.you_are_the) > 0);
				}
				else if (propertyName == "location" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.location) > 0);
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.status) > 0);
				}
				else if (propertyName == "avatar" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.avatar) > 0);
				}
				else if (propertyName == "email" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.email) > 0);
				}
				else if (propertyName == "uid" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.uid) > 0);
				}
				else if (propertyName == "time_last_login" && propertyValue != null)
				{
					result = result.Where(c => c.time_last_login == (DateTime)(propertyValue));
				}
			}
			return result;
		}

		public bool InsertData(preg_user item)
		{
			IEnumerable<preg_user> result = GetUsersByParams(new preg_user() { phone = item.phone, social_type_id = item.social_type_id });
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
			preg_user user = connect.preg_user.Where(c => c.id == id).FirstOrDefault();
			connect.preg_user.Remove(user);
			connect.SaveChanges();
		}

		public IQueryable FilterJoin(IQueryable<preg_user> items, int user_id)
		{
			IQueryable<preg_pregnancy> myPregnancy = (from mp in connect.preg_pregnancy
													  where mp.user_id == user_id
													  select mp);
			IQueryable query = (from u in items
								join up in myPregnancy on u.id equals up.user_id into joined
								from j in joined.DefaultIfEmpty()
								select new { u.id, u.phone, u.social_type_id, u.first_name, u.last_name, u.you_are_the, u.location, u.status, u.avatar, u.email, u.uid, u.time_created, u.time_last_login, j.baby_gender, j.due_date });
			return query;
		}
	}
}