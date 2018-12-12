using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class DailyDao
    {
         PregnancyEntity connect = null;
         public DailyDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_daily> GetListItem()
        {
            return connect.preg_dailys;
        }

        public preg_daily GetItemByID(int id)
        {
            return connect.preg_dailys.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_daily> GetItemsByParams(preg_daily data)
		{
			IEnumerable<preg_daily> result = connect.preg_dailys;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "daily_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.daily_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "hingline_image" && propertyValue != null)
				{
					result = result.Where(c => c.hingline_image == propertyValue.ToString());
				}
				else if (propertyName == "short_description" && propertyValue != null)
				{
					result = result.Where(c => c.short_description == propertyValue.ToString());
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => c.description == propertyValue.ToString());
				}
				else if (propertyName == "daily_relation" && propertyValue != null)
				{
					result = result.Where(c => c.daily_relation == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_daily item)
        {
            connect.preg_dailys.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_daily item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(preg_daily item)
        {
          
            connect.preg_dailys.Remove(item);
            connect.SaveChanges();
        }

    }
}