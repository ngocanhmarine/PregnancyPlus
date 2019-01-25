using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class TimeFrameDao
	{
		PregnancyEntity connect = null;
		public TimeFrameDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_time_frame> GetListItem()
		{
			return connect.preg_time_frame;
		}

		public IQueryable<preg_time_frame> GetItemByID(int id)
		{
			return connect.preg_time_frame.Where(c => c.id == id);
		}
		public IQueryable<preg_time_frame> GetItemsByParams(preg_time_frame data)
		{
			IQueryable<preg_time_frame> result = connect.preg_time_frame;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "frame_title" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.frame_title) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_time_frame item)
		{
			connect.preg_time_frame.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_time_frame item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_time_frame item = GetItemByID(id).FirstOrDefault();
			connect.preg_time_frame.Remove(item);
			connect.SaveChanges();
		}
	}
}