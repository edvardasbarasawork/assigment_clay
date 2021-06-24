using ClayTest.DataAccess.Infrastructure;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ClayTest.DataAccess.Repositories.Infrastructure
{
    public class BaseRepository
    {
        private readonly ConnectionOptions _connectionOptions;

        public BaseRepository(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(QueryObject queryObject)
        {
            using (var connection = new SqlConnection(_connectionOptions.ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync<T>(queryObject.Sql, queryObject.QueryParams);
            }
        }

        protected async Task<T> QuerySingleAsync<T>(QueryObject queryObject)
        {
            using (var connection = new SqlConnection(_connectionOptions.ConnectionString))
            {
                connection.Open();
                return await connection.QuerySingleAsync<T>(queryObject.Sql, queryObject.QueryParams);
            }
        }

        protected async Task<T> QueryFirstOrDefaultAsync<T>(QueryObject queryObject)
        {
            using (var connection = new SqlConnection(_connectionOptions.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<T>(queryObject.Sql, queryObject.QueryParams);
            }
        }

        protected async Task ExecuteAsync(QueryObject queryObject)
        {
            using (var connection = new SqlConnection(_connectionOptions.ConnectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(queryObject.Sql, queryObject.QueryParams);
            }
        }
    }

}
