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
            return connect.preg_daily;
        }

        public preg_daily GetItemByID(int id)
        {
            return connect.preg_daily.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_daily> GetItemsByParams(preg_daily data)
		{
			IEnumerable<preg_daily> result = connect.preg_daily;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "highline_image" && propertyValue != null)
				{
					result = result.Where(c => c.highline_image == propertyValue.ToString());
				}
				else if (propertyName == "short_description" && propertyValue != null)
				{
					result = result.Where(c => c.short_description == propertyValue.ToString());
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => c.description == propertyValue.ToString());
				}
				else if (propertyName == "daily_blog" && propertyValue != null)
				{
					result = result.Where(c => c.daily_blog == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_daily item)
        {
            connect.preg_daily.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_daily item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(preg_daily item)
        {
          
            connect.preg_daily.Remove(item);
            connect.SaveChanges();
        }

    }
}