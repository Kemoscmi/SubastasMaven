using Maven.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Services.Interfaces
{
    public interface IServiceJoya
    {
        Task<List<JoyaListDto>> GetAllAsync();
        Task<JoyaDetailDto?> GetByIdAsync(int id);
    }
}
