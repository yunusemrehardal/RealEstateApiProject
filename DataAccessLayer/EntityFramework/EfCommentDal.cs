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
    public class EfCommentDal : ICommentDal
    {
        private readonly IMongoCollection<Comment> _comment;

        public EfCommentDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("CasgemRealEstate");
            _comment = database.GetCollection<Comment>("Comments");
        }

        public void Delete(Comment t)
        {
            _comment.DeleteOne(x => x.Id == t.Id);
        }

        public Comment GetByID(string id)
        {
            return _comment.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Comment> GetList()
        {
            return _comment.Find(x => true).ToList();
        }

        public void Insert(Comment t)
        {
            _comment.InsertOne(t);
        }

        public void Update(Comment t)
        {
            _comment.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
