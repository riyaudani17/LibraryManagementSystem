using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RKHospital
{
    class DataAddBooks
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\hospital.mdf;Integrated Security=True;Connect Timeout=30");
        public int ID { set; get; }
        public string patient_name{ set; get; }
        public string doctor { set; get; }
        public string registered { set; get; }
        public string image { set; get; }
        public string status { set; get; }

        public List<DataAddBooks> addBooksData()
        {
            List<DataAddBooks> listData = new List<DataAddBooks>();

            if(connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM books WHERE date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        

                        while (reader.Read())
                        {
                            DataAddBooks dab = new DataAddBooks();
                            dab.ID = (int)reader["id"];
                            dab.patient_name = reader["book_title"].ToString();
                            dab.doctor = reader["author"].ToString();
                            dab.registered = reader["published_date"].ToString();
                            dab.image = reader["image"].ToString();
                            dab.status = reader["status"].ToString();

                            listData.Add(dab);
                        }

                        reader.Close();
                    }

                    }catch(Exception ex)
                {
                    Console.WriteLine("Error conenecting Database: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }
    }
}
