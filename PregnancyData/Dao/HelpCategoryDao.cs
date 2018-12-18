using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PregnancyData.Dao
{
    public class HelpCategoryDao
    {
         PregnancyEntity connect = null;
         public HelpCategoryDao()
        {
            connect = new PregnancyEntity();
			connect.Configuration.ProxyCreationEnabled = false;
		}

        public IEnumerable<preg_help_category> GetListItem()
        {
            return connect.preg_help_category;
        }

        public preg_help_category GetItemByID(int id)
        {
            return connect.preg_help_category.Where(c => c.id == id).FirstOrDefault();
        }
		public IEnumerable<preg_help_category> GetItemsByParams(preg_help_category data)
		{
			IEnumerable<preg_help_category> result = connect.preg_help_category;
			for (int i = 0; i < data.GetType().GetProperties().ToList().Count(); i++)
			{
				string propertyName = data.GetType().GetProperties().ToList()[i].Name;
				var propertyValue = data.GetType().GetProperty(propertyName).GetValue(data, null);
				if (propertyName == "id" && Convert.ToInt32(propertyValue) != 0)
				{
					result = result.Where(c => c.id == Convert.ToInt32(propertyValue));
				}
				else if (propertyName == "name" && propertyValue != null)
				{
					result = result.Where(c => c.name == propertyValue.ToString());
				}
				else if (propertyName == "highline_image" && propertyValue != null)
				{
					result = result.Where(c => c.highline_image == propertyValue.ToString());
				}
				else if (propertyName == "order" && propertyValue != null)
				{
					result = result.Where(c => c.order == Convert.ToInt32(propertyValue));
				}
			}
			return result;
		}
		public void InsertData(preg_help_category item)
        {
            connect.preg_help_category.Add(item);
            connect.SaveChanges();
        }

        public void UpdateData(preg_help_category item)
        {
            connect.SaveChanges();
        }

        public void DeleteData(preg_help_category item)
        {
            connect.preg_help_category.Remove(item);
            connect.SaveChanges();
        }

    }
}