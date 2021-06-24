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
    public class DoorEventLogsRepository : BaseRepository, IDoorEventLogsRepository
    {
        public DoorEventLogsRepository(IOptions<ConnectionOptions> connectionOptions)
            : base(connectionOptions.Value)
        {
        }

        public async Task<IEnumerable<DoorEventLog>> GetAllAsync()
        {
            return await QueryAsync<DoorEventLog>(new QueryObject(@"
                select top 50
                    del.Id as Id,
                    del.Event as Event,
                    del.UserId as UserId,
                    del.DoorId as DoorId,
                    del.Created as Created
                from DoorEventLogs del
                order by Created desc "));
        }

        public async Task<IEnumerable<DoorEventLog>> GetByDoorIdAsync(Guid doorId)
        {
            return await QueryAsync<DoorEventLog>(new QueryObject(@"
                select top 50
                    del.Id as Id,
                    del.Event as Event,
                    del.UserId as UserId,
                    del.DoorId as DoorId,
                    del.Created as Created
                from DoorEventLogs del
                where del.DoorId = @DoorId
                order by Created desc 
            ", new { DoorId = doorId }));
        }

        public async Task<DoorEventLog> CreateAsync(DoorEventLog doorEventLog)
        {
            return await QuerySingleAsync<DoorEventLog>(new QueryObject(@"
                insert into DoorEventLogs (Id, Event, UserId, DoorId, Created)
                output inserted.Id, inserted.Event, inserted.UserId, inserted.DoorId, inserted.Created
                values (newid(), @Event, @UserId, @DoorId, getdate())
            ", doorEventLog));
        }

    }
}
