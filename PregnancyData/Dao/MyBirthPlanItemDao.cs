using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyBirthPlanItemDao
	{
		PregnancyEntity connect = null;
		public MyBirthPlanItemDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_birth_plan_item> GetListItem()
		{
			return connect.preg_my_birth_plan_item;
		}

		public preg_my_birth_plan_item GetItemByID(int id)
		{
			return connect.preg_my_birth_plan_item.Where(c => c.id == id).FirstOrDefault();
		}

		public IEnumerable<preg_my_birth_plan_item> GetItemsByParams(preg_my_birth_plan_item data)
		{
			IEnumerable<preg_my_birth_plan_item> result = connect.preg_my_birth_plan_item;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "my_birth_plan_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.my_birth_plan_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "item_content" && propertyValue != null)
				{
					result = result.Where(c => c.item_content == propertyValue.ToString());
				}
				else if (propertyName == "custom_item_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_item_by_user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_my_birth_plan_item item)
		{
			connect.preg_my_birth_plan_item.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_birth_plan_item item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_my_birth_plan_item item)
		{
			connect.preg_my_birth_plan_item.Remove(item);
			connect.SaveChanges();
		}
	}
}