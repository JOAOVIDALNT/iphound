namespace iphound.API.Models.HttpModels.Responses
{
    public class ErrorResponse
    {
        public IList<string> Errors { get; set; }

        public ErrorResponse(string error)
        {
            Errors = [error];
        }
        public ErrorResponse(IList<string> errors) => Errors = errors;
    }
}
