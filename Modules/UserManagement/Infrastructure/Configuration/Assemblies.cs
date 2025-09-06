using System.Reflection;
using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IUserManagementModule).Assembly;
    }
}