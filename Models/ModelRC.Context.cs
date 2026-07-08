namespace RoadCompany.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RoadEntities : DbContext
    {
        public RoadEntities()
            : base("name=RoadEntities")
        {
        }
        private static RoadEntities _context;
        public static RoadEntities GetContext()
        {
            if(_context == null)
                _context = new RoadEntities();

            return _context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Absence> Absence { get; set; }
        public DbSet<AbsenceType> AbsenceType { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartment { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventEmployee> EventEmployee { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialTrening> MaterialTrening { get; set; }
        public DbSet<Parlor> Parlor { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<SubdDepartment> SubdDepartment { get; set; }
        public DbSet<SubDepartment> SubDepartment { get; set; }
        public DbSet<Subdivision> Subdivision { get; set; }
        public DbSet<TrainingCalendar> TrainingCalendar { get; set; }
        public DbSet<TypeEvent> TypeEvent { get; set; }
        public DbSet<TypeMaterial> TypeMaterial { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WorkEmployee> WorkEmployee { get; set; }
        public DbSet<WorkingCalendar> WorkingCalendar { get; set; }
    }
}
