using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class HomeManager : IHomeService
    {
        private readonly IHomeDal _homeDal;

        public HomeManager(IHomeDal homeDal)
        {
            _homeDal = homeDal;
        }

        public void TDelete(Home t)
        {
            _homeDal.Delete(t);
        }

        public Home TGetByID(string id)
        {
            return _homeDal.GetByID(id);
        }

        public List<Home> TGetList()
        {
            return _homeDal.GetList();
        }

        public void TInsert(Home t)
        {
            _homeDal.Insert(t);
        }

        public void TUpdate(Home t)
        {
            _homeDal.Update(t);
        }
    }
}
