using Booket.BuildingBlocks.Domain;

namespace Booket.Modules.UserManagement.Domain.Users;

public class UserId(Guid value) 
    : TypedIdValueBase(value);