using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class AppointmentDao
	{
		PregnancyEntity connect = null;
		public AppointmentDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_appointment> GetListItem()
		{
			return connect.preg_appointment;
		}

		public preg_appointment GetItemByID(int id)
		{
			return connect.preg_appointment.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_appointment> GetItemsByParams(preg_appointment data)
		{
			IEnumerable<preg_appointment> result = connect.preg_appointment;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					result = result.Where(c => c.name == propertyValue.ToString());
				}
				else if (propertyName == "contact_name" && propertyValue != null)
				{
					result = result.Where(c => c.contact_name == propertyValue.ToString());
				}
				else if (propertyName == "profession_id" && propertyValue != null)
				{
					result = result.Where(c => c.profession_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "appointment_date" && propertyValue != null)
				{
					result = result.Where(c => c.appointment_date == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "appointment_time" && propertyValue != null)
				{
					result = result.Where(c => c.appointment_time == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "my_weight_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.my_weight_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "weight_in_st" && propertyValue != null)
				{
					result = result.Where(c => c.weight_in_st == Convert.ToDouble(propertyValue));
				}
				else if (propertyName == "sync_to_calendar" && propertyValue != null)
				{
					result = result.Where(c => c.sync_to_calendar == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "note" && propertyValue != null)
				{
					result = result.Where(c => c.note == propertyValue.ToString());
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "appointment_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.appointment_type_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_appointment item)
		{
			connect.preg_appointment.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_appointment item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_appointment item)
		{
			
			connect.preg_appointment.Remove(item);
			connect.SaveChanges();
		}

	}
}