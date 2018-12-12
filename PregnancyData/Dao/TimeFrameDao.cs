using PregnancyData.Entity;
using System;
using System.Collections.Generic;
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

		public IEnumerable<preg_time_frame> GetListItem()
		{
			return connect.preg_time_frames;
		}

		public preg_time_frame GetItemByID(int id)
		{
			return connect.preg_time_frames.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_time_frame> GetItemsByParams(preg_time_frame data)
		{
			IEnumerable<preg_time_frame> result = connect.preg_time_frames;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "frame_title" && propertyValue != null)
				{
					result = result.Where(c => c.frame_title == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_time_frame item)
		{
			connect.preg_time_frames.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_time_frame item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_time_frame item = GetItemByID(id);
			connect.preg_time_frames.Remove(item);
			connect.SaveChanges();
		}

	}
}