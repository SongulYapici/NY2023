using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace NY2023.Pages.Barn
{
    public class IndexModel : PageModel
    {
        public List<BarnInfo> listbarn = new List<BarnInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Barn";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BarnInfo barnInfo = new BarnInfo();
                                barnInfo.id = "" + reader.GetInt32(0);
                                barnInfo.navn = reader.GetString(1);
                                barnInfo.fødselsdato = reader.GetString(2);
                                barnInfo.køn = reader.GetString(3);
                                barnInfo.telefonnummer = reader.GetString(4);
                                barnInfo.email = reader.GetString(5);
                                barnInfo.solgte_lodder_kontanter = reader.GetString(6);
                                barnInfo.solgte_lodder_mobilepay = reader.GetString(7);
                                barnInfo.modtaget_lodder = reader.GetString(8);

                                listbarn.Add(barnInfo);
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


    public class BarnInfo
    {
        public string id;
        public string navn;
        public string fødselsdato;
        public string køn;
        public string telefonnummer;
        public string email;
        public string solgte_lodder_kontanter;
        public string solgte_lodder_mobilepay;
        public string modtaget_lodder;

    }
}
