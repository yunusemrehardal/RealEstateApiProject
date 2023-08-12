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
    public class EfCategoryDal : ICategoryDal
    {
        private readonly IMongoCollection<Category> _category;

        public EfCategoryDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("CasgemRealEstate");
            _category = database.GetCollection<Category>("Categories");
        }

        public void Delete(Category t)
        {
            _category.DeleteOne(x => x.Id == t.Id);
        }

        public Category GetByID(string id)
        {
            return _category.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Category> GetList()
        {
            return _category.Find(x => true).ToList();
        }

        public void Insert(Category t)
        {
            _category.InsertOne(t);
        }

        public void Update(Category t)
        {
            _category.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
