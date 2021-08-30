using Asp_Core_with_Angular.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace Asp_Core_with_Angular.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly string _connectionString = null;
        public TicketRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<TicketDetail> Create(TicketDetail ticketDetail)
        {
            ticketDetail.Id = Guid.NewGuid();
            var sqlConnection = new SqlConnection(_connectionString);
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "InsertTicket";
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@id", ticketDetail.Id);
                sqlCommand.Parameters.AddWithValue("@Title", ticketDetail.Title);
                sqlCommand.Parameters.AddWithValue("@Description", ticketDetail.Description);
                sqlCommand.Parameters.AddWithValue("@Type", ticketDetail.Type);
                sqlCommand.Parameters.AddWithValue("@Status", ticketDetail.Status);
                await sqlCommand.ExecuteNonQueryAsync();
                return ticketDetail;
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "DeleteTicket";
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlConnection.Open();
                await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
        }
        public async Task<List<TicketDetail>> GetAll()
        {
            var ticketList = new List<TicketDetail>();
            var sqlConnection = new SqlConnection(_connectionString);
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "ListTickets";
                sqlConnection.Open();
                using (var reader = await sqlCommand.ExecuteReaderAsync())
                {
                    var ticketDetails = await HydrateAsync(reader);
                    if (ticketDetails.Any())
                    {
                        ticketList = ticketDetails;
                    }
                }
            }
            return ticketList;
        }
        public async Task<TicketDetail> GetById(Guid id)
        {
            var ticketDetail = new TicketDetail();
            var sqlConnection = new SqlConnection(_connectionString);
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "GetTicketById";
                sqlConnection.Open();
                using (var reader = await sqlCommand.ExecuteReaderAsync())
                {
                    var ticketDetails = await HydrateAsync(reader);
                    if (ticketDetails.Any())
                    {
                        ticketDetail = ticketDetails.FirstOrDefault();
                    }
                }
            }
            return ticketDetail;
        }
        public async Task<bool> Update(Guid id, TicketDetail ticketDetail)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "UpdateTicket";
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@Title", ticketDetail.Title);
                sqlCommand.Parameters.AddWithValue("@Description", ticketDetail.Description);
                sqlCommand.Parameters.AddWithValue("@Type", ticketDetail.Type);
                sqlCommand.Parameters.AddWithValue("@Status", ticketDetail.Status);
                await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
        }
        private async Task<List<TicketDetail>> HydrateAsync(DbDataReader reader)
        {
            var ticketDetails = new List<TicketDetail>();
            if (reader.HasRows)
            {
                var ordId = reader.GetOrdinal("Id");
                var ordTitle = reader.GetOrdinal("Title");
                var ordDescription = reader.GetOrdinal("Description");
                var ordType = reader.GetOrdinal("Type");
                var ordStatus = reader.GetOrdinal("Status");
                int ordCreated = reader.GetOrdinal("Created");

                while (await reader.ReadAsync())
                {
                    var model = new TicketDetail();
                    model.Id = (Guid)reader.GetValue(ordId);
                    model.Title = (string)reader.GetValue(ordTitle);
                    model.Description = (string)reader.GetValue(ordDescription);
                    model.Type = (string)reader.GetValue(ordType);
                    model.Status = (string)reader.GetValue(ordStatus);
                    model.CreatedDate = (DateTime)reader.GetValue(ordCreated);
                    ticketDetails.Add(model);
                }
            }
            return ticketDetails;
        }
    }
}
