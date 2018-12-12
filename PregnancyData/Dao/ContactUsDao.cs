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

		public IEnumerable<preg_cotact_us> GetListItem()
		{
			return connect.preg_cotact_uss;
		}

		public preg_cotact_us GetItemByID(int id)
		{
			return connect.preg_cotact_uss.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_cotact_us> GetItemsByParams(preg_cotact_us data)
		{
			IEnumerable<preg_cotact_us> result = connect.preg_cotact_uss;
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
		public void InsertData(preg_cotact_us item)
		{
			connect.preg_cotact_uss.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_cotact_us item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_cotact_us item)
		{
			
			connect.preg_cotact_uss.Remove(item);
			connect.SaveChanges();
		}

	}
}