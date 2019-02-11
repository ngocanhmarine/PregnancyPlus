using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
	public class GuidesDao
	{
		PregnancyEntity connect = null;
		public GuidesDao()
		{
			connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

		public IQueryable<preg_guides> GetListItem()
		{
			return connect.preg_guides;
		}

		public IQueryable<preg_guides> GetItemByID(int id)
		{
			return connect.preg_guides.Where(c => c.id == id);
		}
		public IQueryable<preg_guides> GetItemsByParams(preg_guides data)
		{
			IQueryable<preg_guides> result = connect.preg_guides;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && (int)(propertyValue) != 0)
				{
					result = result.Where(c => c.id == (int)(propertyValue));
				}
				else if (propertyName == "page_id" && propertyValue != null)
				{
					result = result.Where(c => c.page_id == (int)(propertyValue));
				}
				else if (propertyName == "guides_type_id" && propertyValue != null)
				{
					result = result.Where(c => c.guides_type_id == (int)(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_guides item)
		{
			connect.preg_guides.Add(item);
			connect.SaveChanges();
		}

		public void UpdateData(preg_guides item)
		{
			connect.SaveChanges();
		}

		public void DeleteData(preg_guides item)
		{

			connect.preg_guides.Remove(item);
			connect.SaveChanges();
		}

		public IQueryable FilterJoin(IQueryable<preg_guides> items)
		{
			IQueryable query = (from g in items
								join p in connect.preg_page on g.page_id equals p.id into joined
								from j in joined.DefaultIfEmpty()
								join gt in connect.preg_guides_type on g.guides_type_id equals gt.id into joined2
								from j2 in joined2.DefaultIfEmpty()
								select new { g.id, g.guides_type_id, g.page_id, guides_type = j2.type, j.title, j.content, j.page_image });
			return query;
		}

		public string resultReturn(preg_guides data)
		{
			string result = "{";

			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "preg_guides_type")
				{

				}
				else if (propertyName == "preg_page")
				{

				}
				else
				{
					result += @"""" + propertyName + @""":""" + propertyValue.ToString() + @""",";
				}
			}
			result += "}";
			return result;
		}
	}
}