using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace NY2023.Pages.Børnegruppe
{
    public class IndexModel : PageModel
    {
        public List<BørnegruppeInfo> listbørnegruppe = new List<BørnegruppeInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";
                //String connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Børnegruppe";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BørnegruppeInfo børnegruppeInfo = new BørnegruppeInfo();
                                børnegruppeInfo.ID = "" + reader.GetInt32(0);
                                børnegruppeInfo.Navn = reader.GetString(2);
                                børnegruppeInfo.AntalBørn = reader.GetString(3);
                                børnegruppeInfo.LederID = "" + reader.GetInt32(1);
                                børnegruppeInfo.SolgteLodder = reader.GetString(4);

                                
                                
                                    listbørnegruppe.Add(børnegruppeInfo);
                                

                               
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


    public class BørnegruppeInfo
    {
        public string ID;
        public string Navn; 
        public string AntalBørn;
        public string LederID;
        public string SolgteLodder;

    }
}
