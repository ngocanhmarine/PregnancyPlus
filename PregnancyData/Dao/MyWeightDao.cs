using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class MyWeightDao
	{
		PregnancyEntity connect = null;
		public MyWeightDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_my_weight> GetListItem()
		{
			return connect.preg_my_weight;
		}

		public preg_my_weight GetItemByID(int id)
		{
			return connect.preg_my_weight.Where(c => c.id == id).FirstOrDefault();
		}

		public IEnumerable<preg_my_weight> GetItemsByParams(preg_my_weight data)
		{
			IEnumerable<preg_my_weight> result = connect.preg_my_weight;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;

				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);

				if (propertyName == "id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "my_weight_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.my_weight_type_id == (int)(propertyValue));
				}
				else if (propertyName == "start_date" && propertyValue != null)
				{
					result = result.Where(c => c.start_date == (DateTime)(propertyValue));
				}
				else if (propertyName == "pre_pregnancy_weight" && propertyValue != null)
				{
					result = result.Where(c => c.pre_pregnancy_weight == (double)(propertyValue));
				}
				else if (propertyName == "current_weight" && propertyValue != null)
				{
					result = result.Where(c => c.current_weight == (double)(propertyValue));
				}
				else if (propertyName == "week_id" && propertyValue != null)
				{
					result = result.Where(c => c.week_id == (int)(propertyValue));
				}
				else if (propertyName == "current_date" && propertyValue != null)
				{
					result = result.Where(c => c.current_date == (DateTime)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_my_weight item)
		{
			connect.preg_my_weight.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_my_weight item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_my_weight item)
		{
			connect.preg_my_weight.Remove(item);
			connect.SaveChanges();
		}
	}
}