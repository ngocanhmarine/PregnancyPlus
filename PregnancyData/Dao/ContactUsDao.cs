using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class ContactUsDao
	{
		PregnancyEntity connect = null;
		public ContactUsDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_contact_us> GetListItem()
		{
			return connect.preg_contact_us;
		}

		public preg_contact_us GetItemByID(int id)
		{
			return connect.preg_contact_us.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_contact_us> GetItemsByParams(preg_contact_us data)
		{
			IEnumerable<preg_contact_us> result = connect.preg_contact_us;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "email" && propertyValue != null)
				{
					result = result.Where(c => c.email == propertyValue.ToString());
				}
				else if (propertyName == "message" && propertyValue != null)
				{
					result = result.Where(c => c.message == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_contact_us item)
		{
			connect.preg_contact_us.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_contact_us item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_contact_us item)
		{
			connect.preg_contact_us.Remove(item);
			connect.SaveChanges();
		}

	}
}