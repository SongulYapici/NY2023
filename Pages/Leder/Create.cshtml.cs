using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NY2023.Pages.Børnegruppe;
using System.Data.SqlClient;

namespace NY2023.Pages.Leder
{
    public class CreateModel : PageModel
    {
        public LederInfo lederInfo = new LederInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            lederInfo.id = Request.Form["id"];
            lederInfo.børnegruppe_id = Request.Form["børnegruppe_id"];
            lederInfo.navn = Request.Form["navn"];
            lederInfo.email = Request.Form["email"];
            lederInfo.telefonnummer = Request.Form["telefonnummer"];


            if (lederInfo.id.Length == 0 || lederInfo.børnegruppe_id.Length == 0 ||
                lederInfo.navn.Length == 0 || lederInfo.email.Length == 0 ||
                lederInfo.telefonnummer.Length == 0)
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
                    String sql = "INSERT INTO Leder" +
                                 "(id, børnegruppe_id, navn, email, telefonnummer) VALUES " +
                                 "(@id, @børnegruppeid, @navn, @email, @telefonnummer);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", lederInfo.id);
                        command.Parameters.AddWithValue("@børnegruppeid", lederInfo.børnegruppe_id);
                        command.Parameters.AddWithValue("@navn", lederInfo.navn);
                        command.Parameters.AddWithValue("@email", lederInfo.email);
                        command.Parameters.AddWithValue("@telefonnummer", lederInfo.telefonnummer);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            lederInfo.id = ""; lederInfo.børnegruppe_id = ""; lederInfo.navn = "";
            lederInfo.email = ""; lederInfo.telefonnummer = "";
            successMessage = "Ny børnegruppe tilføjet korrekt";

            Response.Redirect("/Leder/Index");
        }
    }
}
