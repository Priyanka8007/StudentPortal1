using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Data
{
    public class ChallanDbContext : DbContext
    {
        public ChallanDbContext(DbContextOptions<ChallanDbContext> options) : base(options) { }

        public DbSet<PoDtl> PoDtls { get; set; }
        public DbSet<DlvChln> DlvChlns { get; set; }
        public DbSet<LabelTran> LabelTrans { get; set; }
        // Register the DTO as a keyless entity type
        public DbSet<ChallanSplitResultDto> ChallanSplitResults { get; set; }
        // public virtual DbQuery<ChallanSplitResultDto> ChallanSplitResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<ChallanSplitResultDto>().HasNoKey(); // Register as keyless type
            // Configure composite primary key for PoDtl
            modelBuilder.Entity<PoDtl>()
                .HasKey(p => new { p.Pono, p.Posrno });

            // Configure composite primary key for DlvChln
            modelBuilder.Entity<DlvChln>()
                .HasKey(d => new { d.Challanno, d.Challansrno });

            // Configure foreign key relationship between DlvChln and PoDtl
            modelBuilder.Entity<DlvChln>()
                .HasOne(d => d.PoDtl)
                .WithMany(p => p.DlvChlns)
                .HasForeignKey(d => new { d.Pono, d.Posrno })
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
