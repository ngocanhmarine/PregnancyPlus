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

		public IQueryable<preg_user_todo> GetListItem()
		{
			return connect.preg_user_todo;
		}

		public IQueryable<preg_user_todo> GetItemByID(int user_id, int todo_id)
		{
			return connect.preg_user_todo.Where(c => c.user_id == user_id && c.todo_id == todo_id);
		}

		public IQueryable<preg_user_todo> GetItemByUserID(int user_id)
		{
			return connect.preg_user_todo.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_user_todo> GetItemByParams(preg_user_todo data)
		{
			IQueryable<preg_user_todo> result = connect.preg_user_todo;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "todo_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.todo_id == (int)(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == (int)(propertyValue));
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

		public void DeleteData(preg_user_todo item)
		{
			connect.preg_user_todo.Remove(item);
			connect.SaveChanges();
		}
	}
}