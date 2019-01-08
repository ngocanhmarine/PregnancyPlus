using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class SizeGuideDao
	{
		PregnancyEntity connect = null;
		public SizeGuideDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_size_guide> GetListItem()
		{
			return connect.preg_size_guide;
		}

		public preg_size_guide GetItemByWeekID(int week_id)
		{
			return connect.preg_size_guide.Where(c => c.week_id == week_id).FirstOrDefault();
		}

		public IEnumerable<preg_size_guide> GetItemsByParams(preg_size_guide data)
		{
			IEnumerable<preg_size_guide> result = connect.preg_size_guide;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "image" && propertyValue != null)
				{
					result = result.Where(c => c.image == propertyValue.ToString());
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => c.description == propertyValue.ToString());
				}
				else if (propertyName == "week_id" && propertyValue != null)
				{
					result = result.Where(c => c.week_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "length" && propertyValue != null)
				{
					result = result.Where(c => c.length == Convert.ToDouble(propertyValue));
				}
				else if (propertyName == "weight" && propertyValue != null)
				{
					result = result.Where(c => c.weight == Convert.ToDouble(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => c.type == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_size_guide item)
		{
			connect.preg_size_guide.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_size_guide item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_size_guide item = GetItemByWeekID(id);
			connect.preg_size_guide.Remove(item);
			connect.SaveChanges();
		}
	}
}