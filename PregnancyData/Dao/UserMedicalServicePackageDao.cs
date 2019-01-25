using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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

		public IQueryable<preg_user_medical_service_package> GetListItem()
		{
			return connect.preg_user_medical_service_package;
		}

		public IQueryable<preg_user_medical_service_package> GetItemByID(int user_id, int medical_service_package_id)
		{
			return connect.preg_user_medical_service_package.Where(c => c.user_id == user_id && c.medical_service_package_id == medical_service_package_id);
		}

		public IQueryable<preg_user_medical_service_package> GetItemByUserID(int user_id)
		{
			return connect.preg_user_medical_service_package.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_user_medical_service_package> GetItemByParams(preg_user_medical_service_package data)
		{
			IQueryable<preg_user_medical_service_package> result = connect.preg_user_medical_service_package;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "medical_service_package_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.medical_service_package_id == (int)(propertyValue));
				}
				else if (propertyName == "time" && propertyValue != null)
				{
					result = result.Where(c => c.time == (DateTime)(propertyValue));
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.description) > 0);
				}
				else if (propertyName == "total_cost" && propertyValue != null)
				{
					result = result.Where(c => c.total_cost == (double)(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == (int)(propertyValue));
				}
				else if (propertyName == "payment_method" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.payment_method) > 0);
				}
				else if (propertyName == "already_paid" && propertyValue != null)
				{
					result = result.Where(c => c.already_paid == (int)(propertyValue));
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
			preg_user_medical_service_package item = GetItemByID(user_id, medical_service_package_id).FirstOrDefault();
			connect.preg_user_medical_service_package.Remove(item);
			connect.SaveChanges();
		}
	}
}