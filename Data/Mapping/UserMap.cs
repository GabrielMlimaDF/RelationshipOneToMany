using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relação1N.Models;

namespace Relação1N.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.CreateAt)
                .IsRequired()
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValue(DateTime.Now.ToUniversalTime());

            //Indice
            builder.HasIndex(x => x.Email, "IX_User_Email").IsUnique();
        }
    }
}