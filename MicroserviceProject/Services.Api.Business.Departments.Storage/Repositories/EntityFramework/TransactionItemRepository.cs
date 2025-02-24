﻿
using Infrastructure.Transaction.Recovery;

using Microsoft.EntityFrameworkCore;

using Services.Api.Business.Departments.Storage.Configuration.Persistence;
using Services.Api.Business.Departments.Storage.Entities.EntityFramework;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.Storage.Repositories.EntityFramework
{
    /// <summary>
    /// Müşteri ilişkileri işlem tablosu için repository sınıfı
    /// </summary>
    public class TransactionItemRepository : BaseRepository<StorageContext, RollbackItemEntity>, IAsyncDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Veritabanı bağlantı nesnesi
        /// </summary>
        private readonly StorageContext _context;

        /// <summary>
        /// Müşteri ilişkileri işlem tablosu için repository sınıfı
        /// </summary>
        /// <param name="context">Veritabanı bağlantı nesnesi</param>
        public TransactionItemRepository(StorageContext context) : base(context)
        {
            _context = context;
        }      

        public override async Task UpdateAsync(int id, RollbackItemEntity entity, CancellationTokenSource cancellationTokenSource)
        {
            RollbackItemEntity rollbackItemEntity = await _context.RollbackItems.FirstOrDefaultAsync(x => x.Id == id, cancellationTokenSource.Token);

            if (rollbackItemEntity != null)
            {
                rollbackItemEntity.Name = entity.Name;
                rollbackItemEntity.NewValue = entity.NewValue;
                rollbackItemEntity.DataSet = entity.DataSet;
                rollbackItemEntity.Identity = entity.Identity;
                rollbackItemEntity.IsRolledback = entity.IsRolledback;
                rollbackItemEntity.OldValue = entity.OldValue;
                rollbackItemEntity.RollbackType = entity.RollbackType;
                rollbackItemEntity.TransactionIdentity = entity.TransactionIdentity;
            }
            else
            {
                throw new Exception("Transaction öğesi bulunamadı");
            }
        }

        public override async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <param name="disposing">Kaynakların serbest bırakılıp bırakılmadığı bilgisi</param>
        public override async Task DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    await _context.DisposeAsync();

                    disposed = true;
                }
            }
        }
    }
}
