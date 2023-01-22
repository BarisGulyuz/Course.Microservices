namespace FreeCourse.Services.Catalog.Settings
{
    public interface IDbSettings
    {
        public string CategoryCollectionName { get; set; }
        public string CourseCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseCollectionName { get; set; }
        public string DbName { get; set; }
    }
}
