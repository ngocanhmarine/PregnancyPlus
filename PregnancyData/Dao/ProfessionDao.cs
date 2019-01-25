using PregnancyData.Entity;
using System.Collections.Generic;
using System.Linq;

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

		public IQueryable<preg_profession> GetListItem()
		{
			return connect.preg_profession;
		}

		public IQueryable<preg_profession> GetItemByID(int user_id, int profession_type_id)
		{
			return connect.preg_profession.Where(c => c.user_id == user_id & c.profession_type_id == profession_type_id);
		}

		public IQueryable<preg_profession> GetItemsByUserID(int user_id)
		{
			return connect.preg_profession.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_profession> GetItemsByParams(preg_profession data)
		{
			IQueryable<preg_profession> result = connect.preg_profession;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "profession_type_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.profession_type_id == (int)(propertyValue));
				}
				else if (propertyName == "status" && propertyValue != null)
				{
					result = result.Where(c => c.status == (int)(propertyValue));
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
			preg_profession item = GetItemByID(user_id, profession_type_id).FirstOrDefault();
			connect.preg_profession.Remove(item);
			connect.SaveChanges();
		}
	}
}