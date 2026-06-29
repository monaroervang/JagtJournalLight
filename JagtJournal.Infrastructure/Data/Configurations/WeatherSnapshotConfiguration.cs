using JagtJournal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JagtJournal.Infrastructure.Data.Configurations;

public class WeatherSnapshotConfiguration : IEntityTypeConfiguration<WeatherSnapshot>
{
    // Konfigurerer hvordan WeatherSnapshot skal mappes til databasen
    public void Configure(EntityTypeBuilder<WeatherSnapshot> builder)
    {
        builder.HasKey(weatherSnapshot => weatherSnapshot.Id);

        builder.Property(weatherSnapshot => weatherSnapshot.HuntEntryId)
            .IsRequired();

        builder.Property(weatherSnapshot => weatherSnapshot.Temperature)
            .HasPrecision(5, 2);

        builder.Property(weatherSnapshot => weatherSnapshot.WindDirection)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(weatherSnapshot => weatherSnapshot.WindSpeedMs)
            .HasPrecision(5, 2);

        builder.Property(weatherSnapshot => weatherSnapshot.WeatherDescription)
            .IsRequired()
            .HasMaxLength(200);
    }
}