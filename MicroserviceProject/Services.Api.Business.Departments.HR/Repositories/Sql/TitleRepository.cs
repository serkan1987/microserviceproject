﻿using Infrastructure.Transaction.Recovery;
using Infrastructure.Transaction.UnitOfWork.Sql;

using Services.Api.Business.Departments.HR.Entities.Sql;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.HR.Repositories.Sql
{
    /// <summary>
    /// Ünvan tablosu için repository sınıfı
    /// </summary>
    public class TitleRepository : BaseRepository<TitleEntity>, IRollbackableDataAsync<int>, IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Repositorynin ait olduğu tablonun adı
        /// </summary>
        public const string TABLE_NAME = "[dbo].[HR_TITLES]";

        /// <summary>
        /// Ünvan tablosu için repository sınıfı
        /// </summary>
        /// <param name="unitOfWork">Veritabanı işlemlerini kapsayan iş birimi nesnesi</param>
        public TitleRepository(ISqlUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Ünvanların listesini verir
        /// </summary>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public override async Task<List<TitleEntity>> GetListAsync(CancellationTokenSource cancellationTokenSource)
        {
            List<TitleEntity> titles = new List<TitleEntity>();

            SqlCommand sqlCommand = new SqlCommand($@"SELECT 
                                                      [ID],
                                                      [NAME],
                                                      [DELETE_DATE]
                                                      FROM {TABLE_NAME}
                                                      WHERE DELETE_DATE IS NULL",
                                                                 UnitOfWork.SqlConnection,
                                                                 UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync(cancellationTokenSource.Token))
            {
                if (sqlDataReader.HasRows)
                {
                    while (await sqlDataReader.ReadAsync(cancellationTokenSource.Token))
                    {
                        TitleEntity title = new TitleEntity();

                        title.Id = sqlDataReader.GetInt32("ID");
                        title.Name = sqlDataReader.GetString("NAME");

                        titles.Add(title);
                    }
                }

                return titles;
            }
        }

        /// <summary>
        /// Yeni ünvan oluşturur
        /// </summary>
        /// <param name="title">Oluşturulacak ünvan nesnesi</param><
        /// <param name="unitOfWork">Oluşturma esnasında kullanılacak transaction nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public override async Task<int> CreateAsync(TitleEntity title, CancellationTokenSource cancellationTokenSource)
        {
            SqlCommand sqlCommand = new SqlCommand($@"INSERT INTO {TABLE_NAME}
                                                      ([NAME])
                                                      VALUES
                                                      (@NAME);
                                                      SELECT CAST(scope_identity() AS int)",
                                                        UnitOfWork.SqlConnection,
                                                        UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@NAME", ((object)title.Name) ?? DBNull.Value);

            return (int)await sqlCommand.ExecuteScalarAsync(cancellationTokenSource.Token);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <param name="disposing">Kaynakların serbest bırakılıp bırakılmadığı bilgisi</param>
        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    UnitOfWork.Dispose();

                    disposed = true;
                }
            }
        }

        /// <summary>
        /// Bir Id değerine sahip envanteri silindi olarak işaretler
        /// </summary>
        /// <param name="id">Silindi olarak işaretlenecek envanterin Id değeri</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int id, CancellationTokenSource cancellationTokenSource)
        {
            SqlCommand sqlCommand = new SqlCommand($@"UPDATE {TABLE_NAME}
                                                      SET DELETE_DATE = GETDATE()
                                                      WHERE ID = @ID",
                                                       UnitOfWork.SqlConnection,
                                                       UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@ID", id);

            return (int)await sqlCommand.ExecuteNonQueryAsync(cancellationTokenSource.Token);
        }

        /// <summary>
        /// Silindi olarak işaretlenmiş bir envanter kaydının işaretini kaldırır
        /// </summary>
        /// <param name="id">Silindi işareti kaldırılacak envanter kaydının Id değeri</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> UnDeleteAsync(int id, CancellationTokenSource cancellationTokenSource)
        {
            SqlCommand sqlCommand = new SqlCommand($@"UPDATE {TABLE_NAME}
                                                      SET DELETE_DATE = NULL
                                                      WHERE ID = @ID",
                                                                 UnitOfWork.SqlConnection,
                                                                 UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@ID", id);

            return (int)await sqlCommand.ExecuteNonQueryAsync(cancellationTokenSource.Token);
        }

        /// <summary>
        /// Bir envanter kaydındaki bir kolon değerini değiştirir
        /// </summary>
        /// <param name="id">Değeri değiştirilecek envanterin Id değeri</param>
        /// <param name="name">Değeri değiştirilecek kolonun adı</param>
        /// <param name="value">Yeni değer</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> SetAsync(int id, string name, object value, CancellationTokenSource cancellationTokenSource)
        {
            SqlCommand sqlCommand = new SqlCommand($@"UPDATE {TABLE_NAME}
                                                      SET {name.ToUpper()} = @VALUE
                                                      WHERE ID = @ID",
                                                                    UnitOfWork.SqlConnection,
                                                                    UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@ID", id);
            sqlCommand.Parameters.AddWithValue("@VALUE", value);

            return (int)await sqlCommand.ExecuteNonQueryAsync(cancellationTokenSource.Token);
        }
    }
}
