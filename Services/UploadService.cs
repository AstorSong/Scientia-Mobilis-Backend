using Microsoft.EntityFrameworkCore;
using ScientiaMobilis.Data;
using ScientiaMobilis.Models;

namespace ScientiaMobilis.Services
{
    public class UploadService : IUploadService
    {
        private readonly ApplicationDbContext _context;

        public UploadService(ApplicationDbContext context)
            {
                _context = context;
            }

    public async Task<EBook> UploadEBookAsync(EBook ebook)
        {
            ebook.UploadedAt = DateTime.Now;
            _context.EBooks.Add(ebook);
            await _context.SaveChangesAsync();
            return ebook;
        }

    public async Task<List<EBook>> GetAllEBooksAsync()
        {
            return await _context.EBooks.ToListAsync();
        }


    public async Task<bool> DeleteEBookAsync(int id)
        {
            var ebook = await _context.EBooks.FindAsync(id);
            if (ebook == null) return false;

            _context.EBooks.Remove(ebook);
            await _context.SaveChangesAsync();
            return true;
        }

    public async Task<EBook?> UpdateEBookAsync(int id, EBook updatedBook)
    {
        var ebook = await _context.EBooks.FindAsync(id);
        if (ebook == null) return null;

        // update
        ebook.Title = updatedBook.Title;
        ebook.Author = updatedBook.Author;
        ebook.FileUrl = updatedBook.FileUrl;

        await _context.SaveChangesAsync();
        return ebook;
    }



    }

}