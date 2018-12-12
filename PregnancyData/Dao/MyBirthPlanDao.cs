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
			return connect.preg_my_birth_plans;
		}

		public preg_my_birth_plan GetItemByID(int id)
		{
			return connect.preg_my_birth_plans.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_my_birth_plan> GetItemsByParams(preg_my_birth_plan data)
		{
			IEnumerable<preg_my_birth_plan> result = connect.preg_my_birth_plans;
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
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "icon" && propertyValue != null)
				{
					result = result.Where(c => c.icon == propertyValue.ToString());
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => c.title == propertyValue.ToString());
				}
			}
			return result;
		}
		public void InsertData(preg_my_birth_plan item)
		{
			connect.preg_my_birth_plans.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_birth_plan item)
		{
			connect.SaveChanges();
		}

        public void DeleteData(preg_my_birth_plan item)
		{
			
			connect.preg_my_birth_plans.Remove(item);
			connect.SaveChanges();
		}

	}
}