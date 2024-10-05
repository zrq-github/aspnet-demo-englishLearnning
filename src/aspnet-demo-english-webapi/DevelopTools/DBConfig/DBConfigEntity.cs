using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ron.DomainCommons.Models;

namespace DBConfig
{
    public record DBConfigEntity: BaseEntity
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class DBConfigEntityConfig : IEntityTypeConfiguration<DBConfigEntity>
    {
        public DBConfigEntityConfig() { }

        public void Configure(EntityTypeBuilder<DBConfigEntity> builder)
        {
            builder.ToTable("T_Configs");
            builder.Property(e => e.Name).IsUnicode(true);
            builder.Property(e => e.Value).IsUnicode(true);
        }
    }
}
