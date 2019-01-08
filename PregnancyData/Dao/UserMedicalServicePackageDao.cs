using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class UserMedicalServicePackageDao
	{
		PregnancyEntity connect = null;
		public UserMedicalServicePackageDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_user_medical_service_package> GetListItem()
		{
			return connect.preg_user_medical_service_package;
		}

		public preg_user_medical_service_package GetItemByID(int user_id, int medical_service_package_id)
		{
			return connect.preg_user_medical_service_package.Where(c => c.user_id == user_id && c.medical_service_package_id == medical_service_package_id).FirstOrDefault();
		}

		public IEnumerable<preg_user_medical_service_package> GetItemByUserID(int user_id)
		{
			return connect.preg_user_medical_service_package.Where(c => c.user_id == user_id);
		}

		public IEnumerable<preg_user_medical_service_package> GetItemByParams(preg_user_medical_service_package data)
		{
			IEnumerable<preg_user_medical_service_package> result = connect.preg_user_medical_service_package;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "medical_service_package_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.medical_service_package_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "time" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.time == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "description" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.description == propertyValue.ToString());
				}
				else if (propertyName == "total_cost" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.total_cost == Convert.ToDouble(propertyValue));
				}
				else if (propertyName == "status" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "payment_method" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.payment_method == propertyValue.ToString());
				}
				else if (propertyName == "already_paid" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.already_paid == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public bool InsertData(preg_user_medical_service_package item)
		{
			IEnumerable<preg_user_medical_service_package> result = GetItemByParams(item);
			if (result.Count() > 0)
			{
				return false;
			}
			else
			{
				connect.preg_user_medical_service_package.Add(item);
				connect.SaveChanges();
				return true;
			}
		}

		public void UpdateData(preg_user_medical_service_package item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int user_id, int medical_service_package_id)
		{
			preg_user_medical_service_package item = GetItemByID(user_id, medical_service_package_id);
			connect.preg_user_medical_service_package.Remove(item);
			connect.SaveChanges();
		}
	}
}