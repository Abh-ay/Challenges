public class LinkFileWordDetails
{
    public int LinkFileWordDetailsId { get; set; }
    public string Word { get; set; }
    public int Count { get; set; }

    public int FileProcessLogId { get; set; }
    public FileProcessLog FileProcessLog { get; set; }
}