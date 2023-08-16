using static Chotot.ChototMOD.UserMOD;

namespace Chotot.ChototBUS
{
	public class UserBUS
	{
		public ResultMOD RegisterBUS(AccRegisMOD item)
		{
			var Result = new ResultMOD();
			if (item == null || item.Name == null || item.Password == null || item.PhoneNumber == null)
			{
				Result.Status = -1;
				Result.Message = "Missing Infomation";
			}
			return new AccDAL().RegisterDAL(item);
		}


		public ResultMOD LoginBUS(AccLoginMOD user)
		{
			var Result = new ResultMOD();

			try
			{
				if (user.PhoneNumber == null || user.PhoneNumber.ToString() == "")
				{
					Result.Status = 0;
					Result.Message = "PhoneNumber không được để trống";
					return Result;
				}
				else if (user.Password == null || user.Password == "")
				{
					Result.Status = 0;
					Result.Message = "Mật khẩu không được để trống";
					return Result;
				}
				else
				{
					var userLogin = new AccDAL().LoginDAL(user.PhoneNumber, user.Password);
					if (userLogin != null && userLogin.PhoneNumber == user.PhoneNumber)
					{
						Result.Status = 1;
						Result.Message = "Đăng nhập thành công";
						Result.Data = userLogin;
					}
					else
					{
						Result.Status = 0;
						Result.Message = "Tài khoản hoặc mật khẩu không đúng!";
					}
					return Result;
				}
			}
			catch (Exception)
			{
				Result.Status = -1;
				Result.Message = "Xảy ra lỗi trong quá trình thêm mới dữ liệu. Vui lòng liên hệ quản trị để biết thêm thông tin!";
				throw;
			}
			return Result;
		}
	}
}
