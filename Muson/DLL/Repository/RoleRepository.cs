using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;

namespace DLL.Repository
{
    public class RoleRepository : BaseRepository<AppRole>
    {
        public RoleRepository(MusonHotelContext _musonHotelContext) : base(_musonHotelContext)
        {
        }

    }
}
