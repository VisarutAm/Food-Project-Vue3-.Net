using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<FileRecord>>GetAllAsync();

        Task<FileRecord?> DeleteAsync(string id);
    }
}