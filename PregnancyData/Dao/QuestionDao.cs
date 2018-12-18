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
			return connect.preg_question;
		}

		public preg_question GetItemByID(int id)
		{
			return connect.preg_question.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_question> GetItemsByParams(preg_question data)
		{
			IEnumerable<preg_question> result = connect.preg_question;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "question_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.question_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => c.content == propertyValue.ToString());
				}
				else if (propertyName == "custom_question_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_question_by_user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_question item)
		{
			connect.preg_question.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_question item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_question item = GetItemByID(id);
			connect.preg_question.Remove(item);
			connect.SaveChanges();
		}
	}
}