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

		public IQueryable<preg_kick_result> GetListItem()
		{
			return connect.preg_kick_result;
		}

		public IQueryable<preg_kick_result> GetItemByID(int id)
		{
			return connect.preg_kick_result.Where(c => c.id == id);
		}
		public IQueryable<preg_kick_result> GetItemsByParams(preg_kick_result data)
		{
			IQueryable<preg_kick_result> result = connect.preg_kick_result;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "kick_date" && propertyValue != null)
				{
					result = result.Where(c => c.kick_date == (DateTime)(propertyValue));
				}
				else if (propertyName == "duration" && propertyValue != null)
				{
					result = result.Where(c => c.duration == (float)(propertyValue));
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

		/// <summary>
		/// Filter the result to display only with the user_id provided
		/// </summary>
		/// <param name="items"></param>
		/// <param name="user_id"></param>
		/// <returns></returns>
		public IQueryable FilterJoin(IQueryable<preg_kick_result> items, int user_id)
		{
			IQueryable query = (from ks in items
								join ukh in connect.preg_user_kick_history on new { ks.id, user_id } equals new { id = ukh.kick_result_id, ukh.user_id }
								select ks);
			return query;
		}
	}
}