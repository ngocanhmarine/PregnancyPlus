using PregnancyData.Entity;
using System;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace PregnancyData.Dao
{
	public class CustomerResponseDao
	{
		PregnancyEntity connect = null;
		public CustomerResponseDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_customer_response> GetListItem()
		{
			return connect.preg_customer_response;
		}

		public IQueryable<preg_customer_response> GetItemByUserID(int user_id)
		{
			return connect.preg_customer_response.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_customer_response> GetItemsByParams(preg_customer_response data)
		{
			IQueryable<preg_customer_response> result = connect.preg_customer_response;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "user_id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.content) > 0);
				}
				else if (propertyName == "time" && propertyValue != null)
				{
					result = result.Where(c => c.time == (DateTime)(propertyValue));
				}
				else if (propertyName == "answer_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.answer_user_id == (int)(propertyValue));
				}
				else if (propertyName == "answer_date" && propertyValue != null)
				{
					result = result.Where(c => c.answer_date == (DateTime)(propertyValue));
				}
				else if (propertyName == "answer_content" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.answer_content) > 0);
				}
			}
			return result;
		}

		public void InsertData(preg_customer_response item)
		{
			connect.preg_customer_response.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_customer_response item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_customer_response item)
		{

			connect.preg_customer_response.Remove(item);
			connect.SaveChanges();
		}
	}
}