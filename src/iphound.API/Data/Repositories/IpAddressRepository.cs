using iphound.API.Data.Context;
using iphound.API.Data.Repositories.Interfaces;
using iphound.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace iphound.API.Data.Repositories;

public class IpAddressRepository(AppDbContext context) : Repository<IpAddress>(context), IIpAddressRepository
{
    public async Task<IpAddress?> GetIpAddressWithCountryInfo(string ipAddress)
    {
        var entity = await                                          
            dbSet.Include(x => x.Country)                
                .FirstOrDefaultAsync(x => x.Ip == ipAddress);
        return entity ?? null;
    } 
    
    public async Task<List<IpAddress>?> GetIpAddressListWithCountryInfo(int pageSize = 0, int pageNumber = 1)
    {
        var list = await                                          
            dbSet.Include(x => x.Country)                
                .ToListAsync();
        return list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList() ?? null;
    } 
      
      
}