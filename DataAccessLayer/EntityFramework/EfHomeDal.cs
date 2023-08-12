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
    public class EfHomeDal : IHomeDal
    {
        private readonly IMongoCollection<Home> _home;

        public EfHomeDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("RealEstateDB");
            _home = database.GetCollection<Home>("Homes");
        }

        public void Delete(Home t)
        {
            _home.DeleteOne(x => x.Id == t.Id);
        }

        public Home GetByID(string id)
        {
            return _home.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Home> GetList()
        {
            return _home.Find(x => true).ToList();
        }

        public void Insert(Home t)
        {
            _home.InsertOne(t);
        }

        public void Update(Home t)
        {
            _home.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
