using University.Models;
using University.Repositories;

using System;
using System.Collections.Generic;

namespace University
{
    class Program
    {
        private static string _connectionString = @"Data Source=LAPTOP-OR0EMHA3\SQLEXPRESS01;Initial Catalog=University;Pooling=true;Integrated Security=SSPI";

        static void Main(string[] args)
        {
            IStudentRepository studentRepository = new StudentRawSqlRepository(_connectionString);
            IGroupRepository groupRepository = new GroupRawSqlRepository(_connectionString);
            IStudentInGroupsRepository studentInGroupsRepository = new StudentInGroupsRawSqlRepository(_connectionString);

            Console.WriteLine("Доступные команды:");
            Console.WriteLine("add-student - добавить студента");
            Console.WriteLine("add-group - добавить группу");
            Console.WriteLine("add-student-to-group - добавить студента в группу");
            Console.WriteLine("get-students - получить список студентов");
            Console.WriteLine("get-groups - получить список групп");
            Console.WriteLine("get-students-by-id-group - получить список студентов по id группы");

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "get-students")
                {
                    List<Student> students = studentRepository.GetAll();
                    foreach (Student student in students)
                    {
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
                    }
                }
                else if (command == "get-groups")
                {
                    List<Group> groups = groupRepository.GetAll();
                    foreach (Group group in groups)
                    {
                        Console.WriteLine($"Id: {group.Id}, Name: {group.Name}");
                    }
                }
                else if ( command == "get-students-by-id-group" )
                {
                    Console.WriteLine("Введите id группы");
                    int groupId = int.Parse(Console.ReadLine());
                    Student student = new Student();
                    List<StudentInGroups> studentsInGroup = studentInGroupsRepository.Get(groupId);
                    foreach (StudentInGroups studentInGroup in studentsInGroup)
                    {
                        student = studentRepository.GetById(studentInGroup.StudentId);
                        Console.WriteLine($"Id: {studentInGroup.StudentId}, name: {student.Name}");
                    }
                }
                else if (command == "add-group")
                {
                    Console.WriteLine("Введите имя группы");
                    string name = Console.ReadLine();

                    groupRepository.Add(new Group
                    {
                        Name = name
                    });
                    if (name == null)
                    {
                        Console.WriteLine("Имя группы введено неверно");
                        return;
                    }
                    Console.WriteLine("Успешно добавлено");
                }
                else if (command == "add-student")
                {
                    Console.WriteLine("Введите имя студента");
                    string name = Console.ReadLine();

                    studentRepository.Add(new Student
                    {
                        Name = name
                    });
                    if (name == null)
                    {
                        Console.WriteLine("Имя студента неверно");
                        return;
                    }
                    Console.WriteLine("Успешно добавлено");
                }
                else if ( command == "add-student-to-group" )
                {
                     Console.WriteLine("Введите id студента");
                     int studentId = Convert.ToInt32(Console.ReadLine());
                     Console.WriteLine("Введите id группы");
                     int groupsId = Convert.ToInt32(Console.ReadLine());
                     if (studentId < 1)
                     {
                        Console.WriteLine("id студента введен неверно");
                        return;
                     }
                     if (groupsId < 1)
                     {
                        Console.WriteLine(" id группы введен неверно");
                        return;
                     } 
                     studentInGroupsRepository.Add(new StudentInGroups
                     {
                        StudentId = studentId,
                        GroupsId = groupsId
                     });
                    Console.WriteLine("Успешно добавлено");
                }
                else if (command == "exit")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Команда не найдена");
                }
            }
        }
    }
}