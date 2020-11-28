using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentRepository
    {
        public List<Student> GetAllStudents()
        {
            List<Student> result = new List<Student>();

            using (SqlConnection sqlConnection = new SqlConnection(Constants.connectionString)) 
            { SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Students";
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while(sqlDataReader.Read())
                {
                    Student s = new Student();
                    s.Name = sqlDataReader.GetString(0);
                    s.IndexNumber = sqlDataReader.GetString(1);
                    s.AverageMArk = sqlDataReader.GetDecimal(2);
                    result.Add(s);
                }
            
            }
            return result;
        }

        public int InsertStudent(Student s)
        {
            using(SqlConnection sqlConnection = new SqlConnection(Constants.connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = string.Format("INSERT INTO Students VALUES('{0}','{1}',{2})",s.Name,s.IndexNumber,s.AverageMArk);
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
