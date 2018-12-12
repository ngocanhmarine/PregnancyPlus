using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class WeeklyNoteDao
	{
		PregnancyEntity connect = null;
		public WeeklyNoteDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_weekly_note> GetListItem()
		{
			return connect.preg_weekly_notes;
		}

		public preg_weekly_note GetItemByID(int id)
		{
			return connect.preg_weekly_notes.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_weekly_note> GetItemsByParams(preg_weekly_note data)
		{
			IEnumerable<preg_weekly_note> result = connect.preg_weekly_notes;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "week_id" && propertyValue != null)
				{
					result = result.Where(c => c.week_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "user_id" && propertyValue != null)
				{
					result = result.Where(c => c.user_id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "photo" && propertyValue != null)
				{
					result = result.Where(c => c.photo == propertyValue.ToString());
				}
				else if (propertyName == "note" && propertyValue != null)
				{
					result = result.Where(c => c.note == propertyValue.ToString());
				}
			}
			return result;
		}

		public void InsertData(preg_weekly_note item)
		{
			connect.preg_weekly_notes.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_weekly_note item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(int id)
		{
			preg_weekly_note item = GetItemByID(id);
			connect.preg_weekly_notes.Remove(item);
			connect.SaveChanges();
		}

	}
}