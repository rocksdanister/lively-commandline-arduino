using System;

namespace Lively.IPC.Errors
{
    public class ApplicationNotFoundException : Exception
    {
        public ApplicationNotFoundException()
        {
        }

        public ApplicationNotFoundException(string message)
            : base(message)
        {
        }

        public ApplicationNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
