using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Data.Common;
using DamuMed.Services;

namespace DamuMed.Data
{

    public abstract class DbDataAccess<T> : IDisposable
    {

        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;

        public DbDataAccess()
        {
            factory = DbProviderFactories.GetFactory("DamuMedProvider");

            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationService.Configuration["ConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        //public abstract void Update(T entity, string updateColumn, string value);
        //public abstract void Delete(T entity);
        public abstract void Insert(T entity);

    }

}
