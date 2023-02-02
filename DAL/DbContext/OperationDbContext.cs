using App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.DataContext;

public class OperationDbContext : DbContext
{
    public OperationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<usersModel> Users { get; set; }

    public DbSet<OrderTableModel> orderHeader { get; set; }

    public DbSet<OrderDetialsModel> OrderDetial { get; set; }

    public DbSet<itemsTableModel> Items { get; set; }

    public DbSet<categoryModel> category { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderTableModel>()
           .HasOne<usersModel>()
           .WithMany(o => o.orders)
           .HasForeignKey(f => f.UserID);

        modelBuilder.Entity<OrderDetialsModel>()
           .HasOne<OrderTableModel>()
           .WithMany(d => d.orderDetials).HasForeignKey(f => f.OrderID)
           .OnDelete(DeleteBehavior.Cascade); ;

        modelBuilder.Entity<OrderDetialsModel>()
            .HasOne<itemsTableModel>()
            .WithMany().HasForeignKey(f => f.ItemID)
            .OnDelete(DeleteBehavior.Cascade); ;

        modelBuilder.Entity<itemsTableModel>()
            .HasOne<categoryModel>(c => c.category)
            .WithMany(i => i.items)
            .HasForeignKey(f => f.categoryid);

        modelBuilder.Entity<OrderDetialsModel>().ToTable("OrderDetials");

        modelBuilder.Entity<OrderTableModel>().ToTable("OrderHeader");

        modelBuilder.Entity<usersModel>().ToTable("Users");
    }
}
