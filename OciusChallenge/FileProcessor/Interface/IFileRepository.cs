namespace FileProcessor.Interface
{
    public interface IFileRepository
    {
        public void SaveFileLogs(FileProcessLog fileProcessLog);
        public void SaveWordFileDetails(List<LinkFileWordDetails> linkFileWordDetails, FileProcessLog fileLog);


    }
}
