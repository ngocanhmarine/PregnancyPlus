using PregnancyData.Entity;
using System;
using System.Collections.Generic;
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

		public IEnumerable<preg_shopping_category> GetListItem()
		{
			return connect.preg_shopping_category;
		}

		public preg_shopping_category GetItemByID(int id)
		{
			return connect.preg_shopping_category.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_shopping_category> GetItemsByParams(preg_shopping_category data)
		{
			IEnumerable<preg_shopping_category> result = connect.preg_shopping_category;
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
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
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
			preg_shopping_category item = GetItemByID(id);
			connect.preg_shopping_category.Remove(item);
			connect.SaveChanges();
		}
	}
}