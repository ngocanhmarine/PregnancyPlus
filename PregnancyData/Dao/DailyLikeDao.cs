using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class DailyLikeDao
    {  
        PregnancyEntity connect = null;
        public DailyLikeDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_daily_like> GetListItem()
        {
            return connect.preg_daily_likes;
        }

        public preg_daily_like GetItemByID(int id)
        {
            return connect.preg_daily_likes.Where(c => c.id == id).FirstOrDefault();
        }

		public IEnumerable<preg_daily_like> GetItemsByParams(preg_daily_like data)
		{
			IEnumerable<preg_daily_like> result = connect.preg_daily_likes;
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
				else if (propertyName == "like_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.like_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "daily_id" && propertyValue != null)
				{
					result = result.Where(c => c.daily_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_daily_like item)
        {
            connect.preg_daily_likes.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_daily_like item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(preg_daily_like item)
        {
            
            connect.preg_daily_likes.Remove(item);
            connect.SaveChanges();
        }

    }
}