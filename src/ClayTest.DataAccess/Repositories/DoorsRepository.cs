using ClayTest.Core.Entities;
using ClayTest.DataAccess.Infrastructure;
using ClayTest.DataAccess.Repositories.Infrastructure;
using ClayTest.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClayTest.DataAccess.Repositories
{
    public class DoorsRepository : BaseRepository, IDoorsRepository
    {
        public DoorsRepository(IOptions<ConnectionOptions> connectionOptions)
            : base(connectionOptions.Value)
        {
        }

        public async Task<IEnumerable<Door>> GetAllAsync()
        {
            return await QueryAsync<Door>(new QueryObject(@"
                select 
                    d.Id as Id,
                    d.Name as Name,
                    d.CompanyId as CompanyId,
                    d.Closed as Closed,
                    d.Latitude as Latitude,
                    d.Longitude as Longitude
                from Doors d
                where d.Deleted is null"));
        }

        public async Task<Door> GetByIdAsync(Guid id)
        {
            return await QueryFirstOrDefaultAsync<Door>(new QueryObject(@"
                select 
                    d.Id as Id,
                    d.Name as Name,
                    d.CompanyId as CompanyId,
                    d.Closed as Closed,
                    d.Latitude as Latitude,
                    d.Longitude as Longitude
                from Doors d
                where d.Deleted is null
                    and d.id = @Id
            ", new { Id = id }));
        }

        public async Task<Door> UpdateAsync(Door door)
        {
            await ExecuteAsync(new QueryObject(@"
	            update Doors 
                set Closed = @Closed
                where Id = @Id
            ", door));

            return door;
        }
    }
}
