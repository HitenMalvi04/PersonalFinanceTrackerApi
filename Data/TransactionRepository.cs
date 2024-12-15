using PersonalFinanceTrackerAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace PersonalFinanceTrackerAPI.Data
{
    public class TransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get All Transactions
        public List<Transaction> GetAllTransactions()
        {
            var transactions = new List<Transaction>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Transactions_SelectAll", connection);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            Type = reader["Type"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"])
                        });
                    }
                }
            }

            return transactions;
        }

        // Get Transaction by Id
        public Transaction GetTransactionById(int id)
        {
            Transaction transaction = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Transactions_SelectByPK", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        transaction = new Transaction
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            Type = reader["Type"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"])
                        };
                    }
                }
            }

            return transaction;
        }

        // Insert a Transaction
        public void InsertTransaction(Transaction transaction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Transactions_Insert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Title", transaction.Title);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Type", transaction.Type);
                command.Parameters.AddWithValue("@Date", transaction.Date);

                command.ExecuteNonQuery();
            }
        }

        // Update a Transaction
        public void UpdateTransaction(Transaction transaction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Transactions_Update", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", transaction.Id);
                command.Parameters.AddWithValue("@Title", transaction.Title);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Type", transaction.Type);
                command.Parameters.AddWithValue("@Date", transaction.Date);

                command.ExecuteNonQuery();
            }
        }

        // Delete a Transaction
        public void DeleteTransaction(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Transactions_Delete", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
