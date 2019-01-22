using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MedicalTestDao
	{
		PregnancyEntity connect = null;
		public MedicalTestDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_medical_test> GetListItem()
		{
			return connect.preg_medical_test;
		}

		public preg_medical_test GetItemByID(int id)
		{
			return connect.preg_medical_test.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_medical_test> GetItemsByParams(preg_medical_test data)
		{
			IEnumerable<preg_medical_test> result = connect.preg_medical_test;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.title) > 0);
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.type) > 0);
				}
				else if (propertyName == "price" && propertyValue != null)
				{
					result = result.Where(c => c.price == (double)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_medical_test item)
		{
			connect.preg_medical_test.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_medical_test item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_medical_test item)
		{
			connect.preg_medical_test.Remove(item);
			connect.SaveChanges();
		}
	}
}