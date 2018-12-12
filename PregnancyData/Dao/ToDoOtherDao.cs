using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class TodoOtherDao
	{
		PregnancyEntity connect = null;
		public TodoOtherDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_todo_other> GetListItem()
		{
			return connect.preg_todo_others;
		}

		public preg_todo_other GetItemByID(int id)
		{
			return connect.preg_todo_others.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_todo_other> GetItemsByParams(preg_todo_other data)
		{
			IEnumerable<preg_todo_other> result = connect.preg_todo_others;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => c.content == propertyValue.ToString());
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_todo_other item)
		{
			connect.preg_todo_others.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_todo_other item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_todo_other item = GetItemByID(id);
			connect.preg_todo_others.Remove(item);
			connect.SaveChanges();
		}

	}
}