using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Web;

namespace NY2023.Pages.Børnegruppe
{
    public class DeleteModel : PageModel
    {
        public void OnGet()
        {
                try
                {
                    string id = Request.Query["id"];

                    string connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "DELETE FROM Børnegruppe WHERE id=@id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                string path = "/Børnegruppe/Index";
                string encodedSegment = HttpUtility.UrlEncode("Børnegruppe").Replace("+", "%20");
                string encodedPath = "/" + encodedSegment + "/Index";
                Response.Redirect(encodedPath);
            }
        }
}


