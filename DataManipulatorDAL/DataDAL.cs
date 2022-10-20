using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManipulatorModels;

namespace DataManipulatorDAL
{
    public class DataDAL : IDisposable, IDAL<Data>
    {
        private readonly string _connectionString;
        private SqlConnection? _sqlConnection = null;
        private bool disposedValue = false;

        public DataDAL() : this("Data Source = (localdb)\\mssqllocaldb; Integrated Security = true; Initial Catalog = B1Data")
        { }

        public DataDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Create(Data data)
        {
            OpenConnection();
            string sql = "INSERT INTO DATA " +
                "(Date, LatinString, CyrillicString, PositiveEvenInteger, PositiveDouble) " +
                "VALUES " +
                $"(@Date, @LatinString, @CyrillicString, @PositiveEvenInteger, @PositiveDouble)";

            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.CommandType = CommandType.Text;
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Date",
                    Value = data.Date,
                    SqlDbType = SqlDbType.Date,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(parameter);
                parameter = new SqlParameter
                {
                    ParameterName = "@LatinString",
                    Value = data.LatinString,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(parameter);
                parameter = new SqlParameter
                {
                    ParameterName = "@CyrillicString",
                    Value = data.CyrillicString,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(parameter);
                parameter = new SqlParameter
                {
                    ParameterName = "@PositiveEvenInteger",
                    Value = data.PositiveEvenInteger,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(parameter);
                parameter = new SqlParameter
                {
                    ParameterName = "@PositiveDouble",
                    Value = (float)data.PositiveDouble,
                    SqlDbType = SqlDbType.Float,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(parameter);
                await command.ExecuteNonQueryAsync();
                CloseConnection();
            }
           
        }

        public async Task<double> GetMedianOfDouble()
        {
            OpenConnection();

            double median;
            
            using (SqlCommand command = new SqlCommand("MedianFloatingNumbers", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "@Median",
                    SqlDbType = SqlDbType.Float,
                    Direction = ParameterDirection.Output,
                };
                command.Parameters.Add(parameter);

                await command.ExecuteNonQueryAsync();
                median = (double)command.Parameters["@Median"].Value;
                CloseConnection();
            }

            return median;
        }

        public async Task<Int64> GetSumIntegrs()
        {
            OpenConnection();

            Int64 sum;

            using (SqlCommand command = new SqlCommand("SumOfIntegers", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "@Sum",
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.Output,
                };
                command.Parameters.Add(parameter);

                await command.ExecuteNonQueryAsync();
                sum = (Int64)command.Parameters["@Sum"].Value;
                CloseConnection();
            }

            return sum;
        }

        private void OpenConnection()
        {
            _sqlConnection = new SqlConnection
            {
                ConnectionString = _connectionString
            };
            _sqlConnection.Open();
        }
        private void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _sqlConnection != null)
                {
                    _sqlConnection.Dispose();
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
