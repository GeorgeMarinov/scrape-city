namespace ScrapeCity.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using ScrapeCity.Common.Models;
    using ScrapeCity.Common.Models.Brands;
    using ScrapeCity.Common.Models.ImageAddresses;
    using ScrapeCity.Common.Models.Monitors;
    using ScrapeCity.Common.Models.Monitors.MonitorProperties;
    using ScrapeCity.Data.Migrations;
    using System.Data.Entity;

    public class ScrapeCityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ScrapeCityDbContext()
            : base("name=ScrapeCityDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScrapeCityDbContext, Configuration>());
        }

        public virtual DbSet<Monitor> Monitors { get; set; }
        public virtual DbSet<MonitorImageAddress> MonitorImageAddresses { get; set; }
        public virtual DbSet<Speakers> Speakers { get; set; }
        public virtual DbSet<Camera> Cameras { get; set; }
        public virtual DbSet<VideoPort> VideoPorts { get; set; }
        public virtual DbSet<USBPort> USBPorts { get; set; }
        public virtual DbSet<PanelColor> PanelColors { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<CertificateStandartLicense> CertificatesStandartsLicenses { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<UnhandledDIsplaySpecificationProperty> UnhandledDisplaySpecificationProperties { get; set; }

        public static ScrapeCityDbContext Create()
        {
            return new ScrapeCityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Monitor>()
                .HasMany(x => x.VideoPorts)
                .WithRequired()  
                .WillCascadeOnDelete();

            modelBuilder.Entity<Monitor>()
                .HasMany(x => x.USBPorts)
                .WithRequired()
                .WillCascadeOnDelete();

            modelBuilder.Entity<Monitor>()
                .HasMany(x => x.PanelColors)
                .WithRequired()
                .WillCascadeOnDelete();

            modelBuilder.Entity<Monitor>()
                .HasMany(x => x.Features)
                .WithRequired()
                .WillCascadeOnDelete();

            modelBuilder.Entity<Monitor>()
                .HasMany(x => x.CertificatesStandartsLicenses)
                .WithRequired()
                .WillCascadeOnDelete();

            modelBuilder.Entity<Monitor>()
                .HasMany(x => x.UnhandledDisplaySpecificationProperties)
                .WithRequired()
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }
    }
}