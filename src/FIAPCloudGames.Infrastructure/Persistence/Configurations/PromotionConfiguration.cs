using FIAPCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPCloudGames.Infrastructure.Persistence.Configurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.ToTable("Promotions");

        builder.HasKey(promotion => promotion.Id).HasName("PK_Promotions");

        builder.Property(promotion => promotion.Id).IsRequired();

        builder.Property(promotion => promotion.CreatedAt).IsRequired();

        builder.Property(promotion => promotion.Name).HasMaxLength(500);

        builder.Property(promotion => promotion.StartDate).IsRequired();

        builder.Property(promotion => promotion.EndDate).IsRequired();

        builder.Property(promotion => promotion.DiscountPercentage).IsRequired().HasPrecision(10,2);

        builder.Property(promotion => promotion.Description).HasMaxLength(1000);

        builder.Navigation(promotion => promotion.Games).AutoInclude();
    }
}
