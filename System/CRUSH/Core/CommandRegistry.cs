using System;
using System.Collections.Generic;
using Desmux.System.CRUSH.Core.DebugCommands;
using Desmux.System.CRUSH.Core.FileSystemCommands;
using Desmux.System.CRUSH.Core.SystemCommands;
using static Desmux.System.CRUSH.Core.CommandManager;

namespace Desmux.System.CRUSH.Core
{
    public class CommandRegistry
    {
        public CommandRegistry()
        {
            Register(new ShutdownCommand());
            Register(new RebootCommand());
            Register(new ClearCommand());
            Register(new EchoCommand());
            Register(new PanicCommand());
            Register(new NeofetchCmd());
            Register(new LsCommand());
            Register(new CdCommand());
            Register(new PwdCommand());
            Register(new CatCommand());
            Register(new TouchCommand());
            Register(new RmCommand());
            Register(new MkdirCommand());
            Register(new RmdirCommand());
            Register(new MvCommand());
            Register(new CopyCommand());
            Register(new AddrInfo());
            Register(new HechoCommand());
            Register(new HelpCommand());
        }
    }
}