namespace Mep01Web.Exeptions
{
    public class TatvException : Exception
    {
        public int Code { get; set; }

        public TatvException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
