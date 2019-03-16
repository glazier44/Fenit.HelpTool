namespace Fenit.Toolbox.Core.Answers
{
    public class Response
    {
        public string Message { get; protected set; }
        public string InfoType { get; protected set; }
        public bool IsError { get; protected set; }

        public bool IsSucces => !IsError;

        public void AddSucces(string message)
        {
            Message = message;
            InfoType = "success";
            IsError = false;
        }

        public void AddError(string message)
        {
            Message = message;
            InfoType = "danger";
            IsError = true;
        }
        public void AddWarning(string message)
        {
            Message = message;
            InfoType = "warning";
            IsError = true;
        }

        public static Response CreateError(string message)
        {
            return new Response()
            {
                IsError = true,
                Message = message,

            };
        }

        public static Response CreateSucces()
        {
            return new Response();
        }
    }
}