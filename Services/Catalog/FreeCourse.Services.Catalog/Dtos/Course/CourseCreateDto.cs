using FreeCourse.Services.Catalog.Model;

namespace FreeCourse.Services.Catalog.Dtos
{
    public class CourseCreateDto
    {
        public string CategoryId { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Picture { get; set; }

        public Feature Feature { get; set; }

        public string Description { get; set; }

    }
}
