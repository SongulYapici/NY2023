using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NY2023.Pages.Barn;
using System.Data.SqlClient;
using System.Web;

namespace NY2023.Pages.Børnegruppe
{
    public class CreateModel : PageModel
    {
       
        public BørnegruppeInfo børnegruppeInfo = new BørnegruppeInfo();
        public string errorMessage = "";
        public string successMessage = "";
        
        
        public void OnGet() { 
        
        }

        public void OnPost()
        {
            børnegruppeInfo.ID = Request.Form["id"];
            børnegruppeInfo.Navn = Request.Form["navn"];
            børnegruppeInfo.AntalBørn = Request.Form["antal_børn"];
            børnegruppeInfo.LederID = Request.Form["leder_id"];
            børnegruppeInfo.SolgteLodder = Request.Form["solgte_lodder"];
            

            if (børnegruppeInfo.ID.Length == 0 ||
                børnegruppeInfo.Navn.Length == 0 || 
                børnegruppeInfo.AntalBørn.Length == 0 || 
                børnegruppeInfo.LederID.Length == 0 ||
                børnegruppeInfo.SolgteLodder.Length == 0)
            {
                errorMessage = "Alle felter skal udfyldes";
                return;
            }

            //save the new børnegruppe into the database
            try
            {
                string connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Børnegruppe" +
                                 "(id, leder_id, navn, antal_børn, solgte_lodder) VALUES " +
                                 "(@id, @lederid, @navn, @antalbørn, @solgtelodder);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", børnegruppeInfo.ID);
                        command.Parameters.AddWithValue("@navn", børnegruppeInfo.Navn);
                        command.Parameters.AddWithValue("@antalbørn", børnegruppeInfo.AntalBørn);
                        command.Parameters.AddWithValue("@lederid", børnegruppeInfo.LederID);
                        command.Parameters.AddWithValue("@solgtelodder", børnegruppeInfo.SolgteLodder);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            børnegruppeInfo.ID = "";  børnegruppeInfo.Navn = ""; 
            børnegruppeInfo.AntalBørn = ""; børnegruppeInfo.LederID = ""; børnegruppeInfo.SolgteLodder = "";
            successMessage = "Ny børnegruppe tilføjet korrekt";

            string path = "/Børnegruppe/Index";
            string encodedSegment = HttpUtility.UrlEncode("Børnegruppe").Replace("+", "%20");
            string encodedPath = "/" + encodedSegment + "/Index";
            Response.Redirect(encodedPath);
        }
    }
}
