using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MedicalPackageTestDao
	{
		PregnancyEntity connect = null;
		public MedicalPackageTestDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_medical_package_test> GetListItem()
		{
			return connect.preg_medical_package_test;
		}

		public preg_medical_package_test GetItemByPackageID(int medical_service_package_id)
		{
			return connect.preg_medical_package_test.Where(c => c.medical_service_package_id == medical_service_package_id).FirstOrDefault();
		}

		public preg_medical_package_test GetItemByID(int medical_service_package_id, int medical_test_id)
		{
			return connect.preg_medical_package_test.Where(c => c.medical_service_package_id == medical_service_package_id & c.medical_test_id == medical_test_id).FirstOrDefault();
		}
		public IEnumerable<preg_medical_package_test> GetItemsByParams(preg_medical_package_test data)
		{
			IEnumerable<preg_medical_package_test> result = connect.preg_medical_package_test;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "medical_service_package_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.medical_service_package_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "medical_test_id" && propertyValue != null)
				{
					result = result.Where(c => c.medical_test_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public bool InsertData(preg_medical_package_test item)
		{
			try
			{
				connect.preg_medical_package_test.Add(item);
				connect.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public void UpdateData(preg_medical_package_test item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_medical_package_test item)
		{
			connect.preg_medical_package_test.Remove(item);
			connect.SaveChanges();
		}
	}
}