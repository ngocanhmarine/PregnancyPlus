using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class AnswerDao
    {  
        PregnancyEntity connect = null;
        public AnswerDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_answer> GetListItem()
        {
            return connect.preg_answers;
        }

        public preg_answer GetItemByID(int id)
        {
            return connect.preg_answers.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_answer> GetItemsByParams(preg_answer data)
		{
			IEnumerable<preg_answer> result = connect.preg_answers;
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
				else if (propertyName == "answerdate" && propertyValue != null)
				{
					result = result.Where(c => c.answerdate == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => c.content == propertyValue.ToString());
				}
				else if (propertyName == "question_id" && propertyValue != null)
				{
					result = result.Where(c => c.question_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_answer item)
        {
            connect.preg_answers.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_answer item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(preg_answer item)
        {
            
            connect.preg_answers.Remove(item);
            connect.SaveChanges();
        }

    }
}