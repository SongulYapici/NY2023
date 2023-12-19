using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace NY2023.Pages.Barn
{
    public class EditModel : PageModel
    {
        public BarnInfo barnInfo = new BarnInfo();
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
                    string sql = "SELECT * FROM Barn WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                barnInfo.id = "" + reader.GetInt32(0);
                                barnInfo.navn = reader.GetString(1);
                                barnInfo.fødselsdato = reader.GetString(2);
                                barnInfo.køn = reader.GetString(3);
                                barnInfo.telefonnummer = reader.GetString(4);
                                barnInfo.email = reader.GetString(5);
                                barnInfo.solgte_lodder_kontanter = reader.GetString(6);
                                barnInfo.solgte_lodder_mobilepay = reader.GetString(7);
                                barnInfo.modtaget_lodder = reader.GetString(8);
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
            barnInfo.id = Request.Form["id"];
            barnInfo.navn = Request.Form["navn"];
            barnInfo.fødselsdato = Request.Form["fødselsdato"];
            barnInfo.køn = Request.Form["køn"];
            barnInfo.telefonnummer = Request.Form["telefonnummer"];
            barnInfo.email = Request.Form["email"];
            barnInfo.solgte_lodder_kontanter = Request.Form["solgte_lodder_kontanter"];
            barnInfo.solgte_lodder_mobilepay = Request.Form["solgte_lodder_mobilepay"];
            barnInfo.modtaget_lodder = Request.Form["modtaget_lodder"];


            if (barnInfo.id.Length == 0 || barnInfo.navn.Length == 0 || barnInfo.fødselsdato.Length == 0 ||
                barnInfo.køn.Length == 0 || barnInfo.telefonnummer.Length == 0 || barnInfo.email.Length == 0 || barnInfo.solgte_lodder_kontanter.Length == 0 ||
                barnInfo.solgte_lodder_mobilepay.Length == 0 || barnInfo.modtaget_lodder.Length == 0)
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
                    String sql = "UPDATE Barn " +
                                 "SET id=@id, navn=@navn, fødselsdato=@fødselsdato, køn=@køn, telefonnummer=@telefonnummer, email=@email, solgtelodderkontanter=@solgtelodderkontanter, solgteloddermobilepay=@solgteloddermobilepay, modtagetlodder=@modtagetlodder " +
                                 "WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", barnInfo.id);
                        command.Parameters.AddWithValue("@navn", barnInfo.navn);
                        command.Parameters.AddWithValue("@fødselsdato", barnInfo.fødselsdato);
                        command.Parameters.AddWithValue("@køn", barnInfo.køn);
                        command.Parameters.AddWithValue("@telefonnummer", barnInfo.telefonnummer);
                        command.Parameters.AddWithValue("@email", barnInfo.email);
                        command.Parameters.AddWithValue("@solgtelodderkontanter", barnInfo.solgte_lodder_kontanter);
                        command.Parameters.AddWithValue("@solgteloddermobilepay", barnInfo.solgte_lodder_mobilepay);
                        command.Parameters.AddWithValue("@modtagetlodder", barnInfo.modtaget_lodder);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Barn/Index");
        }
    }
}

