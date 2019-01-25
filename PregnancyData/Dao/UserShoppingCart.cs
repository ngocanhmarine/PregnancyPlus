using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class UserShoppingCartDao
	{
		PregnancyEntity connect = null;
		public UserShoppingCartDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_user_shopping_cart> GetListItem()
		{
			return connect.preg_user_shopping_cart;
		}

		public IQueryable<preg_user_shopping_cart> GetItemByID(int user_id, int shopping_item_id)
		{
			return connect.preg_user_shopping_cart.Where(c => c.user_id == user_id && c.shopping_item_id == shopping_item_id);
		}

		public IEnumerable<preg_user_shopping_cart> GetItemByUserID(int user_id)
		{
			return connect.preg_user_shopping_cart.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_user_shopping_cart> GetItemByParams(preg_user_shopping_cart data)
		{
			IQueryable<preg_user_shopping_cart> result = connect.preg_user_shopping_cart;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "shopping_item_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.shopping_item_id == (int)(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == (int)(propertyValue));
				}
			}
			return result;
		}

		public bool InsertData(preg_user_shopping_cart item)
		{
			IEnumerable<preg_user_shopping_cart> result = GetItemByParams(item);
			if (result.Count() > 0)
			{
				return false;
			}
			else
			{
				connect.preg_user_shopping_cart.Add(item);
				connect.SaveChanges();
				return true;
			}
		}

		public void UpdateData(preg_user_shopping_cart item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_user_shopping_cart item)
		{
			connect.preg_user_shopping_cart.Remove(item);
			connect.SaveChanges();
		}
	}
}