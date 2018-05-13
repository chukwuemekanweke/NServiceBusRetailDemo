using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace ClientUI.Messages
{
    public  class DoSomething:ICommand
    {
        public string SomeProperty { get; set; }
    }
}
