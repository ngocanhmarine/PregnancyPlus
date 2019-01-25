using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class ImageTypeDao
	{
		PregnancyEntity connect = null;
		public ImageTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_image_type> GetListItem()
		{
			return connect.preg_image_type;
		}

		public IQueryable<preg_image_type> GetItemByID(int id)
		{
			return connect.preg_image_type.Where(c => c.id == id);
		}
		public IQueryable<preg_image_type> GetItemsByParams(preg_image_type data)
		{
			IQueryable<preg_image_type> result = connect.preg_image_type;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.type) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_image_type item)
		{
			connect.preg_image_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_image_type item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_image_type item)
		{

			connect.preg_image_type.Remove(item);
			connect.SaveChanges();
		}

	}
}