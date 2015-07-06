
namespace Swaksoft.Core.Dto
{
    public enum ActionResultCode
    {
        Unauthorized = -1,
        Unauthenticated = -2,
        Blocked = -3,
        Success = 0,
        Failed = 1,
        Errored = 2,
        ConnectFailure = 3
    }
}
