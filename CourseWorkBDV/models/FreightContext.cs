using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.models
{
    public class FreightContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Wagon> Wagons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=TrainCargoManagementSystem;TrustServerCertificate=True;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cargo)
                .WithMany()
                .HasForeignKey(o => o.CargoID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Route)
                .WithMany()
                .HasForeignKey(o => o.RouteID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Order)
                .WithMany()
                .HasForeignKey(s => s.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Train)
                .WithMany()
                .HasForeignKey(s => s.TrainID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Wagon)
                .WithMany()
                .HasForeignKey(s => s.WagonID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.StartStation)
                .WithMany()
                .HasForeignKey(s => s.StartStationID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.EndStation)
                .WithMany()
                .HasForeignKey(s => s.EndStationID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.OriginStation)
                .WithMany()
                .HasForeignKey(r => r.OriginStationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.DestinationStation)
                .WithMany()
                .HasForeignKey(r => r.DestinationStationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Wagon>()
                .HasOne(w => w.AssignedTrain)
                .WithMany()
                .HasForeignKey(w => w.AssignedTrainID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Train)
                .WithMany()
                .HasForeignKey(s => s.TrainID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Route)
                .WithMany()
                .HasForeignKey(s => s.RouteID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}