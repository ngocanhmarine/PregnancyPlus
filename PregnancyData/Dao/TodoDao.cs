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
			return connect.preg_todo;
		}

		public preg_todo GetItemByID(int id)
		{
			return connect.preg_todo.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_todo> GetItemsByParams(preg_todo data)
		{
			IEnumerable<preg_todo> result = connect.preg_todo;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "day_id" && propertyValue != null)
				{
					result = result.Where(c => c.day_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "custom_task_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_task_by_user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_todo item)
		{
			connect.preg_todo.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_todo item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_todo item = GetItemByID(id);
			connect.preg_todo.Remove(item);
			connect.SaveChanges();
		}
	}
}