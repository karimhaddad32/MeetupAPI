using Microsoft.AspNetCore.Authorization;

namespace MeetupAPI.Authorization
{
    public enum OperationType
    {
        Create,
        Read,
        Update,
        Delete
    }

    public class ResourceOperationRequirement(OperationType operationType) : IAuthorizationRequirement
    {
        public OperationType OperationType { get; } = operationType;
    }
}
