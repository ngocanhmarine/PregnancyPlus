using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class ShoppingItemDao
	{
		PregnancyEntity connect = null;
		public ShoppingItemDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_shopping_item> GetListItem()
		{
			return connect.preg_shopping_item;
		}

		public preg_shopping_item GetItemByID(int id)
		{
			return connect.preg_shopping_item.Where(c => c.id == id).FirstOrDefault();
		}

		public IEnumerable<preg_shopping_item> GetItemsByParams(preg_shopping_item data)
		{
			IEnumerable<preg_shopping_item> result = connect.preg_shopping_item;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "item_name" && propertyValue != null)
				{
					result = result.Where(c => c.item_name == propertyValue.ToString());
				}
				else if (propertyName == "custom_item_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_item_by_user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "category_id" && propertyValue != null)
				{
					result = result.Where(c => c.category_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_shopping_item item)
		{
			connect.preg_shopping_item.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_shopping_item item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_shopping_item item = GetItemByID(id);
			connect.preg_shopping_item.Remove(item);
			connect.SaveChanges();
		}
	}
}