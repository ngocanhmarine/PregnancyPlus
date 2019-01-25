using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MedicalServicePackageDao
	{
		PregnancyEntity connect = null;
		public MedicalServicePackageDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_medical_service_package> GetListItem()
		{
			return connect.preg_medical_service_package;
		}

		public IQueryable<preg_medical_service_package> GetItemByID(int id)
		{
			return connect.preg_medical_service_package.Where(c => c.id == id);
		}
		public IQueryable<preg_medical_service_package> GetItemsByParams(preg_medical_service_package data)
		{
			IQueryable<preg_medical_service_package> result = connect.preg_medical_service_package;
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
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.description) > 0);
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.content) > 0);
				}
				else if (propertyName == "discount" && propertyValue != null)
				{
					result = result.Where(c => c.discount == (double)(propertyValue));
				}
				else if (propertyName == "execution_department" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.execution_department) > 0);
				}
				else if (propertyName == "address" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.address) > 0);
				}
				else if (propertyName == "execution_time" && propertyValue != null)
				{
					result = result.Where(c => c.execution_time == (DateTime)(propertyValue));
				}
				else if (propertyName == "place" && propertyValue != null)
				{
					result = result.Where(c => c.place == (int)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_medical_service_package item)
		{
			connect.preg_medical_service_package.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_medical_service_package item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_medical_service_package item)
		{
			connect.preg_medical_service_package.Remove(item);
			connect.SaveChanges();
		}
	}
}