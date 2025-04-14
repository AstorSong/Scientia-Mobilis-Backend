using ScientiaMobilis.Models;

namespace ScientiaMobilis.Services
{
    public interface IUploadService
    {
        Task<EBook> UploadEBookAsync(EBook ebook);

        Task<List<EBook>> GetAllEBooksAsync();

        Task<bool> DeleteEBookAsync(int id);

        Task<EBook?> UpdateEBookAsync(int id, EBook updatedBook);

    }
}
