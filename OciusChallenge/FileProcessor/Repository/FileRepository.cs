using Microsoft.EntityFrameworkCore;

namespace FileProcessor.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly FileDbContext _db;
        public FileRepository(FileDbContext db)
        {
            _db = db;
        }
        public async Task<bool> SaveFileLogsAsync(List<FileProcessLog> fileLogList) 
        {
            // For save the data in both the tables 
            _db.FileProcessLog.AddRange(fileLogList);
            return await _db.SaveChangesAsync() > 0;
            
        }

        // As we get all files which processed
        // It's heavy operations we need to optimize with given input files list as input params
        public async Task<List<string>> GetAllFiles()
        {
            return await _db.FileProcessLog.Select(i=>i.FileName).ToListAsync();

        }
    }
}
