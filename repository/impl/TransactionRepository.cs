using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class TransactionRepository : ITransactionRepository
    {
        private readonly PlazzoContext _context;

        public TransactionRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.AsNoTracking().ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction?> UpdateAsync(Transaction transaction)
        {
            var existing = await _context.Transactions.FindAsync(transaction.Id);
            if (existing is null) return null;

            existing.FinalPrice = transaction.FinalPrice;
            existing.CompromiseDate = transaction.CompromiseDate;
            existing.DeedDate = transaction.DeedDate;
            existing.Notary = transaction.Notary;
            existing.Status = transaction.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Transactions.FindAsync(id);
            if (existing is null) return false;

            _context.Transactions.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }