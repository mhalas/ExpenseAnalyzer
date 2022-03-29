using DataAccess.Contracts.Shared;

namespace Api.HandlerResponses
{
    public class CreatedResponse : GeneralResponse
    {
        public CreatedResponse(IId item, bool isSuccess, string message) : base(isSuccess, message)
        {
            Item = item;
        }

        public CreatedResponse(string message) 
            : base(message)
        {
        }

        public IId Item { get; }
    }
}
