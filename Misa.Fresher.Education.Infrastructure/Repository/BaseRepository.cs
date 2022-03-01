using Microsoft.Extensions.Options;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Misa.Fresher.Education.Core.MisaAttribute;
using Misa.Fresher.Education.Core.Setting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
       protected readonly IMongoCollection<MISAEntity> _baseCollection;

       protected readonly IMongoClient _mongoClient;
     /*   private EducationDatabaseSettings educationDatabaseSettings;*/

       /* public BaseRepository(
        EducationDatabaseSettings educationDatabaseSettings)
    {
        _mongoClient = new MongoClient(
            educationDatabaseSettings.Value.ConnectionString);

        var mongoDatabase =_mongoClient.GetDatabase(
            educationDatabaseSettings.Value.DatabaseName);

            _baseCollection = mongoDatabase.GetCollection<MISAEntity>(typeof(MISAEntity).Name);
    }*/
        public BaseRepository(EducationDatabaseSettings educationDatabaseSettings)
        {
            _mongoClient = new MongoClient(educationDatabaseSettings.ConnectionString);
            var mongoDatabase = _mongoClient.GetDatabase(educationDatabaseSettings.DatabaseName);
            _baseCollection = mongoDatabase.GetCollection<MISAEntity>(typeof(MISAEntity).Name);
        }

        public async Task<List<MISAEntity>> GetAll() =>
        await _baseCollection.Find(_ => true).ToListAsync();

    public async Task<MISAEntity?> GetById(Guid entity) =>
        await _baseCollection.Find(new BsonDocument("_id",entity.ToString())).FirstOrDefaultAsync();

    public async Task<MISAEntity> Insert(MISAEntity entity)
        {
            var props = typeof(MISAEntity).GetProperties();
            foreach(var prop in props)
            {
                if(Attribute.IsDefined(prop, typeof(PrimaryKey))) 
                {
                    prop.SetValue(entity, Guid.NewGuid());
                    break;
                }
            } 
             await _baseCollection.InsertOneAsync(entity);
            return entity;
        }
  

   /* public async Task Update(MISAEntity entity, Guid entityId) =>
        await _baseCollection.ReplaceOneAsync(new BsonDocument("_id", entityId.ToString()),entity);
*/
      public async Task<MISAEntity> Update(MISAEntity entity, Guid entityId)
        {
            
            await _baseCollection.ReplaceOneAsync(new BsonDocument("_id", entityId.ToString()), entity);
            return entity;
        }


        public async Task Delete(Guid entityId) =>
        await _baseCollection.DeleteOneAsync(new BsonDocument("_id", entityId.ToString()));

     
    }

}
