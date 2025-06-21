public class FileProcessLog
{
    public int FileProcessLogId { get; set; }
    public string FileName { get; set; }
    public string FileText { get; set; }
    public bool IsSuccess { get; set; } = false;
    public string Separator { get; set; }
    public string Remarks { get; set; }
}