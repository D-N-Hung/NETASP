namespace Chotot.ChototMOD
{
	public class UserMOD
	{
		public class AccRegisMOD
		{
			public string? Name { get; set; }
			public int PhoneNumber { get; set; }
			public string? Password { get; set; }
			public int IDRole { get; set; }
		}

		public class AccLoginMOD
		{
			public int PhoneNumber { get; set; }
			public string? Password { get; set; }
		}

		public class AccInfoMOD
		{
			public string? UserName { get; set; }
			public int PhoneNumber { get; set; }
			public string? NameRole { get; set; }
			public List<string>? NameCN { get; set; }
		}
	}
}
