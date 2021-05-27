using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Configuration;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Management.Automation.Remoting;
using System.Collections.ObjectModel;
using Indigox.Common.Logging;

namespace Indigox.Common.ExchangeManager
{
    public class ComplexPSExecutor
    {
        public void ExecuteScript(string script)
        {
            string exchangeServer = ConfigurationManager.AppSettings["ExchangeServer"];
            string runasUsername = ConfigurationManager.AppSettings["Account"];
            string runasPassword = ConfigurationManager.AppSettings["PWD"];

            System.Uri uri = new Uri("http://" + exchangeServer + "/PowerShell?serializationLevel=Full");
            SecureString securePassword = new SecureString();
            for (int i = 0; i < runasPassword.Length; i++)
            {
                securePassword.AppendChar(runasPassword[i]);
            }

            PSCredential creds = new PSCredential(runasUsername, securePassword);

            Runspace runspace = RunspaceFactory.CreateRunspace();

            PowerShell powershell = PowerShell.Create();
            PSCommand command = new PSCommand();
            command.AddCommand("New-PSSession");
            command.AddParameter("ConfigurationName", "Microsoft.Exchange");
            command.AddParameter("ConnectionUri", uri);
            command.AddParameter("Credential", creds);
            command.AddParameter("Authentication", "Default");
            PSSessionOption sessionOption = new PSSessionOption();
            sessionOption.SkipCACheck = true;
            sessionOption.SkipCNCheck = true;
            sessionOption.SkipRevocationCheck = true;
            command.AddParameter("SessionOption", sessionOption);

            powershell.Commands = command;

            try
            {
                // open the remote runspace
                runspace.Open();
                // associate the runspace with powershell
                powershell.Runspace = runspace;

                // invoke the powershell to obtain the results
                Collection<PSSession> result = powershell.Invoke<PSSession>();

                foreach (ErrorRecord current in powershell.Streams.Error)
                {
                    Log.Debug("连接Exchange错误：" + current.Exception.ToString());
                    Console.WriteLine("连接Exchange错误：" + current.Exception.ToString());
                }

                if (result.Count != 1)
                    throw new Exception("Unexpected number of Remote Runspace connections returned.");

                // Set the runspace as a local variable on the runspace
                powershell = PowerShell.Create();
                command = new PSCommand();
                command.AddCommand("Set-Variable");
                command.AddParameter("Name", "ra");
                command.AddParameter("Value", result[0]);
                powershell.Commands = command;
                powershell.Runspace = runspace;
                powershell.Invoke();


                // First import the cmdlets in the current runspace (using Import-PSSession)
                powershell = PowerShell.Create();
                command = new PSCommand();
                command.AddScript("Import-PSSession -Session $ra");
                powershell.Commands = command;
                powershell.Runspace = runspace;
                powershell.Invoke();

                powershell = PowerShell.Create();                
                command = new PSCommand();
                command.AddScript(script);
                powershell.Commands = command;
                powershell.Runspace = runspace;

                Collection<PSObject> results = new Collection<PSObject>();
                results = powershell.Invoke();

                foreach (ErrorRecord current in powershell.Streams.Error)
                {
                    Log.Debug("执行脚本错误：" + current.Exception.ToString());
                    Console.WriteLine("执行脚本错误：" + current.Exception.ToString());
                }

                results = null;
            }
            finally
            {
                // dispose the runspace and enable garbage collection
                runspace.Dispose();
                runspace = null;

                // Finally dispose the powershell and set all variables to null to free up any resources.
                powershell.Dispose();
                powershell = null;
            }
        }
    }
}
