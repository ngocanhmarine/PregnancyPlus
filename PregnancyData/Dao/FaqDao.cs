using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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

		public IQueryable<preg_faq> GetListItem()
		{
			return connect.preg_faq;
		}

		public IQueryable<preg_faq> GetItemByID(int id)
		{
			return connect.preg_faq.Where(c => c.id == id);
		}
		public IQueryable<preg_faq> GetItemsByParams(preg_faq data)
		{
			IQueryable<preg_faq> result = connect.preg_faq;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "question" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.question) > 0);
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == (int)(propertyValue));
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