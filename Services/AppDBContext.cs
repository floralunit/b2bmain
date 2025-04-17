namespace B2BWebService.Services;

using B2BWebService.DBModels;
using B2BWebService.ResponseRequestModels;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ContractorInfo> ContractorInfo { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }
    public DbSet<PartsInfo> PartsInfo { get; set; }
    public DbSet<CreatedOrderInfo> CreatedOrderInfo { get; set; }
    public DbSet<PricingSchemeInfo> PricingSchemeInfo { get; set; }
    public DbSet<ContractorSiteList> ContractorSiteList { get; set; }
    
    public DbSet<OrderInfo> OrderInfo { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContractorInfo>()
            .ToTable("ContractorInfo", "b2b")
            .HasKey(e => e.ContractorInfo_ID);
        modelBuilder.Entity<UserInfo>()
            .HasNoKey();
        modelBuilder.Entity<PartsInfo>()
            .HasNoKey();
        modelBuilder.Entity<CreatedOrderInfo>()
            .HasNoKey();
        modelBuilder.Entity<ContractorSiteList>()
           .HasNoKey();
        modelBuilder.Entity<PricingSchemeInfo>()
            .HasKey(e => e.PricingScheme_ID);
        modelBuilder.Entity<OrderInfo>()
           .HasKey(e => e.DocumentRowBase_ID);
    }
}


