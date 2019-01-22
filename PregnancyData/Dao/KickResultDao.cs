using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class KickResultDao
	{
		PregnancyEntity connect = null;
		public KickResultDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IEnumerable<preg_kick_result> GetListItem()
		{
			return connect.preg_kick_result;
		}

		public preg_kick_result GetItemByID(int id)
		{
			return connect.preg_kick_result.Where(c => c.id == id).FirstOrDefault();
		}
		public IEnumerable<preg_kick_result> GetItemsByParams(preg_kick_result data)
		{
			IEnumerable<preg_kick_result> result = connect.preg_kick_result;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "kick_order" && propertyValue != null)
				{
					result = result.Where(c => c.kick_order == (int)(propertyValue));
				}
				else if (propertyName == "kick_time" && propertyValue != null)
				{
					result = result.Where(c => c.kick_time == (DateTime)(propertyValue));
				}
				else if (propertyName == "elapsed_time" && propertyValue != null)
				{
					result = result.Where(c => c.elapsed_time == (DateTime)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_kick_result item)
		{
			connect.preg_kick_result.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_kick_result item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_kick_result item)
		{

			connect.preg_kick_result.Remove(item);
			connect.SaveChanges();
		}

	}
}