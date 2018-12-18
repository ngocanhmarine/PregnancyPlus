using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class TimeLineDao
	{
		PregnancyEntity connect = null;
		public TimeLineDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_time_line> GetListItem()
		{
			return connect.preg_time_line;
		}

		public preg_time_line GetItemByID(int id)
		{
			return connect.preg_time_line.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_time_line> GetItemsByParams(preg_time_line data)
		{
			IEnumerable<preg_time_line> result = connect.preg_time_line;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "week_id" && propertyValue != null)
				{
					result = result.Where(c => c.week_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "image" && propertyValue != null)
				{
					result = result.Where(c => c.image == propertyValue.ToString());
				}
				else if (propertyName == "position" && propertyValue != null)
				{
					result = result.Where(c => c.position == propertyValue.ToString());
				}
				else if (propertyName == "time_frame_id" && propertyValue != null)
				{
					result = result.Where(c => c.time_frame_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_time_line item)
		{
			connect.preg_time_line.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_time_line item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_time_line item = GetItemByID(id);
			connect.preg_time_line.Remove(item);
			connect.SaveChanges();
		}
	}
}