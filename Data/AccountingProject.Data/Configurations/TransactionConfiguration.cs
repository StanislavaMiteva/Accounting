namespace AccountingProject.Data.Configurations
{
    using AccountingProject.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> transaction)
        {
            transaction
                .HasOne(t => t.DebitGLAccount)
                .WithMany(a => a.DebitTransactions)
                .HasForeignKey(t => t.DebitGLAccountId);

            transaction
                 .HasOne(t => t.CreditGLAccount)
                 .WithMany(a => a.CreditTransactions)
                 .HasForeignKey(t => t.CreditGLAccountId);

            transaction
               .HasOne(t => t.DebitAnalyticalAccount)
               .WithMany(a => a.DebitTransactions)
               .HasForeignKey(t => t.DebitAnalyticalAccountId);

            transaction
                .HasOne(t => t.CreditAnalyticalAccount)
                .WithMany(a => a.CreditTransactions)
                .HasForeignKey(t => t.CreditAnalyticalAccountId);
        }
    }
}
