using PregnancyData.Entity;
using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;

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

		public IQueryable<preg_answer> GetListItem()
		{
			return connect.preg_answer;
		}

		public async Task<IQueryable<preg_answer>> GetListItemAsync()
		{
			return connect.preg_answer;
		}

		public IQueryable<preg_answer> GetItemByUserID(int user_id)
		{
			return connect.preg_answer.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_answer> GetItemByID(int user_id, int question_id)
		{
			return connect.preg_answer.Where(c => c.user_id == user_id && c.question_id == question_id);
		}

		public IQueryable<preg_answer> GetItemsByParams(preg_answer data)
		{
			IQueryable<preg_answer> result = connect.preg_answer;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "question_id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.question_id == (int)(propertyValue));
				}
				else if (propertyName == "questiondate" && propertyValue != null)
				{
					result = result.Where(c => c.questiondate == (DateTime)(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.title) > 0);
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.content) > 0);
				}
			}
			return result;
		}

		public void InsertData(preg_answer item)
		{
			connect.preg_answer.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_answer item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_answer item)
		{

			connect.preg_answer.Remove(item);
			connect.SaveChanges();
		}

	}
}