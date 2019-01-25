using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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

		public IQueryable<preg_my_birth_plan_item> GetListItem()
		{
			return connect.preg_my_birth_plan_item;
		}

		public IQueryable<preg_my_birth_plan_item> GetItemByID(int id)
		{
			return connect.preg_my_birth_plan_item.Where(c => c.id == id);
		}

		public IQueryable<preg_my_birth_plan_item> GetItemsByParams(preg_my_birth_plan_item data)
		{
			IQueryable<preg_my_birth_plan_item> result = connect.preg_my_birth_plan_item;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "my_birth_plan_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.my_birth_plan_type_id == (int)(propertyValue));
				}
				else if (propertyName == "item_content" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.item_content) > 0);
				}
				else if (propertyName == "custom_item_by_user_id" && propertyValue != null)
				{
					result = result.Where(c => c.custom_item_by_user_id == (int)(propertyValue));
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