using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Management.Automation.Remoting;
using System.Collections.ObjectModel;
using System.Security;
using System.Collections;
using SysConsole = System.Console;

namespace Indigox.Common.ExchangeManager.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //SysConsole.WriteLine(args[0]);
            PSExecutor executor = new PSExecutor()
            {
                Script = "New-AddressList -Name Test -DisplayName Test -Container '\\' -RecipientFilter {(Alias -like '*') -and (DisplayName -like 'A*' -or CustomAttribute1 -like '*,A*,*') -and (ObjectCategory -like 'user' -or ObjectCategory -like 'group')}"
            };
            executor.Execute();
             * */

            
            string password = "dlzengyong";
            string userName = "dev\\dl-zengyong";

            System.Uri uri = new Uri("http://mail01VM/PowerShell?serializationLevel=Full");
            System.Security.SecureString securePassword = String2SecureString(password);

            PSCredential creds = new PSCredential(userName, securePassword);

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
                    SysConsole.WriteLine("Exception: " + current.Exception.ToString());
                    SysConsole.WriteLine("Inner Exception: " + current.Exception.InnerException);
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

                //command = new PSCommand();
                //command.AddScript("New-AddressList");
                //command.AddParameter("Name", "Test");
                //command.AddParameter("DisplayName", "Test");
                //command.AddParameter("Container", "\\");
                //command.AddParameter("RecipientFilter", "{(Alias -like '*') -and (DisplayName -like 'A*' -or CustomAttribute1 -like '*,A*,*') -and (ObjectCategory -like 'user' -or ObjectCategory -like 'group')}");

                command = new PSCommand();
                command.AddScript("New-AddressList -Name Test -DisplayName Test -Container '\\' -RecipientFilter {(Alias -like '*') -and (DisplayName -like 'A*' -or CustomAttribute1 -like '*,A*,*') -and (ObjectCategory -like 'user' -or ObjectCategory -like 'group')}");
                //command.AddScript("New-AddressList -Name Test -DisplayName Test -Container '\\E.测试公司' ");
                
                //Change the name of the database
                //command = new PSCommand();
                //command.AddCommand("Get-MailboxDatabase");
                //command.AddParameter("Identity", "OprDB_VIP");
                
                //powershell.Commands.AddScript("Get-MailboxDatabase");
                //command = new PSCommand();
                //command.AddScript("Get-MailboxDatabase -Identity OprDB_VIP");

                //command = new PSCommand();
                //command.AddScript("Invoke-Command -ScriptBlock { New-AddressList -Name Test -DisplayName Test -Container '\' -RecipientFilter {(Alias -like '*') -and (DisplayName -like 'A*' -or CustomAttribute1 -like '*,A*,*') -and (ObjectCategory -like 'user' -or ObjectCategory -like 'group')} } -Session $ra");

                powershell.Commands = command;
                powershell.Runspace = runspace;

                Collection<PSObject> results = new Collection<PSObject>();
                results = powershell.Invoke();

                SysConsole.WriteLine("New-Addresslist execute success!");
                SysConsole.WriteLine("Results count : " + results.Count);

                foreach (ErrorRecord current in powershell.Streams.Error)
                {
                    SysConsole.WriteLine("Exception: " + current.Exception.ToString());
                    SysConsole.WriteLine("Inner Exception: " + current.Exception.InnerException);
                }

                PSMemberInfo Member = null;
                foreach (PSObject oResult in results)
                {
                    foreach (PSMemberInfo psMember in oResult.Members)
                    {
                        Member = psMember;
                        DumpProperties(ref Member);
                    }
                }

                results = null;
                Member = null;
            }
            catch (Exception e)
            {
                SysConsole.WriteLine(e.ToString());
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

        //Method to Dump out the Properties
        public static void DumpProperties(ref PSMemberInfo psMember)
        {
            // Only look at Properties
            if (psMember.MemberType.ToString() == "Property")
            {
                switch (psMember.Name)
                {
                    case "ActivationPreference":
                    case "DatabaseCopies":
                        if (psMember.Value != null)
                        {
                            PSObject oPSObject;
                            ArrayList oArrayList;
                            oPSObject = (PSObject)psMember.Value;
                            oArrayList = (ArrayList)oPSObject.BaseObject;
                            SysConsole.WriteLine("Member Name:" + psMember.Name);
                            SysConsole.WriteLine("Member Type:" + psMember.TypeNameOfValue);
                            SysConsole.WriteLine("----------------------");
                            foreach (string item in oArrayList)
                            {
                                SysConsole.WriteLine(item);
                            }
                            SysConsole.WriteLine("----------------------");
                        }
                        break;
                }
            }
        }

        private static SecureString String2SecureString(string password)
        {
            SecureString remotePassword = new SecureString();
            for (int i = 0; i < password.Length; i++)
                remotePassword.AppendChar(password[i]);
            return remotePassword;
        }
    }
}
