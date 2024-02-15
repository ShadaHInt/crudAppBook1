using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationBook1.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<BookInfo> ListBook = new List<BookInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=SHADAN\\SQLEXPRESS;Initial Catalog=BookList;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    String sql = " SELECT * FROM Book";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                             BookInfo info = new BookInfo();
                                info.BookId = "" + reader.GetInt32(0);
                                info.BookName = reader.GetString(1);
                                info.AuthorName = reader.GetString(2);

                                ListBook.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
				ViewData["ErrorMessage"] = "An error occurred while fetching book details: " + ex.Message;
			}
            
        }
    }

    public class BookInfo
    {
        public string BookId;
        public string BookName;
        public string AuthorName;
    }
}
