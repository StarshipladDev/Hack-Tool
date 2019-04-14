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
            //"http://www.starshiplad.com/sendit.php?name=Test+Data&info=This+si+important+information"
            string url = textBox.Text;
            String output = "";
            int index = url.IndexOf('?');
            int i = 0;
            string[] urlParameters = url.Remove(0, index + 1).Split('&');
            string[] phonyrequests = new String[urlParameters.Length];
            StreamReader reader = null;
                
            foreach(string p in urlParameters)
            {
                //Run to test for XSS ability
                phonyrequests[i] = url.Replace(p,p+"<xss>");
                output += "Attempted to send: "+ phonyrequests[i] + "\n";
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                webRequest.Method = "GET";
                string response = "";
                reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                response = reader.ReadToEnd();
                if (response.Contains("<xss>"))
                {
                    output += "XSS vunrability in param " + p + "\n";
                }
                //Repeat for SQL injection
                phonyrequests[i] = url.Replace(p, p + "bad'info");
                output += "Attempted to send: " + phonyrequests[i] + "\n";
                webRequest = (HttpWebRequest)HttpWebRequest.Create(phonyrequests[i]);
                webRequest.Method = "GET";
                response = "";

                reader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                response = reader.ReadToEnd();
                if (response.Contains("bad'info"))
                {
                    output += "SQL injection vunrability in param " + p +"\n";
                }
                i++;
            }
            label1.Text = output;
        }
    }
}
