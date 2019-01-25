using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class AppointmentMeasurementDao
	{
		PregnancyEntity connect = null;
		public AppointmentMeasurementDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_appointment_measurement> GetListItem()
		{
			return connect.preg_appointment_measurement;
		}

		public IQueryable<preg_appointment_measurement> GetItemByID(int id)
		{
			return connect.preg_appointment_measurement.Where(c => c.id == id);
		}
		public IQueryable<preg_appointment_measurement> GetItemsByParams(preg_appointment_measurement data)
		{
			IQueryable<preg_appointment_measurement> result = connect.preg_appointment_measurement;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "appointment_id" && propertyValue != null)
				{
					result = result.Where(c => c.appointment_id == (int)(propertyValue));
				}
				else if (propertyName == "bp_dia" && propertyValue != null)
				{
					result = result.Where(c => c.bp_dia == (int)(propertyValue));
				}
				else if (propertyName == "bp_sys" && propertyValue != null)
				{
					result = result.Where(c => c.bp_sys == (int)(propertyValue));
				}
				else if (propertyName == "baby_heart" && propertyValue != null)
				{
					result = result.Where(c => c.baby_heart == (int)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_appointment_measurement item)
		{
			connect.preg_appointment_measurement.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_appointment_measurement item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_appointment_measurement item)
		{
			connect.preg_appointment_measurement.Remove(item);
			connect.SaveChanges();
		}
	}
}