using CafeAdminPanelDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeAdminPanelDB.Data.TypeConfigurations
{
    public class TableTypeConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(x => x.ID);



            builder.Property(x => x.ID)
                    .UseIdentityColumn(1, 1);


            builder.HasData(
                new Table { ID = 1, TableNo = 1 }, 
                new Table { ID = 2, TableNo = 2 }, 
                new Table { ID = 3, TableNo = 3 }, 
                new Table { ID = 4, TableNo = 4 }, 
                new Table { ID = 5, TableNo = 5 }, 
                new Table { ID = 6, TableNo = 6 }, 
                new Table { ID = 7, TableNo = 7 }, 
                new Table { ID = 8, TableNo = 8 }, 
                new Table { ID = 9, TableNo = 9 }, 
                new Table { ID = 10, TableNo = 10 }, 
                new Table { ID = 11, TableNo = 11 }, 
                new Table { ID = 12, TableNo = 12 }, 
                new Table { ID = 13, TableNo = 13 }, 
                new Table { ID = 14, TableNo = 14 }, 
                new Table { ID = 15, TableNo = 15 });

        }
    }
}
