using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using MCT.DB.Entities;

namespace MCT.DB.NH
{
    /*<?xml version="1.0" encoding="utf-8"?>  
        <hibernate-configuration  xmlns="urn:nhibernate-configuration-2.2" >  
            <session-factory name="NHibernate.Test">  
                <property name="connection.provider">NHibernate.Connection.DriverConnectionProvider</property>  
                <property name="connection.driver_class">NHibernate.Driver.NpgsqlDriver</property>  
                <property name="connection.connection_string">  
                    Server=localhost;initial catalog=nhibernate;User ID=nhibernate;Password=********;  
                </property>  
                <property name="dialect">NHibernate.Dialect.PostgreSQLDialect</property>  
                <property name="show_sql">false</property> 
            </session-factory>  
        </hibernate-configuration>  
      </xml>
     */
    public static class NHibernateSessionHelper
    {
        public static ISessionFactory GetNHibernateSessionFactory()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(config => {

                config.Dialect<NHibernate.Dialect.PostgreSQL82Dialect>();
                config.Driver<NHibernate.Driver.NpgsqlDriver>();
                config.ConnectionStringName = "ApplicationServices";
                config.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
            });

            configuration.CurrentSessionContext<WebSessionContext>();

            configuration.AddAssembly(typeof(Subject).Assembly);

            return configuration.BuildSessionFactory();
        }
    }
}
