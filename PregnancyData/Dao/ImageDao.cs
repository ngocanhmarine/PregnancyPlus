using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class ImageDao
	{
		PregnancyEntity connect = null;
		public ImageDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_image> GetListItem()
		{
			return connect.preg_image;
		}

		public preg_image GetItemByID(int id)
		{
			return connect.preg_image.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_image> GetItemsByParams(preg_image data)
		{
			IEnumerable<preg_image> result = connect.preg_image;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "image_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.image_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "image" && propertyValue != null)
				{
					result = result.Where(c => c.image == propertyValue.ToString());
				}
				else if (propertyName == "week_id" && propertyValue != null)
				{
					result = result.Where(c => c.week_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_image item)
		{
			connect.preg_image.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_image item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_image item)
		{
			connect.preg_image.Remove(item);
			connect.SaveChanges();
		}
	}
}