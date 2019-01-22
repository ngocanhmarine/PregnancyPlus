using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class HelpCategoryDao
	{
		PregnancyEntity connect = null;
		public HelpCategoryDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_help_category> GetListItem()
		{
			return connect.preg_help_category;
		}

		public preg_help_category GetItemByID(int id)
		{
			return connect.preg_help_category.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_help_category> GetItemsByParams(preg_help_category data)
		{
			IEnumerable<preg_help_category> result = connect.preg_help_category;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.name) > 0);
				}
				else if (propertyName == "highline_image" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.highline_image) > 0);
				}
				else if (propertyName == "order" && propertyValue != null)
				{
					result = result.Where(c => c.order == (int)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_help_category item)
		{
			connect.preg_help_category.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_help_category item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_help_category item)
		{
			connect.preg_help_category.Remove(item);
			connect.SaveChanges();
		}

	}
}