using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MemberDatabase;
using System.Configuration;

namespace Platform.Services
{
    public class DataService // where TEntity : class
    {

        private static string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static MemberContext mem = new MemberContext(connection);
        private GenericRepository.UnitOfWork unitOfWork = new GenericRepository.UnitOfWork(mem);

        public bool Insert(Type objType, Object obj) 
        {

            //            unitOfWork.Repository<MemberDatabase.Member>().Insert(obj);
            return true;

        }


    }
}