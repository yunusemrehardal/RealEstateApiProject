using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfUserDal : IUserDal
    {
        private readonly IMongoCollection<User> _user;

        public EfUserDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("CasgemRealEstate");
            _user = database.GetCollection<User>("Users");
        }

        public void Delete(User t)
        {
            _user.DeleteOne(x => x.Id == t.Id);
        }

        public User GetByID(string id)
        {
            return _user.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<User> GetList()
        {
            return _user.Find(x => true).ToList();
        }

        public void Insert(User t)
        {
            _user.InsertOne(t);
        }

        public void Update(User t)
        {
            _user.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
