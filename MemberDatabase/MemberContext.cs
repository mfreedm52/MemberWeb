using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using GenericRepository;

namespace MemberDatabase
{
    public class MemberContext : DbContext
    {

        public MemberContext() : base("name=MemberDatabase") {
            Database.Log = LogSQL;
            Database.SetInitializer<MemberContext>(null);
        }

        //public MemberContext(string connection) : base(connection)
        //{

        //    Database.Log = LogSQL;
        //    Database.SetInitializer<MemberContext>(null);
        //}

        public DbSet<Event> Events { get; set; }

        //This is what I think we need to change this back too. IDbContext isnt workin
        public DbSet<T> DbSet<T>() where T : class
        {
            return base.Set<T>();
        }

        //public new IDbSet<T> Set<T>() where T : class
        //{
        //    return base.Set<T>();
        //}

        //public DbSet<Contact> myContacts { get; set; }
        //public DbSet<Member> myMembers { get; set; }
        //public DbSet<AuditLog> AuditLogs { get; set; }
        //public DbSet<File> myFiles { get; set; }

        //public DbSet<UserGroup> UserGroups { get; set; }

        ////   public DbSet<MemberGroup> MemberGroups { get; set; }
        //public DbSet<Group> myGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>()
        .Property(c => c.Id)
        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Contact>()
     .Property(c => c.Id)
     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<Contact>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Contacts");
            });


            modelBuilder.Entity<Member>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Members");
            });


            modelBuilder.Entity<Comment>().Map(m =>
            {
                m.ToTable("Comments");
            });

            modelBuilder.Entity<AuditLog>().Map(m =>
            {
                m.ToTable("AuditLogs");
            });

            modelBuilder.Entity<File>().Map(m =>
            {
                m.ToTable("Files");
            });

            modelBuilder.Entity<UserGroup>().Map(m =>
            {
                m.ToTable("UserGroups");
            });

            modelBuilder.Entity<Group>().Map(m =>
            {
                m.ToTable("Groups");
            });

            base.OnModelCreating(modelBuilder);

        }

        override public int SaveChanges()
        {
            LogChanges();
            try
            {
                base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var entityError in e.EntityValidationErrors)
                {

                    foreach (var validationError in entityError.ValidationErrors)
                    {
                        throw new Exception(validationError.ErrorMessage);
                    }
                }

            }

            return 0;
        }


        private void LogChanges()
        {
            var changeTrack = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added
                                || p.State == EntityState.Deleted || p.State == EntityState.Modified);

            foreach (var entry in changeTrack)
            {
                if (entry.Entity != null)
                {
                    string entityName = "";
                    string state = "";
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entityName = ObjectContext.GetObjectType(entry.Entity.GetType()).Name;
                            state = entry.State.ToString();
                            foreach (string prop in entry.OriginalValues.PropertyNames)
                            {
                                object currentValue = entry.CurrentValues[prop];
                                object originalValue = entry.OriginalValues[prop];
                                if (currentValue != null && currentValue.Equals(originalValue))
                                {
                                    WriteChangeToFile(entityName, state, prop, Convert.ToString(originalValue), Convert.ToString(currentValue));


                                    //AuditLogs.Add(new AuditLog
                                    //{
                                    //    entity = entityName,
                                    //    columnName = prop,
                                    //    changeDate = DateTime.Now,
                                    //    oldProperty = Convert.ToString(originalValue),
                                    //    newProperty = Convert.ToString(currentValue),
                                    //});
                                }
                            }
                            break;
                        case EntityState.Added:
                            entityName = ObjectContext.GetObjectType(entry.Entity.GetType()).Name;
                            state = entry.State.ToString();
                            foreach (string prop in entry.CurrentValues.PropertyNames)
                            {

                                WriteChangeToFile(entityName, state, prop, "", Convert.ToString(entry.CurrentValues[prop]));

                                //AuditLogs.Add(new AuditLog
                                //{
                                //    entity = entityName,
                                //    changeDate = DateTime.Now,
                                //    columnName = prop,
                                //    oldProperty = "",
                                //    newProperty = Convert.ToString(entry.CurrentValues[prop]),
                                //});

                            }
                            break;
                        case EntityState.Deleted:
                            entityName = ObjectContext.GetObjectType(entry.Entity.GetType()).Name;
                            state = entry.State.ToString();
                            foreach (string prop in entry.OriginalValues.PropertyNames)
                            {
                                WriteChangeToFile(entityName, state, prop, Convert.ToString(entry.OriginalValues[prop]), "");

                                //AuditLogs.Add(new AuditLog
                                //{
                                //    TableName = entityName,
                                //    State = state,
                                //    ColumnName = prop,
                                //    OriginalValue = Convert.ToString(entry.OriginalValues[prop]),
                                //    NewValue = string.Empty,
                                //});

                            }
                            break;
                        default:
                            break;
                    }
                }
            }



        }


        private void WriteChangeToFile(string entityName, string state, string prop, string originalValue, string currentValue)
        {
            var date = DateTime.Now.ToString("dd_MM_yyyy");

            using (System.IO.StreamWriter sw = System.IO.File.AppendText(String.Format(@"C:\Logs\Changes_{0}.txt", date)))
            {
                sw.WriteLine("Change Tracker [" + entityName + "]: " + prop + " changed from " + originalValue + " to " + currentValue);

            }

        }

        private void LogSQL(string sql)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(@"C:\Logs\SQL.txt"))
            {
                sw.WriteLine(sql);

            }

        }

        public System.Data.Entity.DbSet<MemberDatabase.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<MemberDatabase.Member> Members { get; set; }
    }
}
