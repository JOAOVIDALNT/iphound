namespace iphound.API.Exceptions
{
    public class InvalidIpException : AppBaseException
    {
        public InvalidIpException() : base("Invalid Ip!") { }
    }
}
