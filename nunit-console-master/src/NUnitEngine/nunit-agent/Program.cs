// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using NUnit.Common;
using NUnit.Engine;
using NUnit.Engine.Agents;
using NUnit.Engine.Internal;
using NUnit.Engine.Services;

namespace NUnit.Agent
{
    public class NUnitTestAgent
    {
        static Guid AgentId;
        static string AgencyUrl;
        static Process AgencyProcess;
        static RemoteTestAgent Agent;
        private static Logger log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            AgentId = new Guid(args[0]);
            AgencyUrl = args[1];

            var traceLevel = InternalTraceLevel.Off;
            var pid = Process.GetCurrentProcess().Id;
            var debugArgPassed = false;
            var workDirectory = string.Empty;
            var agencyPid = string.Empty;

            for (int i = 2; i < args.Length; i++)
            {
                string arg = args[i];

                // NOTE: we can test these strings exactly since
                // they originate from the engine itself.
                if (arg == "--debug-agent")
                {
                    debugArgPassed = true;
                }
                else if (arg.StartsWith("--trace="))
                {
                    traceLevel = (InternalTraceLevel)Enum.Parse(typeof(InternalTraceLevel), arg.Substring(8));
                }
                else if (arg.StartsWith("--pid="))
                {
                    agencyPid = arg.Substring(6);
                }
                else if (arg.StartsWith("--work="))
                {
                    workDirectory = arg.Substring(7);
                }
            }

            var logName = $"nunit-agent_{pid}.log";
            InternalTrace.Initialize(Path.Combine(workDirectory, logName), traceLevel);
            log = InternalTrace.GetLogger(typeof(NUnitTestAgent));

            log.Info("Agent process {0} starting", pid);

            if (debugArgPassed)
                TryLaunchDebugger();

            LocateAgencyProcess(agencyPid);

#if NETCOREAPP3_1
            log.Info($"Running .NET Core 3.1 agent under {RuntimeInformation.FrameworkDescription}");
#elif NET40
            log.Info($"Running .NET 4.0 agent under {RuntimeFramework.CurrentFramework.DisplayName}");
#elif NET20
            log.Info($"Running .NET 2.0 agent under {RuntimeFramework.CurrentFramework.DisplayName}");
#endif

            // Create CoreEngine
            var engine = new CoreEngine
            {
                WorkDirectory = workDirectory,
                InternalTraceLevel = traceLevel
            };

            // Custom Service Initialization
            engine.Services.Add(new ExtensionService(isRunningOnAgent: true));
#if !NETCOREAPP
            engine.Services.Add(new DomainManager());
#endif
            engine.Services.Add(new InProcessTestRunnerFactory());
            engine.Services.Add(new DriverService());

            // Initialize Services
            log.Info("Initializing Services");
            engine.InitializeServices();

            log.Info("Starting RemoteTestAgent");
            Agent = new RemoteTestAgent(AgentId, engine.Services);
            Agent.Transport =
#if NETFRAMEWORK
                new Engine.Communication.Transports.Remoting.TestAgentRemotingTransport(Agent, AgencyUrl);
#else
                new Engine.Communication.Transports.Tcp.TestAgentTcpTransport(Agent, AgencyUrl);
#endif

            try
            {
                if (Agent.Start())
                    WaitForStop();
                else
                {
                    log.Error("Failed to start RemoteTestAgent");
                    Environment.Exit(AgentExitCodes.FAILED_TO_START_REMOTE_AGENT);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception in RemoteTestAgent. {0}", ExceptionHelper.BuildMessageAndStackTrace(ex));
                Environment.Exit(AgentExitCodes.UNEXPECTED_EXCEPTION);
            }
            log.Info("Agent process {0} exiting cleanly", pid);

            Environment.Exit(AgentExitCodes.OK);
        }

        private static void LocateAgencyProcess(string agencyPid)
        {
            var agencyProcessId = int.Parse(agencyPid);
            try
            {
                AgencyProcess = Process.GetProcessById(agencyProcessId);
            }
            catch (Exception e)
            {
                log.Error($"Unable to connect to agency process with PID: {agencyProcessId}");
                log.Error($"Failed with exception: {e.Message} {e.StackTrace}");
                Environment.Exit(AgentExitCodes.UNABLE_TO_LOCATE_AGENCY);
            }
        }

        private static void WaitForStop()
        {
            log.Debug("Waiting for stopSignal");

            while (!Agent.WaitForStop(500))
            {
                if (AgencyProcess.HasExited)
                {
                    log.Error("Parent process has been terminated.");
                    Environment.Exit(AgentExitCodes.PARENT_PROCESS_TERMINATED);
                }
            }

            log.Debug("Stop signal received");
        }

        private static void TryLaunchDebugger()
        {
            if (Debugger.IsAttached)
                return;

            try
            {
                Debugger.Launch();
            }
            catch (SecurityException se)
            {
                if (InternalTrace.Initialized)
                {
                    log.Error($"System.Security.Permissions.UIPermission is not set to start the debugger. {se} {se.StackTrace}");
                }
                Environment.Exit(AgentExitCodes.DEBUGGER_SECURITY_VIOLATION);
            }
            catch (NotImplementedException nie) //Debugger is not implemented on mono
            {
                if (InternalTrace.Initialized)
                {
                    log.Error($"Debugger is not available on all platforms. {nie} {nie.StackTrace}");
                }
                Environment.Exit(AgentExitCodes.DEBUGGER_NOT_IMPLEMENTED);
            }
        }
    }
}
