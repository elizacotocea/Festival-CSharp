using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{


    public enum UpdateType
    {
        TICKETS_SOLD
    };
    public class UserEventArgs : EventArgs
    {
        private readonly UpdateType userEvent;
        private readonly Object data;

        public UserEventArgs(UpdateType userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }

        public UpdateType UserEventType
        {
            get { return userEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }

}
