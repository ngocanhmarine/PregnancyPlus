using PregnancyData.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace PregnancyData.Dao
{
	public class DailyDao
	{
		PregnancyEntity connect = null;
		public DailyDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_daily> GetListItem()
		{
			return connect.preg_daily;
		}

		public IQueryable<preg_daily> GetItemByID(int id)
		{
			return connect.preg_daily.Where(c => c.id == id);
		}

		public IQueryable<preg_daily> GetItemsByParams(preg_daily data)
		{
			IQueryable<preg_daily> result = connect.preg_daily;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "title" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.title) > 0);
				}
				else if (propertyName == "highline_image" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.highline_image) > 0);
				}
				else if (propertyName == "short_description" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.short_description) > 0);
				}
				else if (propertyName == "description" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.description) > 0);
				}
				else if (propertyName == "daily_blog" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.daily_blog) > 0);
				}
				else if (propertyName == "meta_description" && propertyValue != null)
				{
					result = result.Where(c => SqlFunctions.PatIndex("%" + propertyValue.ToString() + "%", c.meta_description) > 0);
				}
			}
			return result;
		}

		public void InsertData(preg_daily item)
		{
			connect.preg_daily.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_daily item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_daily item)
		{
			connect.preg_daily.Remove(item);
			connect.SaveChanges();
		}

		public IQueryable FilterJoin(IQueryable<preg_daily> items, int user_id)
		{
			IQueryable<preg_daily_interact> dailyInteract = (from di in connect.preg_daily_interact
															 where di.user_id == user_id
															 select di);
			IQueryable query = (from d in items
								join di in dailyInteract on d.id equals di.daily_id into joined
								from j in joined.DefaultIfEmpty()
								join utd in connect.preg_user_todo on new { a = d.id, user_id } equals new { a = utd.todo_id, utd.user_id } into joined2
								from j2 in joined2.DefaultIfEmpty()
								join td in connect.preg_todo on d.id equals td.day_id into joined3
								from j3 in joined3.DefaultIfEmpty()
								select new { d.id, d.title, d.highline_image, d.short_description, d.description, d.daily_blog, d.meta_description, j.like, j.comment, j.share, todo_title = j3.title, todo_user_id = (j2.user_id > 0) ? j2.user_id.ToString() : null });
			return query;
		}
	}
}