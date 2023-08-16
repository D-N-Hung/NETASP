using static Chotot.ChototMOD.UserMOD;
using System.Data;
using System.Text;

namespace Chotot.ChototDAL
{
	public class UserDAL
	{
		private string strCon = "Data Source=DESKTOP-D81J0HS\\SQLEXPRESS;Initial Catalog=banhang;Persist Security Info=True;User ID=Hungnb;Password=123456;Trusted_Connection=True;TrustServerCertificate=True";
		SqlConnection con = new SqlConnection();
		private void ConnectData(string Command, SqlCommand cmd, int? kq)
		{
			con = new SqlConnection(strCon);
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = Command;
			cmd.Connection = con; 
			con.Open();
			cmd.ExecuteNonQuery();
		}

		public ResultMOD RegisterDAL(AccRegisMOD item)
		{
			Console.OutputEncoding = Encoding.UTF8;
			ResultMOD Result = new ResultMOD();
			var kiemtra = KiemTraTonTaiPhone(item.PhoneNumber);
			string hashedPassword = PasswordHelper.HashPassword(item.Password);
			string query = "INSERT INTO [Accounts] (UserName,Phone,Password,IDRole) VALUES("
					+ "'" + item.Name + "'"
					+ ",'" + item.PhoneNumber + "'"
					+ ",'" + hashedPassword + "'"
					+ "," + item.IDRole + ")";
			try
			{
				SqlCommand cmd = new SqlCommand();

				if (kiemtra == true)
				{
					ConnectData(query, cmd, null);
					if (con != null)
					{
						Result.Status = 1;
						Result.Message = "Register Sucess";
						Result.Data = 1;
					}
					else
					{
						Result.Status = -1;
						Result.Message = "Register Failed";
						Result.Data = -1;
					}
				}
				else
				{
					Console.WriteLine("Số đt này đã được đăng ký");
				}

				con!.Close();
				return Result;
			}
			catch (Exception e)
			{
				throw new Exception("Have Erorr : " + e.Message);
			}
		}

		public bool KiemTraTonTaiPhone(int Phone)
		{
			var Result = false;

			string query = "SELECT COUNT(*) FROM Accounts WHERE Phone = '" + Phone + "'";

			try
			{
				using (SqlConnection con = new SqlConnection(strCon))
				{
					con.Open();

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@Phone", Phone);

						int kq = (int)cmd.ExecuteScalar();

						if (kq <= 0)
						{
							Result = true;
						}
						else
						{
							Result = false;
						}
					}
				}
				return Result;
			}
			catch (Exception e)
			{
				throw; 
			}
		}

		public AccInfoMOD LoginDAL(int PhoneNumber, string Password)
		{
			Console.OutputEncoding = Encoding.UTF8;
			AccInfoMOD item = new AccInfoMOD();
			string hashedPassword = PasswordHelper.HashPassword(Password);
			string query = "SELECT * FROM [Accounts] WHERE Phone = '" + PhoneNumber + "' AND Password = '" + hashedPassword + "'";

			try
			{
				SqlCommand cmd = new SqlCommand();

				ConnectData(query, cmd, null);

				int Idrole = GetIDRole(PhoneNumber, hashedPassword);
				string NameRole = GetNameRole(Idrole);
				List<string> NameCN = GetChucNang(Idrole);

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					item.UserName = reader.GetString(1);
					item.PhoneNumber = reader.GetInt32(2);
					item.NameRole = NameRole;
					item.NameCN = NameCN;
					break;
				}

				reader.Close();
				con.Close();
				return item;
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public string GetNameRole(int idRole)
		{
			string query = "SELECT Rolename FROM Roles WHERE ID = '" + idRole + "'";
			SqlCommand cmd = new SqlCommand();
			string nameRole = "";
			ConnectData(query, cmd, null);
			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.Read())
			{
				nameRole = reader.GetString(0);
				reader.Close();
				return nameRole;
			}
			else
			{
				return "";
			}
		}

		public int GetIDRole(int PhoneNumber, string Password)
		{
			int Idrole = 0;
			string query = "SELECT IDRole FROM Accounts WHERE Phone = '" + PhoneNumber + "' AND Password = '" + Password + "'";

			using (SqlConnection connection = new SqlConnection(strCon))
			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
				cmd.Parameters.AddWithValue("@Password", Password);

				connection.Open();

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						Idrole = reader.GetInt32(0);
						reader.Close();
					}
				}
			}
			return Idrole;
		}

		public List<string> GetChucNang(int idRole)
		{
			List<string> nameFuncList = new List<string>();
			string query = "SELECT Namefunc FROM [Function] WHERE IDRole = @IDRole";

			using (SqlConnection connection = new SqlConnection(strCon))
			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				cmd.Parameters.AddWithValue("@IDRole", idRole);

				connection.Open();

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string nameFunc = reader.GetString(0);
						nameFuncList.Add(nameFunc);
					}
				}
			}
			return nameFuncList;
		}
	}
}
