namespace Fenit.Toolbox.Core.Answers
{
    public class Response
    {
        public Response()
        {
            ResponseStatus = ResponseStatus.Succes;
        }

        public string Message { get; protected set; }
        public ResponseStatus ResponseStatus { get; protected set; }
        public bool IsError => ResponseStatus == ResponseStatus.Error;

        public bool IsSucces => ResponseStatus == ResponseStatus.Succes;

        public void AddSucces(string message)
        {
            Message = message;
            ResponseStatus = ResponseStatus.Succes;
        }

        public void AddError(string message)
        {
            Message = message;
            ResponseStatus = ResponseStatus.Error;
        }

        public void AddWarning(string message)
        {
            Message = message;
            ResponseStatus = ResponseStatus.Warning;
        }

        public static Response CreateError(string message)
        {
            return new Response
            {
                Message = message,
                ResponseStatus = ResponseStatus.Error
            };
        }

        public static Response CreateSucces()
        {
            return new Response();
        }
    }
}