using PregnancyData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace PregnancyService.Service
{
    public class UserService : IUserService
    {
        //private PregnancyEntity db = new PregnancyEntity();

        public List<preg_user> GetListUser()
        {
           return new List<preg_user>();
        }

        public PregnancyData.Entity.preg_user GetUserByID(int id)
        {
            throw new NotImplementedException();
        }
    
IQueryable<preg_user> IUserService.GetListUser()
{
 	throw new NotImplementedException();
}
}
}