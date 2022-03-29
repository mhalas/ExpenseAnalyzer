namespace Api.HandlerResponses
{
    public class GeneralResponse
    {
        public GeneralResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public GeneralResponse(string message)
        {
            IsSuccess = false;

            Message = message;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
