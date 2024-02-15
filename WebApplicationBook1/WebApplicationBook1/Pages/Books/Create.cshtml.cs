using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationBook1.Pages.Books
{
	public class CreateModel : PageModel
	{
		public BookInfo binfo = new BookInfo();
		public string errorMessage = "";
		public string SuccessMessage = "";
		public void OnGet()
		{

		}
		public void OnPost()
		{
			binfo.BookName = Request.Form["name"];
			binfo.AuthorName = Request.Form["author"];
		
			


			if (binfo.BookName.Length == 0 || binfo.AuthorName.Length == 0 )
			{
				errorMessage = "All the fields are required";
				return;
			}

			// save newly added book to the database
			try
			{
				String connectionString = "Data Source=SHADAN\\SQLEXPRESS;Initial Catalog=BookList;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					String sql = "INSERT INTO Book (BookName, AuthorName) VALUES (@BookName, @AuthorName);";

					using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
					{
						sqlCommand.Parameters.AddWithValue("@BookName", binfo.BookName);
						sqlCommand.Parameters.AddWithValue("@AuthorName", binfo.AuthorName);
						
						

						sqlCommand.ExecuteNonQuery();
					}
				}

				binfo.BookName = "";
				binfo.AuthorName = "";
				
			
				SuccessMessage = "New Book is added successfully ";
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			 Response.Redirect("/Books"); 
		}

	}
}
