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

        private IMongoDatabase CreateClient()
        {
            var database = _mongoClient.GetDatabase(DatabaseName);
            return database;
        }

        public Task<CertifiedImage> Load(Guid id)
        {
            var mongoCollection = CreateCollection();
            return mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task Insert(CertifiedImage element)
        {
            var mongoCollection = CreateCollection();
            return mongoCollection.InsertOneAsync(element);
        }

        public Task<long> Count()
        {
            var mongoCollection = CreateCollection();
            return mongoCollection.CountDocumentsAsync(_ => true);
        }

        private IMongoCollection<CertifiedImage> CreateCollection()
        {
            var mongoDatabase = CreateClient();
            var mongoCollection = mongoDatabase.GetCollection<CertifiedImage>(typeof(CertifiedImage).Name);
            return mongoCollection;
        }

        public async Task Delete(Guid id)
        {
            var mongoDatabase = CreateClient();
            var mongoCollection = mongoDatabase.GetCollection<CertifiedImage>(typeof(CertifiedImage).Name);
            await mongoCollection.DeleteOneAsync(i => i.Id == id);
        }
    }
}