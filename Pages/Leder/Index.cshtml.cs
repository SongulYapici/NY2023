using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace NY2023.Pages.Leder
{
    public class IndexModel : PageModel
    {
        public List<LederInfo> listleder = new List<LederInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Leder";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LederInfo lederInfo = new LederInfo();
                                lederInfo.id = "" + reader.GetInt32(0);
                                lederInfo.børnegruppe_id = reader.GetString(1);
                                lederInfo.navn = reader.GetString(2);
                                lederInfo.email = reader.GetString(3);
                                lederInfo.telefonnummer = reader.GetString(4);


                                listleder.Add(lederInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }


    public class LederInfo
    {
        public string id;
        public string børnegruppe_id;
        public string navn;
        public string email;
        public string telefonnummer;

    }
}
