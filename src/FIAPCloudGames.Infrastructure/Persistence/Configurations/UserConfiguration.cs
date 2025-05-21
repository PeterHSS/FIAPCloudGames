using FIAPCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPCloudGames.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id).HasName("PK_Users");

        builder.Property(user => user.Id).IsRequired();

        builder.Property(user => user.Name).IsRequired().HasMaxLength(200);

        builder.Property(user => user.Password).IsRequired();

        builder.Property(user => user.CreatedAt).IsRequired();

        builder.Property(user => user.UpdatedAt);

        builder.Property(user => user.BirthDate);

        builder.Property(user => user.Nickname);

        builder.Property(user => user.UpdatedAt);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(254);

        builder.Property(x => x.Document).IsRequired().HasMaxLength(14);

        builder.HasIndex(user => user.Email).IsUnique().HasDatabaseName("IX_Users_Email");

        builder.HasIndex(user => user.Document).IsUnique().HasDatabaseName("IX_Users_Document");

        builder.HasIndex(user => user.Name).HasDatabaseName("IX_Users_Name");
    }
}
