using FileProcessor.Interface;

namespace FileProcessor.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly FileDbContext _db;
        public FileRepository(FileDbContext db)
        {
            _db = db;
        }
        public void SaveFileLogs(FileProcessLog fileProcessLog) 
        {
            _db.FileProcessLog.Add(fileProcessLog);
            _db.SaveChanges();
            
        }

        public void SaveWordFileDetails(List<LinkFileWordDetails> linkFileWordDetails,FileProcessLog fileLog)
        {
            _db.LinkFileWordDetail.AddRange(linkFileWordDetails);
            fileLog.IsSuccess = true;
            _db.SaveChanges();

        }
    }
}
