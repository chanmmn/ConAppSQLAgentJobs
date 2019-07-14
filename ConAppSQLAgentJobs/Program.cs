using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Specialized;
using System.IO;

namespace ConAppSQLAgentJobs
{
    class Program
    {

        static void Main(string[] args)

        {
            StringCollection sc = new StringCollection();
            ScriptingOptions so = new ScriptingOptions();
            so.IncludeDatabaseContext = true;


            //Setup connection, this is windows authentication
            ServerConnection conn = new ServerConnection();
            conn.LoginSecure = true;
            conn.ServerInstance = "localhost";
            Server srv = new Server(conn);
            
            string script = "";

            string JobName;
            //Loop over all the jobs
            foreach (Job J in srv.JobServer.Jobs)
            {

                //Output name in the console
                Console.WriteLine(J.Name.ToString());

                JobName = J.Name.ToString();
                sc = J.Script(so);

                //Get all the text for the job
                foreach (string s in sc)
                {
                    script += s;
                }

                //Generate the file
                TextWriter tw = new StreamWriter("d:\\temp\\sqlagentjob\\" + JobName + ".sql");
                tw.Write(script);
                tw.Close();

                //Make the string blank again
                script = "";
            }
        }

    }
}
