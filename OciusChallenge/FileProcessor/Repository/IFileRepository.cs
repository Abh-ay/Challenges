namespace FileProcessor.Repository
{
    public interface IFileRepository
    {
        public Task<bool> SaveFileLogsAsync(List<FileProcessLog> fileLogList);
        public Task<List<string>> GetAllFiles();


    }
}
