using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NY2023.Pages.B�rnegruppe;
using System.Data.SqlClient;

namespace NY2023.Pages.Leder
{
    public class EditModel : PageModel
    {
        public LederInfo lederInfo = new LederInfo();
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
                    string sql = "SELECT * FROM Leder WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LederInfo lederInfo = new LederInfo();
                                lederInfo.id = "" + reader.GetInt32(0);
                                lederInfo.b�rnegruppe_id = reader.GetString(1);
                                lederInfo.navn = reader.GetString(2);
                                lederInfo.email = reader.GetString(3);
                                lederInfo.telefonnummer = reader.GetString(4);
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
            lederInfo.id = Request.Form["id"];
            lederInfo.b�rnegruppe_id = Request.Form["b�rnegruppe_id"];
            lederInfo.navn = Request.Form["navn"];
            lederInfo.email = Request.Form["email"];
            lederInfo.telefonnummer = Request.Form["telefonnummer"];


            if (lederInfo.id.Length == 0 || lederInfo.b�rnegruppe_id.Length == 0 ||
                lederInfo.navn.Length == 0 || lederInfo.email.Length == 0 ||
                lederInfo.telefonnummer.Length == 0)
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
                    String sql = "UPDATE Leder " +
                                 "SET id=@id, b�rnegruppe_id=@b�rnegruppeid, navn=@navn, email=@email, telefonnummer=@telefonnummer" +
                                 "WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", lederInfo.id);
                        command.Parameters.AddWithValue("@b�rnegruppe_id", lederInfo.b�rnegruppe_id);
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

            Response.Redirect("/Leder/Index");
        }
    }
}
