namespace Mep01Web.Exeptions
{
    public class CrrgException : Exception
    {
        public int Code { get; set; }

        public CrrgException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
