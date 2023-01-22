using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Settings;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public abstract class BaseService<T>
    {
        private MongoClient mongoClient = null;
        public IMongoCollection<T> ConnectMongoCollection(IDbSettings settings, CollectionEnum collectionEnum)
        {
            return ConnectMonngoCollection<T>(settings, collectionEnum);
        }

        public IMongoCollection<T2> ConnectAnotherMongoCollection<T2>(IDbSettings settings, CollectionEnum collectionEnum)
        {
            return ConnectMonngoCollection<T2>(settings, collectionEnum);
        }

        private IMongoCollection<TCollection> ConnectMonngoCollection<TCollection>(IDbSettings settings, CollectionEnum collectionEnum)
        {
            if (mongoClient is null)
            {
                mongoClient = new MongoClient(settings.ConnectionString);
            }
            var database = mongoClient.GetDatabase(settings.DbName);
            return database.GetCollection<TCollection>(GetCollectionName(settings, collectionEnum));
        }

        private string GetCollectionName(IDbSettings dbsettings, CollectionEnum @enum)
        {
            string collectionName = null;
            switch (@enum)
            {
                case CollectionEnum.Categories:
                    collectionName = dbsettings.CategoryCollectionName;
                    break;
                case CollectionEnum.Courses:
                    collectionName = dbsettings.CourseCollectionName;
                    break;
            }

            return collectionName;
        }
    }
}
