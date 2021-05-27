using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Configuration;
using Indigox.Common.Logging;
using System.Collections;

namespace Indigox.Common.ExchangeManager
{
    public class PSExecutor
    {
        public string Script { get; set; }

        public IDictionary<string, object> Parameters { get; set; }

        private static WSManConnectionInfo connInfo;

        public PSExecutor()
        {
            this.Parameters = new Dictionary<string, object>();
            if (connInfo == null)
            {
                string exchangeServer = ConfigurationManager.AppSettings["ExchangeServer"];
                string runasUsername = ConfigurationManager.AppSettings["Account"];
                string runasPassword = ConfigurationManager.AppSettings["PWD"];
                SecureString ssRunasPassword = new SecureString();
                foreach (char x in runasPassword)
                {
                    ssRunasPassword.AppendChar(x);
                }
                PSCredential credentials =
                    new PSCredential(runasUsername, ssRunasPassword);
                connInfo = new WSManConnectionInfo(new Uri("http://" + exchangeServer + "/PowerShell"),
                    "http://schemas.microsoft.com/powershell/Microsoft.Exchange",
                    credentials);
                connInfo.AuthenticationMechanism = AuthenticationMechanism.Kerberos;
            }
        }

        public IList<Hashtable> Execute()
        {
            Log.Debug(String.Format("Excute Script:{0}", this.Script));
            List<Hashtable> list = new List<Hashtable>();

            Runspace runspace = RunspaceFactory.CreateRunspace(connInfo);
            Command command = new Command(this.Script, true);
            //foreach (string key in this.Parameters.Keys)
            //{
            //    command.Parameters.Add(key, this.Parameters[key]);
            //}

            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.Add(command);
            Collection<PSObject> results = pipeline.Invoke();
            //Console.WriteLine(String.Format("ps has {0} result(s)", results.Count));
            foreach (PSObject result in results)
            {
                Hashtable item = new Hashtable();
                foreach (PSPropertyInfo  property in result.Properties)
                {
                    //Log.Debug(String.Format("ps result {0}={1}", property.Name, property.Value));
                    //Console.WriteLine(String.Format("ps result {0}={1}", property.Name, property.Value));
                    item.Add(property.Name, property.Value);
                }
                list.Add(item);
            }
            if (pipeline.Error.Count > 0)
            {
                Log.Debug("通道错误数：" + pipeline.Error.Count);
                Console.WriteLine("通道错误数：" + pipeline.Error.Count);
                for (int i = 0, count = pipeline.Error.Count; i < count; i++)
                {
                    Collection<ErrorRecord> errors = (Collection<ErrorRecord>)pipeline.Error.Read();
                    foreach (ErrorRecord error in errors)
                    {
                        Log.Debug(this.Script + "\r\n" + error.Exception.ToString()
                            + "\r\n" + error.Exception.Message
                            + "\r\n" + error.Exception.StackTrace);
                        Console.WriteLine(error.Exception.ToString());
                        runspace.Dispose();
                        throw error.Exception;
                    }
                }
            }
            //foreach (PSObject result in results)
            //{
            //    Console.WriteLine("Members:");
            //    foreach (PSMemberInfo member in result.Members)
            //    {
            //        Console.WriteLine(member.Name + "=" + member.Value);
            //    }
            //    Console.WriteLine("Methods:");
            //    foreach (PSMethodInfo method in result.Methods)
            //    {
            //        Console.WriteLine(method.Name + "=" + method.Value);
            //    }
            //    Console.WriteLine("Properties:");
            //    foreach (PSPropertyInfo property in result.Properties)
            //    {
            //        Console.WriteLine(property.Name + "=" + property.Value);
            //    }
            //    Console.WriteLine("TypeNames:");
            //    foreach (string typeName in result.TypeNames)
            //    {
            //        Console.WriteLine(typeName);
            //    }
            //}
            runspace.Dispose();
            return list;
        }
    }
}
