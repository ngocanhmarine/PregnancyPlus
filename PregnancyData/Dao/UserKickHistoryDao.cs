using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PregnancyData.Dao
{
	public class UserKickHistoryDao
	{
		PregnancyEntity connect = null;
		public UserKickHistoryDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_user_kick_history> GetListItem()
		{
			return connect.preg_user_kick_history;
		}

		public IQueryable<preg_user_kick_history> GetItemByID(int user_id, int kick_result_id)
		{
			return connect.preg_user_kick_history.Where(c => c.user_id == user_id && c.kick_result_id == kick_result_id);
		}

		public IQueryable<preg_user_kick_history> GetItemByUserID(int user_id)
		{
			return connect.preg_user_kick_history.Where(c => c.user_id == user_id);
		}

		public IQueryable<preg_user_kick_history> GetItemByParams(preg_user_kick_history data)
		{
			IQueryable<preg_user_kick_history> result = connect.preg_user_kick_history;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "user_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.user_id == (int)(propertyValue));
				}
				else if (propertyName == "kick_result_id" && (int)propertyValue != 0)
				{
					result = result.Where(c => c.kick_result_id == (int)(propertyValue));
				}
			}
			return result;
		}

		public bool InsertData(preg_user_kick_history item)
		{
			IEnumerable<preg_user_kick_history> result = GetItemByParams(item);
			if (result.Count() > 0)
			{
				return false;
			}
			else
			{
				connect.preg_user_kick_history.Add(item);
				connect.SaveChanges();
				return true;
			}
		}

		public void UpdateData(preg_user_kick_history item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_user_kick_history item)
		{
			connect.preg_user_kick_history.Remove(item);
			connect.SaveChanges();
		}

		public IQueryable FilterJoin(IQueryable<preg_user_kick_history> items, int user_id)
		{
			var query = (from uk in items
						 join krd in (from uk in items
									  join krd in connect.preg_kick_result_detail on uk.kick_result_id equals krd.kick_result_id
									  group krd by krd.kick_result_id into joined
									  from j in joined.DefaultIfEmpty().Distinct()
									  select new { count = joined.Count() > 0 ? joined.Count() : 0, j.kick_result_id }).Distinct() on uk.kick_result_id equals krd.kick_result_id into joinedKrd
						 from j in joinedKrd.DefaultIfEmpty()
						 join k in connect.preg_kick_result on uk.kick_result_id equals k.id into joined2
						 from j2 in joined2.DefaultIfEmpty()
						 select new { uk.user_id, uk.kick_result_id, j2.kick_date, j2.duration, kick = j.count > 0 ? j.count : 0 });
			return query;
		}
	}
}