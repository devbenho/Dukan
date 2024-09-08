using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Domain.ValueObjects;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
               builder.Property(x => x.FirstName)
                      .HasConversion(x => x.Value, v => FirstName.Create(v).Value)
                      .IsRequired()
                      .HasMaxLength(50);

               builder.Property(x => x.LastName)
                      .HasConversion(x => x.Value, v => LastName.Create(v).Value)
                      .IsRequired()
                      .HasMaxLength(50);

               builder.Property(x => x.Email)
                      .HasConversion(x => x.Value, v => Email.Create(v).Value)
                      .IsRequired()
                      .HasMaxLength(100);

               builder.Property(x => x.UserName)
                      .HasConversion(x => x.Value, v => Username.Create(v).Value)
                      .IsRequired()
                      .HasMaxLength(50);

               builder.Property(x => x.Password)
                      .HasConversion(x=> x.Value, v => Password.Create(v).Value)
                      .IsRequired();

               builder.Property(x => x.PhoneNumber)
                      .HasConversion(x => x.Value, v => PhoneNumber.Create(v).Value)
                      .HasMaxLength(15);

               builder.Property(x => x.IsAdmin);

            builder.Property(x => x.IsAdmin)
                   .IsRequired();

            builder.Property(x => x.IsSuperAdmin)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .IsRequired();

            builder.Property(x => x.Created)
                   .IsRequired();

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(100);

            builder.Property(x => x.LastModified)
                   .IsRequired();

            builder.Property(x => x.LastModifiedBy)
                   .HasMaxLength(100);

            // Relationships
            builder.OwnsMany(x => x.Addresses, a =>
            {
                a.WithOwner().HasForeignKey("UserId");
                a.ToTable("UserAddresses"); // Specify the table name if you want it to be different from the default
                a.Property<Guid>("Id"); // Define an explicit shadow property to act as a primary key
                a.HasKey("Id"); // Use the shadow property as the primary key
                a.Property(ad => ad.Street).IsRequired();
                a.Property(ad => ad.City).IsRequired();
                a.Property(ad => ad.State).IsRequired();
                a.Property(ad => ad.Country).IsRequired();
                a.Property(ad => ad.ZipCode).IsRequired();
                a.Property(ad => ad.Type).IsRequired();
            });

            builder.OwnsMany(x => x.Roles, r =>
            {
                r.WithOwner().HasForeignKey("UserId");
                r.Property<Guid>("Id"); // Shadow property as a primary key for the table
                r.HasKey("Id"); // Define the shadow property as the primary key
                r.Property(rr => rr.Role).IsRequired();
                r.ToTable("UserRoles"); // This will create a separate table for UserRoles
            });
        }
    }
}
