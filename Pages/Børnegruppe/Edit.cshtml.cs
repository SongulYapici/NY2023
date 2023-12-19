using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NY2023.Pages.Barn;
using System.Data.SqlClient;
using System.Web;

namespace NY2023.Pages.Børnegruppe
{
    public class EditModel : PageModel
    {
        public BørnegruppeInfo børnegruppeInfo = new BørnegruppeInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Børnegruppe WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                børnegruppeInfo.ID = "" + reader.GetInt32(0);
                                børnegruppeInfo.Navn = reader.GetString(2);
                                børnegruppeInfo.AntalBørn = reader.GetString(3);
                                børnegruppeInfo.LederID = "" + reader.GetInt32(0);
                                børnegruppeInfo.SolgteLodder = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
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
            try
            {
                string connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Børnegruppe " +
                                 "SET id=@id, leder_id=@lederid, navn=@navn, antal_børn=@antalbørn, solgte_lodder=@solgtelodder " +
                                 "WHERE id=@id;";

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

            string path = "/Børnegruppe/Index";
            string encodedSegment = HttpUtility.UrlEncode("Børnegruppe").Replace("+", "%20");
            string encodedPath = "/" + encodedSegment + "/Index";
            Response.Redirect(encodedPath);
        }
    }
}
