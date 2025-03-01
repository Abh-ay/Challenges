using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

public class SaleDbContext:DbContext
{
    public SaleDbContext(DbContextOptions<SaleDbContext> options) : base(options) { }

    public DbSet<Sale> Sale { get; set; }
}
