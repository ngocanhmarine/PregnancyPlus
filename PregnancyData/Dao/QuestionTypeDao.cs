using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class QuestionTypeDao
	{
		PregnancyEntity connect = null;
		public QuestionTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_question_type> GetListItem()
		{
			return connect.preg_question_type;
		}

		public preg_question_type GetItemByID(int id)
		{
			return connect.preg_question_type.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_question_type> GetItemsByParams(preg_question_type data)
		{
			IEnumerable<preg_question_type> result = connect.preg_question_type;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.type) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_question_type item)
		{
			connect.preg_question_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_question_type item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_question_type item = GetItemByID(id);
			connect.preg_question_type.Remove(item);
			connect.SaveChanges();
		}
	}
}