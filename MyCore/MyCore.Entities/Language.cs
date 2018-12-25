using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCore.Entities
{
    public class Language : BaseEntity
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Language");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Key).HasMaxLength(100);
            builder.Property(x => x.Value).HasMaxLength(150);
        }
    }
}
