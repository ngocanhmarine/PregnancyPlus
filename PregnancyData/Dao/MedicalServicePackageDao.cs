using PregnancyData.Entity;
using System;
using System.Collections.Generic;
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

		public IEnumerable<preg_medical_service_package> GetListItem()
		{
			return connect.preg_medical_service_package;
		}

		public preg_medical_service_package GetItemByID(int id)
		{
			return connect.preg_medical_service_package.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_medical_service_package> GetItemsByParams(preg_medical_service_package data)
		{
			IEnumerable<preg_medical_service_package> result = connect.preg_medical_service_package;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => c.description == propertyValue.ToString());
				}
				else if (propertyName == "content" && propertyValue != null)
				{
					result = result.Where(c => c.content == propertyValue.ToString());
				}
				else if (propertyName == "discount" && propertyValue != null)
				{
					result = result.Where(c => c.discount == Convert.ToDouble(propertyValue));
				}
				else if (propertyName == "execution_department" && propertyValue != null)
				{
					result = result.Where(c => c.execution_department == propertyValue.ToString());
				}
				else if (propertyName == "address" && propertyValue != null)
				{
					result = result.Where(c => c.address == propertyValue.ToString());
				}
				else if (propertyName == "execution_time" && propertyValue != null)
				{
					result = result.Where(c => c.execution_time == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "place" && propertyValue != null)
				{
					result = result.Where(c => c.place == Convert.ToInt32(propertyValue));
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