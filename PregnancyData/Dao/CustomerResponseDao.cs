using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

		public IEnumerable<preg_customer_response> GetListItem()
		{
			return connect.preg_customer_response;
		}

		public preg_customer_response GetItemByID(int user_id)
		{
			return connect.preg_customer_response.Where(c => c.user_id == user_id).FirstOrDefault();
		}

		public IEnumerable<preg_customer_response> GetItemsByParams(preg_customer_response data)
		{
			IEnumerable<preg_customer_response> result = connect.preg_customer_response;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => c.content == propertyValue.ToString());
				}
				else if (propertyName == "time" && propertyValue != null)
				{
					result = result.Where(c => c.time == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "answer_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.answer_user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "answer_date" && propertyValue != null)
				{
					result = result.Where(c => c.answer_date == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "answer_content" && propertyValue != null)
				{
					result = result.Where(c => c.answer_content == propertyValue.ToString());
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