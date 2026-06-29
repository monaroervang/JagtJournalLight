using JagtJournal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JagtJournal.Infrastructure.Data.Configurations;

public class HuntResultConfiguration : IEntityTypeConfiguration<HuntResult>
{
    // Konfigurerer hvordan HuntResult skal mappes til databasen
    public void Configure(EntityTypeBuilder<HuntResult> builder)
    {
        builder.HasKey(huntResult => huntResult.Id);

        builder.Property(huntResult => huntResult.HuntEntryId)
            .IsRequired();

        builder.Property(huntResult => huntResult.SpeciesId)
            .IsRequired();

        builder.Property(huntResult => huntResult.Amount)
            .IsRequired();

        builder.Property(huntResult => huntResult.Note)
            .HasMaxLength(500);
    }
}