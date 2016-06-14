using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Service_Web_API_2_Sample
{
    public partial class WinServiceWebApi : ServiceBase
    {

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        private int _eventId;

        internal static string EventSourceName = "WinServiceWebApi_EventSource";
        internal string LogName = "Win Service Web API2";

        public WinServiceWebApi()
        {
            InitializeComponent();

            if (!System.Diagnostics.EventLog.SourceExists(EventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(EventSourceName, LogName);
            }
            eventLog.Source = EventSourceName;
            eventLog.Log = LogName;

        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Win Service Web API2 - Started");

            // Set up a timer to trigger every minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 600000; // 600*1000ms = 10 minutes timeout
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);


        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Win Service Web API2 - Stopped");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog.WriteEntry("Monitoring the System -- you can do periodic work here", EventLogEntryType.Information, _eventId++);
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }


    }
}
