using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyBirthPlanTypeDao
	{
		PregnancyEntity connect = null;
		public MyBirthPlanTypeDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_birth_plan_type> GetListItem()
		{
			return connect.preg_my_birth_plan_type;
		}

		public preg_my_birth_plan_type GetItemByID(int id)
		{
			return connect.preg_my_birth_plan_type.Where(c => c.id == id).FirstOrDefault();
		}

		public IEnumerable<preg_my_birth_plan_type> GetItemsByParams(preg_my_birth_plan_type data)
		{
			IEnumerable<preg_my_birth_plan_type> result = connect.preg_my_birth_plan_type;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "type" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.type) > 0);
				}
				else if (propertyName == "type_icon" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.type_icon) > 0);
				}
			}
			return result;
		}

		public void InsertData(preg_my_birth_plan_type item)
		{
			connect.preg_my_birth_plan_type.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_birth_plan_type item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_my_birth_plan_type item)
		{
			connect.preg_my_birth_plan_type.Remove(item);
			connect.SaveChanges();
		}
	}
}