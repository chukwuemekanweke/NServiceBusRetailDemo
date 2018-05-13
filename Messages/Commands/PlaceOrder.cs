using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Messages.Commands
{
    public class PlaceOrder:ICommand
    {
        public string OrderId { get; set; }

    }
}
