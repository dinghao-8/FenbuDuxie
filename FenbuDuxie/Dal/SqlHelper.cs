using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FenbuDuxie.Dal
{
   public  class SqlHelper
    {
        private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;
        public T Find<T>(int id) where T : BaseModel {
            Type type = typeof(T);
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{ p.Name}]"));
            string sql = $@"SELECT {columnString} FROM [{type.Name}] WHERE id=@id";
            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@id",id)
            };
            using (SqlConnection con =new SqlConnection(ConnectionStringCustomers)) {
                SqlCommand com = new SqlCommand(sql,con);
                com.Parameters.AddRange(sqlParameters);
                con.Open();
                var reader = com.ExecuteReader();
                if (reader.Read())
                {
                    T t = Activator.CreateInstance<T>();
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(t, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
                    }
                    return t;
                }
                else {
                    return default;
                }
            }
        }
        public bool Insert<T>(T t) where T : BaseModel {
            Type type = typeof(T);
            string columuString = string.Join(",", type.GetProperties().Select(p => $"[ {p.Name} ]"));
            string valuesstring = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            string sql = $"Insert into [{type.Name}] ({columuString}) Values ({valuesstring})";
            var SqlparameterList = type.GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            using (SqlConnection con=new SqlConnection(ConnectionStringCustomers)) {
                SqlCommand com = new SqlCommand(sql,con);
                com.Parameters.AddRange(SqlparameterList);
                con.Open();
                int iResult = com.ExecuteNonQuery();
                return iResult == 1;
            }
        }
    }
}
