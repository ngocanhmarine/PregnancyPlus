using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class PhoneDao
    {
        PregnancyEntity connect = null;
        public PhoneDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_phone> GetListItem()
        {
            return connect.preg_phone;
        }

        public preg_phone GetItemByID(int id)
        {
            return connect.preg_phone.Where(c => c.id == id).FirstOrDefault();
        }

		public IEnumerable<preg_phone> GetItemsByParams(preg_phone data)
		{
			IEnumerable<preg_phone> result = connect.preg_phone;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "profession_id" && propertyValue != null)
				{
					result = result.Where(c => c.profession_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "phone_number" && propertyValue != null)
				{
					result = result.Where(c => c.phone_number == propertyValue.ToString());
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_phone item)
        {
            connect.preg_phone.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_phone item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(int id)
        {
            preg_phone Phone = GetItemByID(id);
            connect.preg_phone.Remove(Phone);
            connect.SaveChanges();
        }

    }
}