using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.Urls
{
    internal sealed class UrlMap : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> b)
        {
            b.ToTable(nameof(Url));

            b.HasKey(x => x.Id);

            b.HasIndex(x => x.Id);
            b.HasIndex(x => x.ShortUrl);

            b.Property(x => x.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("timestamp");

            b.Property(x => x.Hits)
                .HasColumnName("Hits")
                .HasColumnType("integer");

            b.Property(x => x.OriginalUrl)
                .HasColumnName("OriginalUrl")
                .HasColumnType("varchar(255)");

            b.Property(x => x.ShortUrl)
                .HasColumnName("ShortUrl")
                .HasColumnType("varchar(30)");
        }
    }
}