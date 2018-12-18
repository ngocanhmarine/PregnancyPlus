using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class FaqDao
	{
		PregnancyEntity connect = null;
		public FaqDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_faq> GetListItem()
		{
			return connect.preg_faq;
		}

		public preg_faq GetItemByID(int id)
		{
			return connect.preg_faq.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_faq> GetItemsByParams(preg_faq data)
		{
			IEnumerable<preg_faq> result = connect.preg_faq;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "question" && propertyValue != null)
				{
					result = result.Where(c => c.question == propertyValue.ToString());
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_faq item)
		{
			connect.preg_faq.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_faq item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_faq item)
		{
			connect.preg_faq.Remove(item);
			connect.SaveChanges();
		}
	}
}