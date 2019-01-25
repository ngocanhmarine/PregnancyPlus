using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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

		public IQueryable<preg_faq_answer> GetListItem()
		{
			return connect.preg_faq_answer;
		}

		public IQueryable<preg_faq_answer> GetItemByID(int id)
		{
			return connect.preg_faq_answer.Where(c => c.id == id);
		}
		public IQueryable<preg_faq_answer> GetItemsByParams(preg_faq_answer data)
		{
			IQueryable<preg_faq_answer> result = connect.preg_faq_answer;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "faq_id" && propertyValue != null)
				{
					result = result.Where(c => c.faq_id == (int)(propertyValue));
				}
				else if (propertyName == "answer_content" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.answer_content) > 0);
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