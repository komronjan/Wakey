using Domain.Dtos.CountDto;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.CountService
{
    public interface ICountService
    {
        Task<Response<List<GetCountDto>>>  GetCount();
        Task<Response<AddCountDto>> AddCount(AddCountDto model);
        Task<Response<string>> DeleteCount (int id);
    }
}
