using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class QuestionDao
	{
		PregnancyEntity connect = null;
		public QuestionDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_question> GetListItem()
		{
			return connect.preg_questions;
		}

		public preg_question GetItemByID(int id)
		{
			return connect.preg_questions.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_question> GetItemsByParams(preg_question data)
		{
			IEnumerable<preg_question> result = connect.preg_questions;
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
				else if (propertyName == "questiondate" && propertyValue != null)
				{
					result = result.Where(c => c.questiondate == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => c.content == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_question item)
		{
			connect.preg_questions.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_question item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_question item = GetItemByID(id);
			connect.preg_questions.Remove(item);
			connect.SaveChanges();
		}

	}
}