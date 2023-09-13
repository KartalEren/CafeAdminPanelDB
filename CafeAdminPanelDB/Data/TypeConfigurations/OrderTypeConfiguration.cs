using CafeAdminPanelDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeAdminPanelDB.Data.TypeConfigurations
{
    public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.ID);



            builder.Property(x => x.ID)
                    .UseIdentityColumn(1, 1);


            builder.HasOne<Table>(x=>x.Table).WithMany(x => x.Orders).HasForeignKey(x => x.TableID);
        }
    }
}
