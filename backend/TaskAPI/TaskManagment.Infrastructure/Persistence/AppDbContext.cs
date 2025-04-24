using Microsoft.EntityFrameworkCore;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectUser> ProjectUsers => Set<ProjectUser>();
    public DbSet<Mission> Missions => Set<Mission>();
    public DbSet<History> Histories => Set<History>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region   Project User iliskisi
        /*
         * ProjectUser tablosu coktan coka iliskiyi temsil eder
         * HasKey PU tablosunun pk yi olarak project id ve userid yi kullanilir
         * HasOne PU entitysi bir project ve bir user ile iliskilidir
         * WithMany project ve user entityleri birden fazla PU ile iliskilendirilebilir.
         */
        modelBuilder.Entity<ProjectUser>().HasKey(pu=> new { pu.ProjectId, pu.UserId });
        
        modelBuilder.Entity<ProjectUser>()
            .HasOne(pu => pu.Project)
            .WithMany(p => p.ProjectUsers)
            .HasForeignKey(pu => pu.ProjectId);
        
        modelBuilder.Entity<ProjectUser>()
            .HasOne(pu => pu.User)
            .WithMany(u => u.ProjectUsers)
            .HasForeignKey(pu => pu.UserId);
        
        #endregion

        
        // mission ile user 

        #region mission
        
        /*
         * Mission ile project arsindaki iliskiyi tanimlar
         * bir mission bir project ile iliskili
         * ve bir project birden fazla mission ile iliskili
         */
        modelBuilder.Entity<Mission>()
            .HasOne(m => m.Project)
            .WithMany(p => p.Missions)
            .HasForeignKey(m => m.ProjectId);
        
        
        /*
         * mission ile assign edilmis user arasindaki iliskidir.
         * missiona atanmis bir useri temsil eder.
         *  kullanici silindiginde ona atanmis gorevler Restrict ile korunur.
         */
        modelBuilder.Entity<Mission>()
            .HasOne(m => m.AssignedUser)
            .WithMany(u => u.AssignedMissions)
            .HasForeignKey(m => m.AssignedUserId)
            .OnDelete(DeleteBehavior.Restrict);
        

        #endregion

        
        
        #region History ilişkileri
        modelBuilder.Entity<History>()
            .HasOne(h => h.Mission)
            .WithMany(m => m.HistoryRecords)
            .HasForeignKey(h => h.MissionId);

        modelBuilder.Entity<History>()
            .HasOne(h => h.User)
            .WithMany()
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<History>()
            .HasOne(h => h.Project)
            .WithMany()
            .HasForeignKey(h => h.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        
        #endregion
    }
}