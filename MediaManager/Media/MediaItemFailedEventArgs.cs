using System;
using MediaManager.Media;

namespace MediaManager.Media
{
    public class MediaItemFailedEventArgs : MediaItemEventArgs
    {
        public MediaItemFailedEventArgs(IMediaItem Item, Exception Exception, string Message) : base(Item)
        {
            this.Exeption = Exception;
            this.Message = Message;
        }

        public Exception Exeption { get; private set; }
        public string Message { get; private set; }
    }
}
