using System;
using University.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repositories
{
    public class StudentInGroupsRawSqlRepository : IStudentInGroupsRepository
    {
        private readonly string _connectionString;

        public StudentInGroupsRawSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(StudentInGroups studentInGroups)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO [StudentInGroups]([GroupsId], [StudentId]) VALUES (@groupsId, @studentId)";
                    command.Parameters.Add("@groupsId", SqlDbType.Int).Value = studentInGroups.GroupsId;
                    command.Parameters.Add("@studentId", SqlDbType.Int).Value = studentInGroups.StudentId;
                    studentInGroups.GroupsId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
        public List<StudentInGroups> Get(int groupId)
        {
            var result = new List<StudentInGroups>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"select [StudentId]
                        from [StudentInGroups]
                        where [GroupsId] = @groupId";
                    command.Parameters.Add("@groupId", SqlDbType.Int).Value = groupId;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new StudentInGroups
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"])
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
