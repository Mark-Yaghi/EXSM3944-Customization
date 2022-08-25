using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Demo.Models;

namespace MVC_Demo.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<InventoryProduct> Inventoryproducts { get; set; } = null!;
        public virtual DbSet<OrderInventory> Orderinventories { get; set; } = null!;
        public virtual DbSet<OrderInvoice> Orderinvoices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=mvc_exercise", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");
            });

            modelBuilder.Entity<InventoryProduct>(entity =>
            {
                entity.ToTable("inventoryproduct");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Qoh)
                    .HasColumnType("int(11)")
                    .HasColumnName("qoh");
            });

            modelBuilder.Entity<OrderInventory>(entity =>
            {
                entity.ToTable("orderinventory");

                entity.HasIndex(e => e.Inventoryid, "FK_OrderInventory_InventoryProduct");

                entity.HasIndex(e => e.Orderid, "FK_OrderInventory_OrderInvoice");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Inventoryid)
                    .HasColumnType("int(11)")
                    .HasColumnName("inventoryid");

                entity.Property(e => e.Orderid)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderid");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderInventories)
                    .HasForeignKey(d => d.Inventoryid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderInventory_InventoryProduct");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderInventories)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderInventory_OrderInvoice");
            });

            modelBuilder.Entity<OrderInvoice>(entity =>
            {
                entity.ToTable("orderinvoice");

                entity.HasIndex(e => e.Customerid, "FK_OrderInvoice_Customer");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Customerid)
                    .HasColumnType("int(11)")
                    .HasColumnName("customerid");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderInvoices)
                    .HasForeignKey(d => d.Customerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderInvoice_Customer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
