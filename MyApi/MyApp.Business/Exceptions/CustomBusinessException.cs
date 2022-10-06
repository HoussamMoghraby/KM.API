namespace MyApp.Business.Exceptions
{
    public class CustomBusinessException : Exception
    {
        public ErrorType Type { get; set; }
        public string Code { get; set; } = string.Empty;
        public CustomBusinessException(string errorMessage) : base(errorMessage) { }

        public CustomBusinessException(ErrorType errorType, string errorMessage) : base(errorMessage)
        {
            Type = errorType;
        }
    }

    public enum ErrorType
    {
        BadRequest = 0,
        NotFound,
        Other
    }

    //public class ExceptionConstants
    //{
    //    public const string WrongEmailCode = "wrong_email";
    //    public const string WrongPasswordCode = "wrong_password";


    //    public const string WrongEmailMessage = "This email does not already exist";
    //    public const string WrongPasswordMessage = "The password provided is incorrect";
    //}
}
