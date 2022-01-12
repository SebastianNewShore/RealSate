namespace ApiRealState.Models.ModelDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbRealState : DbContext
    {
        public dbRealState()
            : base("name=dbRealState")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbCategory> tbCategories { get; set; }
        public virtual DbSet<tbCity> tbCities { get; set; }
        public virtual DbSet<tbCountry> tbCountries { get; set; }
        public virtual DbSet<tbCustomer> tbCustomers { get; set; }
        public virtual DbSet<tbDocument> tbDocuments { get; set; }
        public virtual DbSet<tbEmployee> tbEmployees { get; set; }
        public virtual DbSet<tbGender> tbGenders { get; set; }
        public virtual DbSet<tbImage> tbImages { get; set; }
        public virtual DbSet<tbOwner> tbOwners { get; set; }
        public virtual DbSet<tbPosition> tbPositions { get; set; }
        public virtual DbSet<tbProperty> tbProperties { get; set; }
        public virtual DbSet<tbPropertyTrace> tbPropertyTraces { get; set; }
        public virtual DbSet<tbRegion> tbRegions { get; set; }
        public virtual DbSet<tbStateProperty> tbStateProperties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<tbCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbCategory>()
                .HasMany(e => e.tbProperties)
                .WithRequired(e => e.tbCategory)
                .HasForeignKey(e => e.IdCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbCity>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbCity>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbCity>()
                .HasMany(e => e.tbCustomers)
                .WithRequired(e => e.tbCity)
                .HasForeignKey(e => e.IdCity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbCity>()
                .HasMany(e => e.tbEmployees)
                .WithRequired(e => e.tbCity)
                .HasForeignKey(e => e.IdCity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbCity>()
                .HasMany(e => e.tbOwners)
                .WithRequired(e => e.tbCity)
                .HasForeignKey(e => e.IdCity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbCountry>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbCountry>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbCountry>()
                .HasMany(e => e.tbRegions)
                .WithRequired(e => e.tbCountry)
                .HasForeignKey(e => e.IdCountry)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbCustomer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .Property(e => e.DocumentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .HasMany(e => e.tbPropertyTraces)
                .WithRequired(e => e.tbCustomer)
                .HasForeignKey(e => e.IdCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbDocument>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbDocument>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbDocument>()
                .HasMany(e => e.tbCustomers)
                .WithRequired(e => e.tbDocument)
                .HasForeignKey(e => e.IdDocument)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbDocument>()
                .HasMany(e => e.tbEmployees)
                .WithRequired(e => e.tbDocument)
                .HasForeignKey(e => e.IdDocument)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbDocument>()
                .HasMany(e => e.tbOwners)
                .WithRequired(e => e.tbDocument)
                .HasForeignKey(e => e.IdDocument)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbEmployee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<tbEmployee>()
                .Property(e => e.DocumentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tbEmployee>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<tbEmployee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbEmployee>()
                .HasMany(e => e.tbPropertyTraces)
                .WithOptional(e => e.tbEmployee)
                .HasForeignKey(e => e.IdEmployee);

            modelBuilder.Entity<tbGender>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbGender>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbGender>()
                .HasMany(e => e.tbCustomers)
                .WithRequired(e => e.tbGender)
                .HasForeignKey(e => e.IdGender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbGender>()
                .HasMany(e => e.tbEmployees)
                .WithRequired(e => e.tbGender)
                .HasForeignKey(e => e.IdGender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbGender>()
                .HasMany(e => e.tbOwners)
                .WithRequired(e => e.tbGender)
                .HasForeignKey(e => e.IdGender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbImage>()
                .Property(e => e.File)
                .IsUnicode(false);

            modelBuilder.Entity<tbOwner>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbOwner>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<tbOwner>()
                .Property(e => e.DocumentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tbOwner>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<tbOwner>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<tbOwner>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbOwner>()
                .HasMany(e => e.tbProperties)
                .WithRequired(e => e.tbOwner)
                .HasForeignKey(e => e.IdOwner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbPosition>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbPosition>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbPosition>()
                .HasMany(e => e.tbEmployees)
                .WithRequired(e => e.tbPosition)
                .HasForeignKey(e => e.IdPosition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbProperty>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<tbProperty>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbProperty>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<tbProperty>()
                .Property(e => e.InternalValue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tbProperty>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbProperty>()
                .HasMany(e => e.tbImages)
                .WithRequired(e => e.tbProperty)
                .HasForeignKey(e => e.IdProperty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbProperty>()
                .HasMany(e => e.tbPropertyTraces)
                .WithRequired(e => e.tbProperty)
                .HasForeignKey(e => e.IdProperty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbPropertyTrace>()
                .Property(e => e.Value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tbRegion>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<tbRegion>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbRegion>()
                .HasMany(e => e.tbCities)
                .WithRequired(e => e.tbRegion)
                .HasForeignKey(e => e.IdRegion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbStateProperty>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbStateProperty>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbStateProperty>()
                .HasMany(e => e.tbPropertyTraces)
                .WithRequired(e => e.tbStateProperty)
                .HasForeignKey(e => e.IdStateProperty)
                .WillCascadeOnDelete(false);
        }
    }
}
