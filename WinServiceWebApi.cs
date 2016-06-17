using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;

namespace Windows_Service_Web_API_2_Sample
{
    public partial class WinServiceWebApi : ServiceBase
    {

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        private int _eventId;

        internal static string EventSourceName = "WinServiceWebApi_EventSource";
        internal string LogName = "Win Service Web API2";
        private System.Web.Http.SelfHost.HttpSelfHostServer WebApiSelfHostedSampleServer;

        [Conditional("DEBUG")]
        private static void DebugMode()
        {
            Debugger.Break();
        }


        public WinServiceWebApi(string[] args)
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

            ConfigAndRunWebAPI();

        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Win Service Web API2 - Stopped");
            WebApiSelfHostedSampleServer.CloseAsync().Wait();
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

        internal void ConfigAndRunWebAPI()
        {

            var serviceUrl = ConfigurationManager.AppSettings["WinServiceWebAPIUrl"];
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(serviceUrl);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            WebApiSelfHostedSampleServer = new HttpSelfHostServer(config);

            try
            {
                WebApiSelfHostedSampleServer.OpenAsync().Wait();
                EventLog.WriteEntry("Print Service Web API Started");
            }
            catch (Exception ex)
            {

                EventLog.WriteEntry("Web API Server Service start FAIL:: " + Environment.NewLine + "Exception: " + Environment.NewLine + ex.ToString() + Environment.NewLine + " Inner Exception: " + ex.InnerException.ToString(), EventLogEntryType.Error);

            }
        }

    }
}
