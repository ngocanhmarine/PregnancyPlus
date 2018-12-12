using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using PregnancyData.Entity;
namespace PregnancyService.Service
{
   public interface IUserService
    {
       IQueryable<preg_user> GetListUser();
       preg_user GetUserByID(int id);
    }
}
