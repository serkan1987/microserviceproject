﻿using MicroserviceProject.Model.Logging;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Logging.Sql.Repositories
{
    /// <summary>
    /// Request-Response logları repository sınıfı
    /// </summary>
    public class RequestResponseRepository
    {
        /// <summary>
        /// Veritabanı bağlantı cümlesi
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Request-Response logları repository sınıfı
        /// </summary>
        /// <param name="connectionString">Veritabanı bağlantı cümlesi</param>
        public RequestResponseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Veritabanına bir request-response log kaydı ekler
        /// </summary>
        /// <param name="requestResponseLogModel">Eklenecek logun nesnesi</param>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> InsertLogAsync(RequestResponseLogModel requestResponseLogModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Exception exception = null;
            int result = 0;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[REQUEST_RESPONSE_LOGS]
                                                            (
                                                                [MACHINE_NAME],
                                                                [APPLICATION_NAME],
                                                                [LOG_TEXT],
                                                                [DATE],
                                                                [CONTENT],
                                                                [REQUEST_CONTENT_LENGTH],
                                                                [HOST],
                                                                [IPADDRESS],
                                                                [METHOD],
                                                                [PROTOCOL],
                                                                [RESPONSE_CONTENT_LENGTH],
                                                                [RESPONSE_CONTENT_TYPE],
                                                                [RESPONSE_TIME],
                                                                [STATUS_CODE],
                                                                [URL]
                                                            )
                                                            VALUES
                                                            (
                                                                @MACHINE_NAME,
                                                                @APPLICATION_NAME,
                                                                @LOG_TEXT,
                                                                @DATE,
                                                                @CONTENT,
                                                                @REQUEST_CONTENT_LENGTH,
                                                                @HOST,
                                                                @IPADDRESS,
                                                                @METHOD,
                                                                @PROTOCOL,
                                                                @RESPONSE_CONTENT_LENGTH,
                                                                @RESPONSE_CONTENT_TYPE,
                                                                @RESPONSE_TIME,
                                                                @STATUS_CODE,
                                                                @URL
                                                            )", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@MACHINE_NAME", ((object)requestResponseLogModel.MachineName) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@APPLICATION_NAME", ((object)requestResponseLogModel.ApplicationName) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@LOG_TEXT", ((object) requestResponseLogModel.LogText) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DATE", ((object)requestResponseLogModel.Date) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CONTENT", ((object)requestResponseLogModel.Content) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@REQUEST_CONTENT_LENGTH", ((object)requestResponseLogModel.RequestContentLength) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@HOST", ((object)requestResponseLogModel.Host) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@IPADDRESS", ((object)requestResponseLogModel.IpAddress) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@METHOD", ((object)requestResponseLogModel.Method) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@PROTOCOL", ((object)requestResponseLogModel.Protocol) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@RESPONSE_CONTENT_LENGTH", ((object)requestResponseLogModel.ResponseContentLength) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@RESPONSE_CONTENT_TYPE", ((object)requestResponseLogModel.ResponseContentType) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@RESPONSE_TIME", ((object)requestResponseLogModel.ResponseTime) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@STATUS_CODE", ((object)requestResponseLogModel.StatusCode) ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@URL", ((object)requestResponseLogModel.Url) ?? DBNull.Value);

                if (sqlConnection.State != ConnectionState.Open)
                {
                    await sqlConnection.OpenAsync(cancellationToken);
                }

                result = await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    await sqlConnection.CloseAsync();
                }
            }

            if (exception != null)
            {
                throw exception;
            }

            return result;
        }
    }
}