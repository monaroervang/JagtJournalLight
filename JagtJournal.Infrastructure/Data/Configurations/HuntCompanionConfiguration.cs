using JagtJournal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JagtJournal.Infrastructure.Data.Configurations;

public class HuntCompanionConfiguration : IEntityTypeConfiguration<HuntCompanion>
{
    // Konfigurerer hvordan HuntCompanion skal mappes til databasen
    public void Configure(EntityTypeBuilder<HuntCompanion> builder)
    {
        builder.HasKey(huntCompanion => huntCompanion.Id);

        builder.Property(huntCompanion => huntCompanion.HuntEntryId)
            .IsRequired();

        builder.Property(huntCompanion => huntCompanion.UserId)
            .IsRequired(false);

        builder.Property(huntCompanion => huntCompanion.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}