using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService : BaseService<Course>, ICourseService
    {
        private IMongoCollection<Course> _courseCollection;
        private IMongoCollection<Category> _categoryCollection;
        private IDbSettings _dbSettings;
        private IMapper _mapper;

        public CourseService(IDbSettings dbSettings, IMapper mapper)
        {
            _dbSettings = dbSettings;
            _mapper = mapper;
            _courseCollection = ConnectMongoCollection(_dbSettings, CollectionEnum.Courses);
            _categoryCollection = ConnectAnotherMongoCollection<Category>(dbSettings, CollectionEnum.Categories);
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            List<Course> courses = await _courseCollection.Find(course => true).ToListAsync();
            await FillCourseCategories(courses);

            List<CourseDto> coursesForReturn = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(coursesForReturn, (int)HttpStatusCode.OK);
        }
        public async Task<Response<CourseDto>> GetByIdAsync(string courseId)
        {
            Course course = await _courseCollection.Find<Course>(course => course.Id == courseId).FirstOrDefaultAsync();

            if (course is not null)
            {
                course.Category = await _categoryCollection.Find<Category>(c => c.Id == course.CategoryId).FirstOrDefaultAsync();
                CourseDto couserToReturn = _mapper.Map<CourseDto>(course);
                return Response<CourseDto>.Success(couserToReturn, (int)HttpStatusCode.OK);
            }

            return Response<CourseDto>.Fail("Not Found", (int)HttpStatusCode.BadRequest);
        }
        public async Task<Response<List<CourseDto>>> GetAllByUserId(string userId)
        {
            List<Course> courses = await _courseCollection.Find<Course>(course => course.UserId == userId).ToListAsync();
            await FillCourseCategories(courses);

            List<CourseDto> coursesForReturn = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(coursesForReturn, (int)HttpStatusCode.OK);
        }
        public async Task<Response<string>> InsertAsync(CourseCreateDto courseCreateDto)
        {
            Course course = _mapper.Map<Course>(courseCreateDto);
            course.CreatedDate = System.DateTime.Now;
            await _courseCollection.InsertOneAsync(course);

            return Response<string>.Success(course.Id, (int)HttpStatusCode.Created);
        }
        public async Task<Response<string>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            Course course = _mapper.Map<Course>(courseUpdateDto);
            Course result = await _courseCollection.FindOneAndReplaceAsync(c => c.Id == courseUpdateDto.Id, course);

            if (result is not null)
            {
                return Response<string>.Success(course.Id, (int)HttpStatusCode.NoContent);
            }

            return Response<string>.Fail(courseUpdateDto.Id, (int)HttpStatusCode.BadRequest);
        }
        private async Task FillCourseCategories(List<Course> courses)
        {
            if (courses.Any())
            {
                foreach (Course course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(c => c.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
        }
    }
}

