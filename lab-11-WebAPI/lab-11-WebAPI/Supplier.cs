using WebApiModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;

namespace WebEmployees
{
    public class Supp : ISuppliers
    {
        public static ConcurrentDictionary<int, Supplier> suppliersCache;
        private Northwind db;

        public Supp(Northwind db)
        {
            this.db = db;

            if (suppliersCache == null)
            {
                suppliersCache = new ConcurrentDictionary<int, Supplier>(db.Suppliers.ToDictionary(s => s.SupplierId));
            }
        }

        private Supplier? UpdateCache(int id, Supplier supplier)
        {
            Supplier old;
            if (suppliersCache.TryGetValue(id, out old))
            {
                if (suppliersCache.TryUpdate(id, supplier, old))
                {
                    return supplier;
                }
            }
            return null;
        }

        public async Task<Supplier?> CreateAsync(Supplier supplier)
        {
            EntityEntry<Supplier> added = await db.Suppliers.AddAsync(supplier);
            int affectedRows = await db.SaveChangesAsync();
            if (affectedRows > 0)
            {
                return suppliersCache.AddOrUpdate(supplier.SupplierId, supplier, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> DeleteAsync(int supplierId)
        {
            Supplier? c = db.Suppliers.Find(supplierId);
            db.Suppliers.Remove(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return suppliersCache.TryRemove(supplierId, out c);
            }
            else
            {
                return null;
            }
        }

        public Task<Supplier?> GetAsync(int supplierId)
        {
            return Task.Run(() =>
            {
                Supplier? supplier = new Supplier();
                suppliersCache.TryGetValue(supplierId, out supplier);
                if (supplier != null)
                {
                    return supplier;
                }
                else
                {
                    return null;
                }
            });
        }

        public Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return Task.Run<IEnumerable<Supplier>>(() => suppliersCache.Values);
        }

        public async Task<Supplier?> UpdateAsync(int Id, Supplier supplier)
        {
            db.Suppliers.Update(supplier);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // обновление в кэше
                return UpdateCache(Id, supplier);
            }
            else
            {
                return null;
            }
        }
    }
}