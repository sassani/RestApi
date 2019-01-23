

using EF_V0.Core.Entities;
using EF_V0.DataBase.Core.Domain;

namespace EF_V0.Core.Helpers
{
	public static class Mapper
	{

		public static void UserMapper(User user, UserDb userDb)
		{
			int missedProps = MapDbModelToClassModel(user, userDb);
			foreach (var ur in userDb.UserRole)
			{
				user.Roles.Add(ur.Role.Name);
			}
		}

		public static int MapDbModelToClassModel(object ntModel, object dbModel, object mapperItems = null)
		{
			int missedPropCount = 0;
			foreach (var ntProp in ntModel.GetType().GetProperties())
			{
				var dbmProp = dbModel.GetType().GetProperty(ntProp.Name);
				if (dbmProp != null)
				{
					ntProp.SetValue(ntModel, dbmProp.GetValue(dbModel, null));
				}
				else
				{
					missedPropCount++;
				}
			}
			return missedPropCount;
		}
	}
}
