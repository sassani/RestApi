using EF_V0.Core.Helpers;
using EF_V0.Core.Models.Requests;
using EF_V0.DataBase.Core;
using System;
using System.Collections.Generic;

namespace EF_V0.Core.Models
{
	public class User
	{
		public int Id { get; set; }
		public string PublicId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; private set; }

		public bool IsEmailVerified { get; set; }
		public bool IsActive { get; set; }
		public DateTime? LastLoginAt { get; set; }

		public HashSet<string> Roles { get; set; }

		private readonly IUnitOfWork unitOfWork;

		public User(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			Roles = new HashSet<string>();
		}

		public bool Authenticate(LoginUser loginUser)
		{
			// get data from credential
			var dbUser = unitOfWork.User.FindByEmail(loginUser.Email);
			if (dbUser != null)
			{
				// check password
				if (StringHelper.CompareStringToHash(dbUser.Password, loginUser.Password))
				{
					MapDbModelToClassModel(dbUser);
					foreach (var ur in dbUser.UserRole)
					{
						Roles.Add(ur.Role.Name);
					}
					return true;
				}
			}

			return false;
		}

		// TODO: put this method in a parent class
		private void MapDbModelToClassModel(object dbModel)
		{
			foreach (var prop in GetType().GetProperties())
			{
				var dbmProp = dbModel.GetType().GetProperty(prop.Name);
				if (dbmProp != null)
				{
					prop.SetValue(this, dbmProp.GetValue(dbModel, null));
				}
			}
		}



	}

}
