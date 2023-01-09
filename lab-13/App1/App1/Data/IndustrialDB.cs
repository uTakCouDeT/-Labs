using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using App1.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace App1.Data
{
    public class IndustrialDB
    {
        readonly SQLiteAsyncConnection db;

        public IndustrialDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<Good>().Wait();
            db.CreateTableAsync<Company>().Wait();
            db.CreateTableAsync<Recipient>().Wait();
        }
        //---------------Good-------------------------------
        public Task<List<Good>> GetGoodsAsync()
        {
            return db.Table<Good>().ToListAsync();
        }
        public Task<Good> GetGoodAsync(int id)
        {
            return db.Table<Good>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveGoodAsync(Good good)
        {
            if (good.Id != 0)
            {
                return db.UpdateAsync(good);
            }
            else
            {
                return db.InsertAsync(good);
            }
        }
        public Task<int> DeleteGoodAsync(Good good)
        {
            return db.DeleteAsync(good);
        }
        //------------------Comapny----------------------
        public Task<List<Company>> GetCompaniesAsync()
        {
            return db.Table<Company>().ToListAsync();
        }
        public Task<Company> GetCompanyAsync(int id)
        {
            return db.Table<Company>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveCompanyAsync(Company company)
        {
            if (company.Id != 0)
            {
                return db.UpdateAsync(company);
            }
            else
            {
                return db.InsertAsync(company);
            }
        }
        public async Task<int> DeleteCompanyAsync(Company company)
        {
            var list =await db.Table<Good>()
                .Where(i => i.NameOfCompany != null)
                .ToListAsync();
            
            foreach(var item in list)
            {
                await db.DeleteAsync(item);
            }
            return await db.DeleteAsync(company);
        }
        //-------------------Recipient--------------------------
        public Task<List<Recipient>> GetRecipientsAsync()
        {
            return db.Table<Recipient>().ToListAsync();
        }
        public Task<Recipient> GetRecipientAsync(int id)
        {
            return db.Table<Recipient>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveRecipientAsync(Recipient rec)
        {
            if (rec.Id != 0)
            {
                return db.UpdateAsync(rec);
            }
            else
            {
                return db.InsertAsync(rec);
            }
        }
        public async Task<int> DeleteRecipientAsync(Recipient rec)
        {
            var list = await db.Table<Good>()
                .Where(i => i.NameOfRecipient == rec.FullName)
                .ToListAsync();
            foreach (var item in list)
            {
                await db.DeleteAsync(item);
            }
            return await db.DeleteAsync(rec);
        }
    }
}
