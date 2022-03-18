using University.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace University.Repositories
{
    class StudentRawSqlRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRawSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAll() //получение всех студентов
        {
            var result = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select [Id], [Name] from [Student]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        public void Add(Student student) //добавление студента 
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"insert into [Student]
                            ([Name])
                        values
                            (@name)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = student.Name;

                    student.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public Student GetById(int id) //получение студента по id
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"select [Id], [Name]
                        from [Student]
                        where [Id] = @id";

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}