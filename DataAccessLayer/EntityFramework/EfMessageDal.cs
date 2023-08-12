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
    public class EfMessageDal : IMessageDal
    {
        private readonly IMongoCollection<Message> _message;

        public EfMessageDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("CasgemRealEstate");
            _message = database.GetCollection<Message>("Messages");
        }

        public void Delete(Message t)
        {
            _message.DeleteOne(x => x.Id == t.Id);
        }

        public Message GetByID(string id)
        {
            return _message.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Message> GetList()
        {
            return _message.Find(x => true).ToList();
        }

        public void Insert(Message t)
        {
            _message.InsertOne(t);
        }

        public void Update(Message t)
        {
            _message.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
