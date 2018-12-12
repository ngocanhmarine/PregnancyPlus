using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class AppointmentBpDiaDao
	{
		PregnancyEntity connect = null;
		public AppointmentBpDiaDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_appointment_bp_dia> GetListItem()
		{
			return connect.preg_appointment_bp_dias;
		}

		public preg_appointment_bp_dia GetItemByID(int id)
		{
			return connect.preg_appointment_bp_dias.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_appointment_bp_dia> GetItemsByParams(preg_appointment_bp_dia data)
		{
			IEnumerable<preg_appointment_bp_dia> result = connect.preg_appointment_bp_dias;
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
					result = result.Where(c => c.value == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_appointment_bp_dia item)
		{
			connect.preg_appointment_bp_dias.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_appointment_bp_dia item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_appointment_bp_dia item)
		{
		
			connect.preg_appointment_bp_dias.Remove(item);
			connect.SaveChanges();
		}

	}
}