using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PregnancyData.Dao
{
	public class BabyNameDao
	{
		PregnancyEntity connect = null;
		public BabyNameDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_baby_name> GetListItem()
		{
			return connect.preg_baby_name;
		}

		public IQueryable<preg_baby_name> GetItemByID(int id)
		{
			return connect.preg_baby_name.Where(c => c.id == id);
		}

		public IQueryable<preg_baby_name> GetItemsByParams(preg_baby_name data)
		{
			IQueryable<preg_baby_name> results = connect.preg_baby_name;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					results = results.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "country_id" && propertyValue != null)
				{
					results = results.Where(c => c.country_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "gender_id" && propertyValue != null)
				{
					var temp = Convert.ToInt32(propertyValue);
					results = results.Where(c => c.gender_id == temp);
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					results = results.Where(c => c.name == propertyValue.ToString());
				}
				else if (propertyName == "custom_baby_name_by_user_id" && propertyValue != null)
				{
					results = results.Where(c => c.custom_baby_name_by_user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "order" && propertyValue != null)
				{
					results = results.Where(c => c.order == Convert.ToInt32(propertyValue));
				}
			}

			return results;
		}

		public void InsertData(preg_baby_name item)
		{
			connect.preg_baby_name.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_baby_name item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_baby_name item)
		{
			connect.preg_baby_name.Remove(item);
			connect.SaveChanges();
		}

		#region Options method
		public IQueryable FilterUserID(IQueryable<preg_baby_name> filterParams, int user_id)
		{
			IQueryable<preg_user_baby_name> filterUserID = (from ubn in connect.preg_user_baby_name
															where ubn.user_id == user_id
															select ubn);
			var query = (from bn in filterParams
						 join ubn in filterUserID on bn.id equals ubn.baby_name_id
 into joined
						 from j in joined.DefaultIfEmpty()
						 select new
						 {
							 bn.id,
							 bn.country_id,
							 bn.gender_id,
							 bn.name,
							 bn.custom_baby_name_by_user_id,
							 bn.order,
							 user_id = j.user_id.ToString() == "" ? null : user_id.ToString()
						 });
			return query;
		}

		#endregion
	}
}