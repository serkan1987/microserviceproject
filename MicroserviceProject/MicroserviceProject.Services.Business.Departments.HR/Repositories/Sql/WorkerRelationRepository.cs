﻿using MicroserviceProject.Services.Business.Departments.HR.Entities.Sql;
using MicroserviceProject.Services.Transaction;
using MicroserviceProject.Services.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceProject.Services.Business.Departments.HR.Repositories.Sql
{
    /// <summary>
    /// Çalışan ilişkileri tablosu için repository sınıfı
    /// </summary>
    public class WorkerRelationRepository : BaseRepository<WorkerRelationEntity>, IRollbackableDataAsync<int>, IDisposable
    {
        /// <summary>
        /// Repositorynin ait olduğu tablonun adı
        /// </summary>
        public const string TABLE_NAME = "[dbo].[HR_WORKERRELATIONS]";

        /// <summary>
        /// Çalışan ilişkileri tablosu için repository sınıfı
        /// </summary>
        /// <param name="unitOfWork">Veritabanı işlemlerini kapsayan iş birimi nesnesi</param>
        public WorkerRelationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Çalışan ilişkilerinin listesini verir
        /// </summary>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public override async Task<List<WorkerRelationEntity>> GetListAsync(CancellationToken cancellationToken)
        {
            List<WorkerRelationEntity> workerRelations = new List<WorkerRelationEntity>();

            SqlCommand sqlCommand = new SqlCommand($@"SELECT [ID]
                                                      [WORKERS_ID],
                                                      [HR_WORKERS_MANAGER_ID],
                                                      [FROM_DATE],
                                                      [TO_DATE]
                                                      FROM {TABLE_NAME}
                                                      WHERE DELETE_DATE IS NULL",
                                                     UnitOfWork.SqlConnection,
                                                     UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync(cancellationToken);

            if (sqlDataReader.HasRows)
            {
                while (await sqlDataReader.ReadAsync(cancellationToken))
                {
                    WorkerRelationEntity workerRelation = new WorkerRelationEntity();

                    workerRelation.Id = sqlDataReader.GetInt32("ID");
                    workerRelation.WorkerId = sqlDataReader.GetInt32("HR_WORKERS_ID");
                    workerRelation.ManagerId = sqlDataReader.GetInt32("HR_WORKERS_MANAGER_ID");
                    workerRelation.FromDate = sqlDataReader.GetDateTime("FROM_DATE");
                    workerRelation.ToDate =
                        sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("TO_DATE"))
                        ?
                        null
                        :
                        sqlDataReader.GetDateTime("TO_DATE");

                    workerRelations.Add(workerRelation);
                }
            }

            return workerRelations;
        }

        /// <summary>
        /// Yeni çalışan ilişkisi oluşturur
        /// </summary>
        /// <param name="workerRelation">Oluşturulacak çalışan ilişkisi nesnesi</param><
        /// <param name="unitOfWork">Oluşturma esnasında kullanılacak transaction nesnesi</param>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public override async Task<int> CreateAsync(WorkerRelationEntity workerRelation, CancellationToken cancellationToken)
        {
            SqlCommand sqlCommand = new SqlCommand($@"INSERT INTO {TABLE_NAME}
                                                      ([WORKERS_ID]
                                                      [HR_WORKERS_MANAGER_ID]
                                                      [FROM_DATE]
                                                      [TO_DATE])
                                                      VALUES
                                                      (@HR_WORKERS_ID,
                                                      @HR_WORKERS_MANAGER_ID,
                                                      @FROM_DATE,
                                                      @TO_DATE);
                                                      SELECT CAST(scope_identity() AS int)",
                                                     UnitOfWork.SqlConnection,
                                                     UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@HR_WORKERS_ID", ((object)workerRelation.WorkerId) ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@HR_WORKERS_MANAGER_ID", ((object)workerRelation.ManagerId) ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FROM_DATE", ((object)workerRelation.FromDate) ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@TO_DATE", ((object)workerRelation.ToDate) ?? DBNull.Value);

            return (int)await sqlCommand.ExecuteScalarAsync(cancellationToken);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <param name="disposing">Kaynakların serbest bırakılıp bırakılmadığı bilgisi</param>
        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Disposed)
                {
                    UnitOfWork.Dispose();

                    Disposed = true;
                }
            }
        }

        /// <summary>
        /// Bir Id değerine sahip envanteri silindi olarak işaretler
        /// </summary>
        /// <param name="id">Silindi olarak işaretlenecek envanterin Id değeri</param>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            SqlCommand sqlCommand = new SqlCommand($@"UPDATE {TABLE_NAME}
                                                      SET DELETE_DATE = GETDATE()
                                                      WHERE ID = @ID",
                                                     UnitOfWork.SqlConnection,
                                                     UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@ID", id);

            return (int)await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        /// <summary>
        /// Silindi olarak işaretlenmiş bir envanter kaydının işaretini kaldırır
        /// </summary>
        /// <param name="id">Silindi işareti kaldırılacak envanter kaydının Id değeri</param>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> UnDeleteAsync(int id, CancellationToken cancellationToken)
        {
            SqlCommand sqlCommand = new SqlCommand($@"UPDATE {TABLE_NAME}
                                                      SET DELETE_DATE = NULL
                                                      WHERE ID = @ID",
                                                              UnitOfWork.SqlConnection,
                                                              UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@ID", id);

            return (int)await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        /// <summary>
        /// Bir envanter kaydındaki bir kolon değerini değiştirir
        /// </summary>
        /// <param name="id">Değeri değiştirilecek envanterin Id değeri</param>
        /// <param name="name">Değeri değiştirilecek kolonun adı</param>
        /// <param name="value">Yeni değer</param>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> SetAsync(int id, string name, object value, CancellationToken cancellationToken)
        {
            SqlCommand sqlCommand = new SqlCommand($@"UPDATE {TABLE_NAME}
                                                      SET {name.ToUpper()} = @VALUE
                                                      WHERE ID = @ID",
                                                                  UnitOfWork.SqlConnection,
                                                                  UnitOfWork.SqlTransaction);

            sqlCommand.Transaction = UnitOfWork.SqlTransaction;

            sqlCommand.Parameters.AddWithValue("@ID", id);
            sqlCommand.Parameters.AddWithValue("@VALUE", value);

            return (int)await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
