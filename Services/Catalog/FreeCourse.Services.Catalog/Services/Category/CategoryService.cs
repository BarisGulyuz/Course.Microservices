using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private IMongoCollection<Category> _categoryCollection;
        private IDbSettings _dbSettings;
        private IMapper _mapper;
        public CategoryService(IDbSettings dbSettings, IMapper mapper)
        {
            _dbSettings = dbSettings;
            _mapper = mapper;
            _categoryCollection = ConnectMongoCollection(_dbSettings, CollectionEnum.Categories);
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            List<Category> categories = await _categoryCollection.Find(category => true).ToListAsync();
            List<CategoryDto> categoriesToRetun = _mapper.Map<List<CategoryDto>>(categories);

            return Response<List<CategoryDto>>.Success(categoriesToRetun, (int)HttpStatusCode.OK);

        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string categoryId)
        {
            Category category = await _categoryCollection.Find<Category>(c => c.Id == categoryId).FirstOrDefaultAsync();
            if (category is not null)
            {
                CategoryDto categororyToReturn = _mapper.Map<CategoryDto>(category);
                return Response<CategoryDto>.Success(categororyToReturn, (int)HttpStatusCode.OK);
            }

            return Response<CategoryDto>.Fail("Not Found", (int)HttpStatusCode.BadRequest);

        }
        public async Task<Response<string>> InsertAsync(CategoryDto categoryDto)
        {

            Category category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);

            return Response<string>.Success(category.Id, (int)HttpStatusCode.Created);

        }
        //private void ConnectCategoryCollection()
        //{
        //    var client = new MongoClient(_dbSettings.ConnectionString);
        //    var database = client.GetDatabase(_dbSettings.DbName);
        //    _categoryCollection = database.GetCollection<Category>(_dbSettings.CategoryCollectionName);
        //}
    }
}
