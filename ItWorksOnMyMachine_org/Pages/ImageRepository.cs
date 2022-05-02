using MongoDB.Bson;
using MongoDB.Driver;

namespace ItWorksOnMyMachine_org.Pages
{
    public class ImageRepository
    {
        private readonly MongoClient _mongoClient;
        public const string DatabaseName = "ItWorksOnMyMachine";

        public ImageRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        protected IMongoDatabase CreateClient()
        {
            var database = _mongoClient.GetDatabase(DatabaseName);
            return database;
        }

        protected Task<CertifiedImage> Load(ObjectId id)
        {
            var mongoCollection = CreateCollection();
            return mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        protected Task Insert(CertifiedImage element)
        {
            var mongoCollection = CreateCollection();
            return mongoCollection.InsertOneAsync(element);
        }

        protected Task<long> Count()
        {
            var mongoCollection = CreateCollection();
            return mongoCollection.CountDocumentsAsync(_ => true);
        }

        protected IMongoCollection<CertifiedImage> CreateCollection()
        {
            var mongoDatabase = CreateClient();
            var mongoCollection = mongoDatabase.GetCollection<CertifiedImage>(typeof(CertifiedImage).Name);
            return mongoCollection;
        }

        protected async Task Delete(ObjectId id)
        {
            var mongoDatabase = CreateClient();
            var mongoCollection = mongoDatabase.GetCollection<CertifiedImage>(typeof(CertifiedImage).Name);
            await mongoCollection.DeleteOneAsync(i => i.Id == id);
        }
    }

    public class CertifiedImage
    {
        public ObjectId Id { get; set; }
    }
}