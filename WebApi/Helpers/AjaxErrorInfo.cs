namespace WebApi.Helpers
{
    public class AjaxErrorInfo
    {
        public string Message { get; private set; }
        public string Info { get; private set; }

        public AjaxErrorInfo()
        {
            Message = "";
            Info = "";
        }

        public AjaxErrorInfo(Exception e)
        {
            Message = e.Message;
            Info = "";
            Info += ": STACK TRACE: ";
            Info += e.StackTrace;

            if(e.InnerException != null)
            {
                AjaxErrorInfo innerError = new AjaxErrorInfo(e.InnerException);
                Info += "INNER_EXCEPTION: { ";
                Info += innerError.Message;
                Info += " Info: ";
                Info += innerError.Info;
                Info += " }";
            }

        }
    }
}
