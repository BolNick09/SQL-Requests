using System.Data.Common;
using Microsoft.Data.SqlClient;
namespace SQL_Requests
{
    internal class Program
    {
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test_BD;Integrated Security=True;Connect Timeout=30";

        static string sqlQuerry = "";
        static SqlConnection conn = null;
        static void Main(string[] args)
        {
            //querryIn();
            readDb();

            //updateDB();
            //readDb();

            deleteDB();
            readDb();

        }


        static void querryIn()
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            sqlQuerry = $"Insert Into Users Values (@age, @name)";
            SqlCommand cmd = new SqlCommand(sqlQuerry, conn);

            //ввод с клавиатуры
            string name = Console.ReadLine();
            int age;
            Int32.TryParse(Console.ReadLine(), out age);

            SqlParameter parName = new SqlParameter("@name", name);
            cmd.Parameters.Add(parName);
            SqlParameter parAge = new SqlParameter ("@age", age);
            cmd.Parameters.Add(parAge);

            int number = cmd.ExecuteNonQuery(); //update delete insert
            Console.WriteLine($"Запись кол-во {number} добавлена в БД");
        }

        static void readDb()
        {
            sqlQuerry = "Select * From Users";
            conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuerry, conn);

            SqlDataReader reader = cmd.ExecuteReader();
            //int dbCount = reader.FieldCount;
            if (reader.HasRows)
            {
                string col1 = reader.GetName(0);//Получить название перовго столцба
                string col2 = reader.GetName(1);//Получить название второго столцба
                string col3 = reader.GetName(2);//Получить название третьего столцба
                Console.WriteLine($"{col1}\t {col2}\t {col3}");
                while (reader.Read())
                {
                    object id = reader.GetValue(0);
                    object age = reader.GetValue(1);
                    object name = reader.GetValue(2);

                    //cmd.ExecuteScalar();// для агрегирующих функций
                    Console.WriteLine($"{id}\t {age}\t {name}");
                }

            }
            reader.Close();
            conn.Close();

        }

        static void updateDB()
        {
            using (conn = new SqlConnection(connectionString))//автоочищение после окончания использования
            {
                conn.Open();
                
                sqlQuerry = "Update Users set Age = @parAge where Id = @parId";
                SqlCommand cmd = new SqlCommand (sqlQuerry, conn);

                int id;
                Console.WriteLine("Enter ID");
                Int32.TryParse(Console.ReadLine(), out id);
                int age;
                Console.WriteLine("Enter Age");
                Int32.TryParse(Console.ReadLine(), out age);                

                SqlParameter parAge = new SqlParameter("@parAge", age);
                cmd.Parameters.Add(parAge);

                SqlParameter parId = new SqlParameter("@parId", id);
                cmd.Parameters.Add(parId);

                int number = cmd.ExecuteNonQuery();
                Console.WriteLine($"Запись кол-во {number} обновлена в БД");

            }
        }
        static void deleteDB()
        {
            using (conn = new SqlConnection(connectionString))//автоочищение после окончания использования
            {
                conn.Open();

                sqlQuerry = "DELETE FROM Users where Id = @parId";
                SqlCommand cmd = new SqlCommand(sqlQuerry, conn);

                int id;
                Console.WriteLine("Enter ID");
                Int32.TryParse(Console.ReadLine(), out id);
               
                SqlParameter parId = new SqlParameter("@parId", id);
                cmd.Parameters.Add(parId);

                int number = cmd.ExecuteNonQuery();
                Console.WriteLine($"Запись кол-во {number} удалена из БД");

            }
        }

    }
}
