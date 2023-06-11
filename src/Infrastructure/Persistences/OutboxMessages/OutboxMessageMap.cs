using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.OutboxMessages
{
    internal sealed class OutboxMessageMap : IEntityTypeConfiguration<OutBoxMessage>
    {
        public void Configure(EntityTypeBuilder<OutBoxMessage> b)
        {
            b.ToTable(nameof(OutBoxMessage));

            b.HasKey(x => x.Id);

            b.HasIndex(x => x.Id);

            b.Property(x => x.Type)
                .HasColumnName("Type")
                .HasColumnType("varchar(100)");

            b.Property(x => x.Content)
                .HasColumnName("Content")
                .HasColumnType("text");

            b.Property(x => x.OccurredOn)
                .HasColumnName("OccurredOn")
                .HasColumnType("timestamp");

            b.Property(x => x.ProcessedOn)
                .HasColumnName("ProcessedOn")
                .HasColumnType("timestamp");

            b.Property(x => x.Error)
                .HasColumnName("Error")
                .HasColumnType("varchar(255)");

            b.Property(x => x.Success)
                .HasColumnName("success")
                .HasColumnType("bool");
        }
    }
}