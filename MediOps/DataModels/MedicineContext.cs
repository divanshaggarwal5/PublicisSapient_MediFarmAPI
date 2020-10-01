using Microsoft.EntityFrameworkCore;

namespace MediOps.DataModels
{
    public class MedicineContext:DbContext
    {
        public MedicineContext(DbContextOptions<MedicineContext> dbOptions):base(dbOptions)
        {
            
        }

        public DbSet<Medicine> MedDbSet { get; set; }
    }
}
