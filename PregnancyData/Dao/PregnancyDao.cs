using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class PregnancyDao
    {
         PregnancyEntity connect = null;
        public PregnancyDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_pregnancy> GetListItem()
        {
            return connect.preg_pregnancys;
        }

        public preg_pregnancy GetItemByID(int id)
        {
            return connect.preg_pregnancys.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_pregnancy> GetItemsByParams(preg_pregnancy data)
		{
			IEnumerable<preg_pregnancy> result = connect.preg_pregnancys;
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
				else if (propertyName == "baby_gender" && propertyValue != null)
				{
					result = result.Where(c => c.baby_gender == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "due_date" && propertyValue != null)
				{
					result = result.Where(c => c.due_date == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "show_week" && propertyValue != null)
				{
					result = result.Where(c => c.show_week == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "pregnancy_loss" && propertyValue != null)
				{
					result = result.Where(c => c.pregnancy_loss == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "baby_already_bom" && propertyValue != null)
				{
					result = result.Where(c => c.baby_already_bom == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "date_of_birth" && propertyValue != null)
				{
					result = result.Where(c => c.date_of_birth == Convert.ToDateTime(propertyValue));
				}
				else if (propertyName == "weeks_pregnant" && propertyValue != null)
				{
					result = result.Where(c => c.weeks_pregnant == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_pregnancy item)
        {
            connect.preg_pregnancys.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_pregnancy item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(int id)
        {
            preg_pregnancy item = GetItemByID(id);
            connect.preg_pregnancys.Remove(item);
            connect.SaveChanges();
        }

    }
}