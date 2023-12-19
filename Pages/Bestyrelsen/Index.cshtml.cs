using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NY2023.Pages.Bestyrelsen
{
    public class IndexModel : PageModel
    {

        public List<BestyrelsenInfo> listbestyrelsen = new List<BestyrelsenInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))

                {
                    connection.Open();
                    String sql = "SELECT * FROM Bestyrelsen";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BestyrelsenInfo bestyrelsenInfo = new BestyrelsenInfo();
                                bestyrelsenInfo.id_for_b�rne_gruppe = "" + reader.GetInt32(0);
                                bestyrelsenInfo.super_s�lger = reader.GetString(1);
                                bestyrelsenInfo.antal_solgte_lodder_kontanter = reader.GetString(2);
                                bestyrelsenInfo.antal_solgte_lodder_mobilepay = reader.GetString(3);
                                bestyrelsenInfo.telefonnummer = reader.GetString(4);
                                bestyrelsenInfo.email = reader.GetString(5);


                                listbestyrelsen.Add(bestyrelsenInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("Execption: " + ex.ToString());
            } 
        }
    }


    public class BestyrelsenInfo 
    {
        public string id_for_b�rne_gruppe;
        public string super_s�lger;
        public string antal_solgte_lodder_kontanter;
        public string antal_solgte_lodder_mobilepay;
        public string telefonnummer;
        public string email;
    }
   
}