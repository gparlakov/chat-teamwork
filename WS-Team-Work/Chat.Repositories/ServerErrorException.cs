using System;

namespace Chat.Repositories
{
    public class ServerErrorException : Exception
    {
        public ServerErrorException()
            : base()
        {
        }

        public ServerErrorException(string msg)
            : base(msg)
        {
        }

        public ServerErrorException(string msg, string errCode)
            : base(msg)
        {
            this.ErrorCode = errCode;
        }

        public string ErrorCode { get; set; }
    }
}
