using Dapper;
using FreeCourse.Shared;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostreSql"));
        }

        public async Task<Response<List<Models.Discount>>> GetAllAsync()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }
        public async Task<Response<Models.Discount>> GetByIdAsync(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE id=@Id", new { Id = id })).SingleOrDefault();
            if (discount is null) return Response<Models.Discount>.Fail("Not Found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE code=@Code AND userid=@UserId",
                                                                          new { Code = code, UserId = userId })).SingleOrDefault();
            if (discount is null) return Response<Models.Discount>.Fail("Not Found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }
        public async Task<Response<NoContent>> AddAsync(Models.Discount discount)
        {
            int effectedRows = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)"
                                                               , discount);

            return effectedRows > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("Error occured", 500);
        }

        public async Task<Response<NoContent>> UpdateAsync(Models.Discount discount)
        {
            int effectedRows = await _dbConnection.ExecuteAsync("UPDATE SET userid=@UserId, rate=@Rate, code=@Code WHERE id = @Id)"
                                                               , discount);

            return effectedRows > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("Error occured", 500);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            int effectedRows = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id = @Id)"
                                                         , new { Id = id });

            return effectedRows > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("Error occured", 500);
        }
    }
}

