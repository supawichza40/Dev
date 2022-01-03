// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System;

namespace NUnit.Engine.Communication.Messages
{
    [Serializable]
    public class CommandReturnMessage : TestEngineMessage
    {
        public CommandReturnMessage(object returnValue)
        {
            ReturnValue = returnValue;
        }

        public object ReturnValue { get; }
    }
}
