using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class ProfessionDao
	{
		PregnancyEntity connect = null;
		public ProfessionDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_profession> GetListItem()
		{
			return connect.preg_profession;
		}

		public preg_profession GetItemByID(int user_id, int profession_type_id)
		{
			return connect.preg_profession.Where(c => c.user_id == user_id & c.profession_type_id == profession_type_id).FirstOrDefault();
		}

		public IEnumerable<preg_profession> GetItemsByUserID(int user_id)
		{
			return connect.preg_profession.Where(c => c.user_id == user_id);
		}

		public IEnumerable<preg_profession> GetItemsByParams(preg_profession data)
		{
			IEnumerable<preg_profession> result = connect.preg_profession;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "profession_type_id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.profession_type_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_profession item)
		{
			connect.preg_profession.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_profession item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int user_id, int profession_type_id)
		{
			preg_profession item = GetItemByID(user_id, profession_type_id);
			connect.preg_profession.Remove(item);
			connect.SaveChanges();
		}

	}
}