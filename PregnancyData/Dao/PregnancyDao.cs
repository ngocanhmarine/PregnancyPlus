using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class PregnancyDao
	{
		PregnancyEntity connect = null;
		public PregnancyDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_pregnancy> GetListItem()
		{
			return connect.preg_pregnancy;
		}

		public IQueryable<preg_pregnancy> GetItemByID(int id)
		{
			return connect.preg_pregnancy.Where(c => c.id == id);
		}
		public IQueryable<preg_pregnancy> GetItemsByParams(preg_pregnancy data)
		{
			IQueryable<preg_pregnancy> result = connect.preg_pregnancy;
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
				else if (propertyName == "baby_gender" && propertyValue != null)
				{
					result = result.Where(c => c.baby_gender == (int)(propertyValue));
				}
				else if (propertyName == "due_date" && propertyValue != null)
				{
					result = result.Where(c => c.due_date == (DateTime)(propertyValue));
				}
				else if (propertyName == "show_week" && propertyValue != null)
				{
					result = result.Where(c => c.show_week == (int)(propertyValue));
				}
				else if (propertyName == "pregnancy_loss" && propertyValue != null)
				{
					result = result.Where(c => c.pregnancy_loss == (int)(propertyValue));
				}
				else if (propertyName == "baby_already_born" && propertyValue != null)
				{
					result = result.Where(c => c.baby_already_born == (int)(propertyValue));
				}
				else if (propertyName == "date_of_birth" && propertyValue != null)
				{
					result = result.Where(c => c.date_of_birth == (DateTime)(propertyValue));
				}
				else if (propertyName == "weeks_pregnant" && propertyValue != null)
				{
					result = result.Where(c => c.weeks_pregnant == (int)(propertyValue));
				}
				else if (propertyName == "start_date" && propertyValue != null)
				{
					result = result.Where(c => c.start_date == (DateTime)(propertyValue));
				}
			}
			return result;
		}

		public void InsertData(preg_pregnancy item)
		{
			connect.preg_pregnancy.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_pregnancy item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int user_id)
		{
			preg_pregnancy item = GetItemsByParams(new preg_pregnancy() { user_id = user_id }).FirstOrDefault();
			connect.preg_pregnancy.Remove(item);
			connect.SaveChanges();
		}
	}
}