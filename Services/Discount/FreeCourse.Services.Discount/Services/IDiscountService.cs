﻿using FreeCourse.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<NoContent>> AddAsync(Models.Discount discount);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Models.Discount>>> GetAllAsync();
        Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId);
        Task<Response<Models.Discount>> GetByIdAsync(int id);
        Task<Response<NoContent>> UpdateAsync(Models.Discount discount);
    }
}