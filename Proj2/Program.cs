using System;
using System.Data.SqlClient;

namespace Proj2
{
    class Program
    {

        static SqlConnection ConnectToDB(string str)
        {
            try
            {
                SqlConnection connection = new SqlConnection(str);
                connection.Open();
                Console.WriteLine("Connected!");
                return connection;
            }
            catch (Exception)
            {
                
                throw;
            }


        }
        static void ShowCommand(SqlCommand com)
        {
           
            SqlDataReader reader = com.ExecuteReader();
            do
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    Console.Write(reader.GetName(i) + " ");
                }
                Console.WriteLine();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {

                        Console.Write(reader[i].ToString() + " ");
                    }
                    Console.WriteLine();

                }
                Console.WriteLine();
            } while (reader.NextResult());
            reader.Close();

        }
        static void ShowAll(SqlConnection con)
        {
            string query = "select Firstname,Lastname,Groupname,SubjectName,Mark from Students inner join Marks on Marks.StudentId=Students.Id " +
                "inner join Subjects on Marks.Subjectid=Subjects.Id "+
                "inner join Groups on Groups.Id=Students.GroupId"

                ;
            ShowCommand(new SqlCommand(query, con));
        }
        static void ShowStudent(SqlConnection con)
        {
            string query = "select Firstname,Lastname from Students;";
            ShowCommand(new SqlCommand(query, con));
        }
        static void ShowAverages(SqlConnection con)
        {
            string query = "select Firstname,avg(Mark) from Students inner join Marks on Students.Id=Marks.StudentId group by Students.FirstName";
            ShowCommand(new SqlCommand(query, con));
        }
        static void ShowMoreThanAverages(SqlConnection con)
        {
            string query = "select avg(Mark) from Marks";
            int avg = Convert.ToInt32((new SqlCommand(query, con).ExecuteScalar()));
            query = $"select FirstName,LastName from Students inner join Marks on Students.Id=Marks.StudentId where Mark>{avg}";

            ShowCommand(new SqlCommand(query, con));
        }
        static void ShowLeastSubjects(SqlConnection con)
        {
            string query = "select min(Mark) from Marks";
            int min = Convert.ToInt32((new SqlCommand(query, con).ExecuteScalar()));
            query = $"select SubjectName from Subjects inner join marks on Marks.Subjectid=Subjects.Id where Mark={min}";

            ShowCommand(new SqlCommand(query, con));
        }
        static void Main(string[] args)
        {
            #region comments
            //string connstring = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=example; Integrated security=true;";
            //using (SqlConnection connection = new SqlConnection(connstring))
            //{
            //    connection.Open();
            //    string query = "Select * from Students; Select * from Marks";
            //    SqlCommand command = new SqlCommand(query, connection);
            //    SqlDataReader reader = command.ExecuteReader();

            //    do
            //    {
            //        for (int i = 0; i < reader.FieldCount; i++)
            //        {

            //            Console.Write(reader.GetName(i) + " ");
            //        }
            //        Console.WriteLine();
            //        while (reader.Read())
            //        {
            //            for (int i = 0; i < reader.FieldCount; i++)
            //            {

            //                Console.Write(reader[i].ToString() + " ");
            //            }
            //            Console.WriteLine();

            //        }
            //        Console.WriteLine();
            //    } while (reader.NextResult());

            //    Console.WriteLine();
            //    reader.Close();

            //    query = @"Select avg(Mark) from Marks";
            //    command = new SqlCommand(query, connection);
            //    Console.WriteLine(Convert.ToString(command.ExecuteScalar()));


            //}
            #endregion

            Console.WriteLine("Connect to db?y/n");
            string a = Console.ReadLine();
            if (a=="y")
            {
                SqlConnection connection= ConnectToDB("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=example; Integrated security=true;");
                bool game = true;


                while (game)
                {
                    Console.WriteLine("1.ShowAll  2.ShowNames  3.Show AvgMarks  4.MoreThanAvg  5.LeastSubjects 6.Clear 7.Ex4");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            ShowAll(connection);
                            break;
                        case 2:
                            ShowStudent(connection);
                            break;
                        case 3:
                            ShowAverages(connection);
                            break;
                        case 4:
                            ShowMoreThanAverages(connection);
                            break;
                        case 5:
                            ShowLeastSubjects(connection);
                            break;
                        case 6:
                            Console.Clear();
                            break;
                        case 7:
                            game = false;
                            break;
                        default:

                            break;
                    }
                }

                game = true;
                Console.WriteLine("Ex4");
                while (game)
                {
                    Console.WriteLine("1.ShowAll  2.ShowNames  3.Show AvgMarks  4.MoreThanAvg  5.LeastSubjects 6.Clear 7.Exit");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            ShowAll(connection);
                            break;
                        case 2:
                            ShowStudent(connection);
                            break;
                        case 3:
                            ShowAverages(connection);
                            break;
                        case 4:
                            ShowMoreThanAverages(connection);
                            break;
                        case 5:
                            ShowLeastSubjects(connection);
                            break;
                        case 6:
                            Console.Clear();
                            break;
                        case 7:
                            game = false;
                            break;
                        default:

                            break;
                    }
                }
            }
            else
            {

            }
        }
    }
}
