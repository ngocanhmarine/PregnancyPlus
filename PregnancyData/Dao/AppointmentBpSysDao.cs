using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class AppointmentBpSysDao
	{
		PregnancyEntity connect = null;
		public AppointmentBpSysDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_appointment_bp_sys> GetListItem()
		{
			return connect.preg_appointment_bp_syss;
		}

		public preg_appointment_bp_sys GetItemByID(int id)
		{
			return connect.preg_appointment_bp_syss.Where(c => c.id == id).FirstOrDefault();
		}

		public IEnumerable<preg_appointment_bp_sys> GetItemsByParams(preg_appointment_bp_sys data)
		{
			IEnumerable<preg_appointment_bp_sys> result = connect.preg_appointment_bp_syss;
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
		public void InsertData(preg_appointment_bp_sys item)
		{
			connect.preg_appointment_bp_syss.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_appointment_bp_sys item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_appointment_bp_sys item)
		{
			
			connect.preg_appointment_bp_syss.Remove(item);
			connect.SaveChanges();
		}

	}
}