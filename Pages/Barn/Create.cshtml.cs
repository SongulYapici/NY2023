using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace NY2023.Pages.Barn
{
    public class CreateModel : PageModel
    {
        public BarnInfo barnInfo = new BarnInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            barnInfo.id = Request.Form["id"];
            barnInfo.navn = Request.Form["navn"];
            barnInfo.f�dselsdato = Request.Form["f�dselsdato"];
            barnInfo.k�n = Request.Form["k�n"];
            barnInfo.telefonnummer = Request.Form["telefonnummer"];
            barnInfo.email = Request.Form["email"];
            barnInfo.solgte_lodder_kontanter = Request.Form["solgte_lodder_kontanter"];
            barnInfo.solgte_lodder_mobilepay = Request.Form["solgte_lodder_mobilepay"];
            barnInfo.modtaget_lodder = Request.Form["modtaget_lodder"];

            if (barnInfo.id.Length == 0 || barnInfo.navn.Length == 0 ||
                barnInfo.f�dselsdato.Length == 0 || barnInfo.k�n.Length == 0 ||
                barnInfo.telefonnummer.Length == 0 || barnInfo.email.Length == 0 ||
                barnInfo.solgte_lodder_kontanter.Length == 0 ||
                barnInfo.solgte_lodder_mobilepay.Length == 0 || barnInfo.modtaget_lodder.Length == 0)
            {
                errorMessage = "Alle felter skal udfyldes";
                return;
            }

            //save the new client into the database
            try
            {
                string connectionString = "Data Source=LAPTOP-VTMJHV3I;Initial Catalog=MyDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Barn" +
                                 "(id, navn, f�dselsdato, k�n, telefonnummer, email, solgtelodderkontanter, solgteloddermobilepay, modtagetlodder) VALUES " +
                                 "(@id, @navn, @f�dselsdato, @k�n, @telefonnummer, @email, @solgtelodderkontanter, @solgteloddermobilepay, @modtagetlodder);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", barnInfo.id);
                        command.Parameters.AddWithValue("@navn", barnInfo.navn);
                        command.Parameters.AddWithValue("@f�dselsdato", barnInfo.f�dselsdato);
                        command.Parameters.AddWithValue("@k�n", barnInfo.k�n);
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

            barnInfo.id = ""; barnInfo.navn = ""; barnInfo.f�dselsdato = ""; barnInfo.k�n = ""; barnInfo.telefonnummer = ""; barnInfo.email = "";
            barnInfo.solgte_lodder_kontanter = ""; barnInfo.solgte_lodder_mobilepay = ""; barnInfo.modtaget_lodder = "";
            successMessage = "Ny barn tilf�jet korrekt";

            Response.Redirect("/Barn/Index");
        }
    }
}
