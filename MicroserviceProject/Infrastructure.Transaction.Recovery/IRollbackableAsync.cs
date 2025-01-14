﻿
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Transaction.Recovery
{
    /// <summary>
    /// Bir veri seti için transaction işlemleri arayüzü
    /// </summary>
    public interface IRollbackableAsync
    {
        /// <summary>
        /// İşlem yığınını geri almak için yedekleme noktası oluşturur
        /// </summary>
        /// <param name="rollback">Yedeklemenin modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        Task CreateCheckpointAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource);

        /// <summary>
        /// Bir işlemi veri setinden geri alır
        /// </summary>
        /// <param name="rollback">Geri alınacak işlemin yedekleme modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        Task RollbackTransactionAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource);
    }

    /// <summary>
    /// Bir veri seti için transaction işlemleri arayüzü
    /// </summary>
    /// <typeparam name="TIdentity">İşlemin geri dönüş tipi</typeparam>
    public interface IRollbackableAsync<TIdentity>
    {
        /// <summary>
        /// İşlem yığınını geri almak için yedekleme noktası oluşturur
        /// </summary>
        /// <param name="rollback">Yedeklemenin modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns>TIdentity işlemin geri dönüş tipidir</returns>
        Task<TIdentity> CreateCheckpointAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource);

        /// <summary>
        /// Bir işlemi veri setinden geri alır
        /// </summary>
        /// <param name="rollback">Geri alınacak işlemin yedekleme modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns>TIdentity işlemin geri dönüş tipidir</returns>
        Task<TIdentity> RollbackTransactionAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource);
    }
}
