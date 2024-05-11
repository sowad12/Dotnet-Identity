//using Dapper;
//using Dapper.Contrib.Extensions;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Options;
//using System.Data;
//using System.Text.Json;

//namespace WC.Addon.ClubeezWF.Library.Infrastructure
//{
//    public class DapperContext : IDapperContext, IAsyncDisposable, IDisposable
//    {
//        private readonly DatabaseOptions _databaseOptions;

//        private readonly SqlConnection _connection;

//        private Utf8JsonWriter _jsonWriter;

//        private bool _disposed = false;

//        public DapperContext(IOptions<DatabaseOptions> databaseOptions)
//        {
//            _databaseOptions = databaseOptions.Value;
//            _connection = new SqlConnection(_databaseOptions.ConnectionString);
//            TryOpenConnection();
//        }

//        public DapperContext(string connectionString)
//        {
//            _connection = new SqlConnection(connectionString);
//            TryOpenConnection();
//        }

//        private void TryOpenConnection()
//        {
//            try
//            {
//                if (_connection.State != ConnectionState.Open)
//                {
//                    _connection.Open();
//                }
//            }
//            catch (Exception)
//            {
//            }
//        }

//        public async ValueTask DisposeAsync()
//        {
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            await _connection.DisposeAsync();
//        }

//        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
//        {
//            IEnumerable<T> result = await _connection.QueryAsync<T>(sql, parameters);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return result;
//        }

//        public async Task<T> First<T>(string sql, object parameters = null)
//        {
//            T item = await _connection.QueryFirstAsync<T>(sql, parameters);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return item;
//        }

//        public async Task<T> FirstOrDefault<T>(string sql, object parameters = null)
//        {
//            T item = await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return item;
//        }

//        public async Task<T> Single<T>(string sql, object parameters = null)
//        {
//            T item = await _connection.QuerySingleAsync<T>(sql, parameters);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return item;
//        }

//        public async Task<T> SingleOrDefault<T>(string sql, object parameters = null)
//        {
//            T item = await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return item;
//        }

//        public async Task<int> RunCommand(string sql, object parameters = null)
//        {
//            int result = 0;
//            using SqlTransaction transaction = _connection.BeginTransaction();
//            try
//            {
//                result = await _connection.ExecuteAsync(sql, parameters, transaction);
//                await transaction.CommitAsync();
//            }
//            catch (Exception)
//            {
//                await transaction.RollbackAsync();
//            }

//            return result;
//        }

//        public async Task<int> CreateAsync<T>(T value) where T : class
//        {
//            var result = await _connection.InsertAsync<T>(value);
//            if (_connection.State == ConnectionState.Open) _connection.Close();
//            return result;
//        }

//        public async Task<bool> UpdateAsync<T>(T value) where T : class
//        {
//            var result = await _connection.UpdateAsync<T>(value);
//            if (_connection.State == ConnectionState.Open) _connection.Close();
//            return result;
//        }

//        public async Task<bool> DeleteAsync<T>(T value) where T : class
//        {
//            var result = await _connection.DeleteAsync<T>(value);
//            return result;
//        }

//        public List<T> StoredProcedureQuery<T>(string sql, object parameters = null)
//        {
//            if (_connection.State == ConnectionState.Closed)
//                _connection.Open();
//            IEnumerable<T> source = _connection.Query<T>(sql, parameters, null, buffered: true, commandType: CommandType.StoredProcedure, commandTimeout: 300);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return source.ToList();
//        }

//        public async Task<List<T>> StoredProcedureQueryAsync<T>(string sql, object parameters = null)
//        {
//            IEnumerable<T> result = await _connection.QueryAsync<T>(sql, parameters, null, commandType: CommandType.StoredProcedure, commandTimeout: 300);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return result.ToList();
//        }

//        public async Task<T> StoredProcedureQueryFirst<T>(string sql, object parameters = null)
//        {
//            T result = await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters, null, commandType: CommandType.StoredProcedure, commandTimeout: 300);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return result;
//        }

//        public async Task<int> StoredProcedureCommand(string sql, object parameters = null)
//        {
//            int result = await _connection.ExecuteAsync(sql, parameters, null, commandType: CommandType.StoredProcedure, commandTimeout: 300);
//            if (_connection.State == ConnectionState.Open)
//            {
//                _connection.Close();
//            }

//            return result;
//        }

//        public void Dispose()
//        {
//            Dispose(disposing: true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!_disposed)
//            {
//                if (disposing)
//                {
//                    _jsonWriter?.Dispose();
//                }

//                _disposed = true;
//            }
//        }
//    }
//}
