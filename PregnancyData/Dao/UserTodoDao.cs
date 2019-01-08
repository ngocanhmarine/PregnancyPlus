using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class UserTodoDao
	{
		PregnancyEntity connect = null;
		public UserTodoDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_user_todo> GetListItem()
		{
			return connect.preg_user_todo;
		}

		public preg_user_todo GetItemByID(int user_id, int todo_id)
		{
			return connect.preg_user_todo.Where(c => c.user_id == user_id && c.todo_id == todo_id).FirstOrDefault();
		}

		public IEnumerable<preg_user_todo> GetItemByUserID(int user_id)
		{
			return connect.preg_user_todo.Where(c => c.user_id == user_id);
		}

		public IEnumerable<preg_user_todo> GetItemByParams(preg_user_todo data)
		{
			IEnumerable<preg_user_todo> result = connect.preg_user_todo;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "todo_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.todo_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "status" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public bool InsertData(preg_user_todo item)
		{
			IEnumerable<preg_user_todo> result = GetItemByParams(item);
			if (result.Count() > 0)
			{
				return false;
			}
			else
			{
				connect.preg_user_todo.Add(item);
				connect.SaveChanges();
				return true;
			}
		}

		public void UpdateData(preg_user_todo item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int user_id, int todo_id)
		{
			preg_user_todo item = GetItemByID(user_id, todo_id);
			connect.preg_user_todo.Remove(item);
			connect.SaveChanges();
		}
	}
}