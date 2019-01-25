using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class ShoppingCategoryDao
	{
		PregnancyEntity connect = null;
		public ShoppingCategoryDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_shopping_category> GetListItem()
		{
			return connect.preg_shopping_category;
		}

		public IQueryable<preg_shopping_category> GetItemByID(int id)
		{
			return connect.preg_shopping_category.Where(c => c.id == id);
		}
		public IQueryable<preg_shopping_category> GetItemsByParams(preg_shopping_category data)
		{
			IQueryable<preg_shopping_category> result = connect.preg_shopping_category;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.title) > 0);
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == (int)(propertyValue));
				}
				else if (propertyName == "icon" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.icon) > 0);
				}
			}
			return result;
		}
		public void InsertData(preg_shopping_category item)
		{
			connect.preg_shopping_category.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_shopping_category item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_shopping_category item = GetItemByID(id).FirstOrDefault();
			connect.preg_shopping_category.Remove(item);
			connect.SaveChanges();
		}
	}
}