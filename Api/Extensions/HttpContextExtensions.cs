namespace Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return "";
            }
            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}
