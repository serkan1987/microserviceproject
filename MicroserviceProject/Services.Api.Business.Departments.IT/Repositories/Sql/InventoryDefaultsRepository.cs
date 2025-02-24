﻿using Infrastructure.Transaction.UnitOfWork.Sql;

using Services.Api.Business.Departments.IT.Entities.Sql;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.IT.Repositories.Sql
{
    /// <summary>
    /// Envanter varsayılanları tablosu için repository sınıfı
    /// </summary>
    public class InventoryDefaultsRepository : BaseRepository<InventoryDefaultsEntity>, IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Repositorynin ait olduğu tablonun adı
        /// </summary>
        public const string TABLE_NAME = "[dbo].[IT_INVENTORIES_DEFAULTS]";

        /// <summary>
        /// Envanter varsayılanları tablosu için repository sınıfı
        /// </summary>
        /// <param name="unitOfWork">Veritabanı işlemlerini kapsayan iş birimi nesnesi</param>
        public InventoryDefaultsRepository(ISqlUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Envanter varsayılanlarının listesini verir
        /// </summary>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public override async Task<List<InventoryDefaultsEntity>> GetListAsync(CancellationTokenSource cancellationTokenSource)
        {
            List<InventoryDefaultsEntity> defaults = new List<InventoryDefaultsEntity>();

            SqlCommand sqlCommand = new SqlCommand($@"SELECT 
                                                      [ID],
                                                      [IT_INVENTORIES_ID_INVENTORYID],
                                                      [FOR_NEW_WORKER],
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
                        InventoryDefaultsEntity inventoryDefault = new InventoryDefaultsEntity();

                        inventoryDefault.Id = sqlDataReader.GetInt32("ID");
                        inventoryDefault.InventoryId = sqlDataReader.GetInt32("IT_INVENTORIES_ID_INVENTORYID");
                        inventoryDefault.ForNewWorker = sqlDataReader.GetBoolean("FOR_NEW_WORKER");
                        inventoryDefault.DeleteDate =
                            sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("DELETE_DATE"))
                            ?
                            null
                            :
                            sqlDataReader.GetDateTime("DELETE_DATE");

                        defaults.Add(inventoryDefault);
                    }
                }

                return defaults;
            }
        }

        /// <summary>
        /// Yeni envanter varsayılanı oluşturur
        /// </summary>
        /// <param name="inventoryDefault">Oluşturulacak envanter varsayılanı nesnesi</param><
        /// <param name="unitOfWork">Oluşturma esnasında kullanılacak transaction nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public override async Task<int> CreateAsync(InventoryDefaultsEntity inventoryDefault, CancellationTokenSource cancellationTokenSource)
        {
            SqlCommand sqlCommand = new SqlCommand($@"INSERT INTO {TABLE_NAME}
                                                      ([IT_INVENTORIES_ID_INVENTORYID],
                                                      [FOR_NEW_WORKER])
                                                      VALUES
                                                      (@IT_INVENTORIES_ID_INVENTORYID,
                                                      @FOR_NEW_WORKER);
                                                      SELECT CAST(scope_identity() AS int)",
                                                      UnitOfWork.SqlConnection,
                                                      UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@IT_INVENTORIES_ID_INVENTORYID", ((object)inventoryDefault.InventoryId) ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FOR_NEW_WORKER", ((object)inventoryDefault.ForNewWorker) ?? DBNull.Value);

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
        /// Bir envanterin yeni çalışanlar için varsayılan olup olmadığı bilgisini verir
        /// </summary>
        /// <param name="inventoryId">Denetlenecek envanterin Id değeri</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<bool> CheckExistAsync(int inventoryId, CancellationTokenSource cancellationTokenSource)
        {
            SqlCommand sqlCommand = new SqlCommand($@"SELECT
                                                      ID
                                                      FROM {TABLE_NAME}
                                                      WHERE DELETE_DATE IS NULL
                                                      AND
                                                      FOR_NEW_WORKER = 1
                                                      AND
                                                      IT_INVENTORIES_ID_INVENTORYID = @INVENTORY_ID",
                                                      UnitOfWork.SqlConnection,
                                                      UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@INVENTORY_ID", ((object)inventoryId) ?? DBNull.Value);

            using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync(cancellationTokenSource.Token))
            {
                bool exists = sqlDataReader.HasRows;

                return exists;
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
