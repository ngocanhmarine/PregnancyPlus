using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyBirthPlanDao
	{
		PregnancyEntity connect = null;
		public MyBirthPlanDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_birth_plan> GetListItem()
		{
			return connect.preg_my_birth_plan;
		}

		public preg_my_birth_plan GetItemByID(int user_id, int my_birth_plan_item_id)
		{
			return connect.preg_my_birth_plan.Where(c => c.my_birth_plan_item_id == my_birth_plan_item_id && c.user_id == user_id).FirstOrDefault();
		}

		public IEnumerable<preg_my_birth_plan> GetItemByUserID(int user_id)
		{
			return connect.preg_my_birth_plan.Where(c => c.user_id == user_id);
		}

		public IEnumerable<preg_my_birth_plan> GetItemsByParams(preg_my_birth_plan data)
		{
			IEnumerable<preg_my_birth_plan> result = connect.preg_my_birth_plan;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "my_birth_plan_item_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.my_birth_plan_item_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_my_birth_plan item)
		{
			connect.preg_my_birth_plan.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_birth_plan item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_my_birth_plan item)
		{
			connect.preg_my_birth_plan.Remove(item);
			connect.SaveChanges();
		}
	}
}