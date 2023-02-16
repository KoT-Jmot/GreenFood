using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFood.Domain.Models.Utils
{
    public static class AccountRoles
    {
        public static string GetDefaultRole() => "User";
        public static string GetAdministratorRole() => "Admin";
    }
}
