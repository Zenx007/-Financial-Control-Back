using FamilyFinancialControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Infrastructure.Data.EntitiesConfiguration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(x => x.Id).IsRequired(true);
        builder.Property(x => x.Description).HasMaxLength(256).IsRequired(true);
        builder.Property(x => x.Value).HasPrecision(10, 2).IsRequired(true);
        builder.Property(x => x.TypeTransaction).IsRequired(true);
        builder.Property(x => x.CategoryId).IsRequired(true);
        builder.Property(x => x.UserId).IsRequired(true);
    }
}
