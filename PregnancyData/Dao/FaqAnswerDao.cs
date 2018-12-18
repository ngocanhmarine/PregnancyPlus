using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class FaqAnswerDao
	{
		PregnancyEntity connect = null;
		public FaqAnswerDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_faq_answer> GetListItem()
		{
			return connect.preg_faq_answer;
		}

		public preg_faq_answer GetItemByID(int id)
		{
			return connect.preg_faq_answer.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_faq_answer> GetItemsByParams(preg_faq_answer data)
		{
			IEnumerable<preg_faq_answer> result = connect.preg_faq_answer;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "faq_id" && propertyValue != null)
				{
					result = result.Where(c => c.faq_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "answer_content" && propertyValue != null)
				{
					result = result.Where(c => c.answer_content == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_faq_answer item)
		{
			connect.preg_faq_answer.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_faq_answer item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_faq_answer item)
		{
			connect.preg_faq_answer.Remove(item);
			connect.SaveChanges();
		}
	}
}