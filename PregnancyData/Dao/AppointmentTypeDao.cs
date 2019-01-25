using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class AppointmentTypeDao
	{
		PregnancyEntity connect = null;
		public AppointmentTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_appointment_type> GetListItem()
		{
			return connect.preg_appointment_type;
		}

		public IQueryable<preg_appointment_type> GetItemByID(int id)
		{
			return connect.preg_appointment_type.Where(c => c.id == id);
		}
		public IQueryable<preg_appointment_type> GetItemsByParams(preg_appointment_type data)
		{
			IQueryable<preg_appointment_type> result = connect.preg_appointment_type;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.type) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_appointment_type item)
		{
			connect.preg_appointment_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_appointment_type item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_appointment_type item)
		{
			connect.preg_appointment_type.Remove(item);
			connect.SaveChanges();
		}
	}
}