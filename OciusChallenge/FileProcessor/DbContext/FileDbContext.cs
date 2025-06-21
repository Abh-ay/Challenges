using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class FileDbContext : DbContext
{
    public FileDbContext(DbContextOptions<FileDbContext> options)
       : base(options)
    {
    }
    public DbSet<FileProcessLog> FileProcessLog { get; set; }
    public DbSet<LinkFileWordDetails> LinkFileWordDetail { get; set; }
}
