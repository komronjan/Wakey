using Domain.Dtos.CountDto;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.CountService
{
    public class CountService : ICountService
    {
        private readonly DataContext context;

        public CountService(DataContext context)
        {
            this.context = context;
        }
        public async Task<Response<AddCountDto>> AddCount(AddCountDto model)
        {
            try
            {
                var count = new Count()
                {
                    UserId = model.UserId,
                    Diamonds = model.Diamonds,
                    Steps = model.Steps,
                    ScoreColoriya = model.ScoreColoriya,
                };
                await context.Counts.AddAsync(count);
                await context.SaveChangesAsync();
                return new Response<AddCountDto>(model);
            }
            catch (Exception ex)
            {
                return new Response<AddCountDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<string>> DeleteCount(int id)
        {
            try
            {
                var find = await context.Counts.FindAsync(id);
                context.Counts.Remove(find);
                await context.SaveChangesAsync();
                return new Response<string>("Okey");
            }
            catch (Exception ex) 
            { 
                return new Response<string>(HttpStatusCode.InternalServerError, new List<string>() {ex.Message});
            }
        }

        public async Task<Response<List<GetCountDto>>> GetCount()
        {
            try
            {
                var result = await context.Counts.Select(x => new GetCountDto() {
                    UserId=x.UserId,
                    Diamonds=x.Diamonds,
                    Steps=x.Steps,
                    ScoreColoriya=x.ScoreColoriya,
                    UserName = $"{x.User.FirstName} {x.User.LastName}"
                }).OrderBy(x => x.UserId).ToListAsync();
                return new Response<List<GetCountDto>>(result);
            }
            catch (Exception ex)
            {
                return new Response<List<GetCountDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message});
            }
        }
       
    }
}
