using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class AppointmentBabyHeartDao
	{
		PregnancyEntity connect = null;
		public AppointmentBabyHeartDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_appointment_baby_heart> GetListItem()
		{
			return connect.preg_appointment_baby_hearts;
		}

		public preg_appointment_baby_heart GetItemByID(int id)
		{
			return connect.preg_appointment_baby_hearts.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_appointment_baby_heart> GetItemsByParams(preg_appointment_baby_heart data)
		{
			IEnumerable<preg_appointment_baby_heart> result = connect.preg_appointment_baby_hearts;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "value" && propertyValue != null)
				{
					result = result.Where(c => c.value == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_appointment_baby_heart item)
		{
			connect.preg_appointment_baby_hearts.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_appointment_baby_heart item)
		{
			connect.SaveChanges();
		}

		public void DeleteData( preg_appointment_baby_heart item)
		{
			
			connect.preg_appointment_baby_hearts.Remove(item);
			connect.SaveChanges();
		}

	}
}