using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class TodoDao
	{
		PregnancyEntity connect = null;
		public TodoDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_todo> GetListItem()
		{
			return connect.preg_todos;
		}

		public preg_todo GetItemByID(int id)
		{
			return connect.preg_todos.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_todo> GetItemsByParams(preg_todo data)
		{
			IEnumerable<preg_todo> result = connect.preg_todos;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "week_id" && propertyValue != null)
				{
					result = result.Where(c => c.week_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_todo item)
		{
			connect.preg_todos.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_todo item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_todo item = GetItemByID(id);
			connect.preg_todos.Remove(item);
			connect.SaveChanges();
		}

	}
}