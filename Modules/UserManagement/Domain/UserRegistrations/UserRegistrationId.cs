using Booket.BuildingBlocks.Domain;

namespace Booket.Modules.UserManagement.Domain.UserRegistrations;

public class UserRegistrationId(Guid value) 
    : TypedIdValueBase(value);