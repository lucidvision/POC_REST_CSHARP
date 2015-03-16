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
using System.Xml;
using System.IO;
using System.Web;

using ConsoleRedirection;


namespace POC_REST_CSHARP
{
    public partial class POC : Form
    {
        TextWriter _writer = null;
        string ltoken ="";
        string IP ="";
        string port ="";
        string userName = "";
        string pw = "";

        public POC()
        {
            InitializeComponent();
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string dformat = "";
            if (xmlRadio.Checked)
            {
                dformat = "xml";
            }
            else if (jsonRadio.Checked)
            {
                dformat = "json";
            }
            else
            {
                Console.WriteLine("Please select a data format type");
            }
            try
            {
            string CUID = rlcBox.Text;
            string server = "http://" + IP + ":" + port + "/biprws/raylight/v1/documents/" + CUID + "/reports/";
            Console.WriteLine("Operation: GET - URI: " +server);
            HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(server);
            GetRequest.Method = "GET";
            GetRequest.Accept = "application/"+dformat;
            GetRequest.Headers.Set("X-SAP-LogonToken", ltoken);
            HttpWebResponse GETResponse = (HttpWebResponse)GetRequest.GetResponse();
            Stream GETResponseStream = GETResponse.GetResponseStream();
            StreamReader sr = new StreamReader(GETResponseStream);
            Console.WriteLine("Response from Server:");
            Console.WriteLine(sr.ReadToEnd());
            }
            catch (Exception)
            {
                if (IP == "" || port == "")
                {
                    Console.WriteLine("Please test your server connection");
                }
                if (ltoken == "")
                {
                    Console.WriteLine("Please login");
                }
                Console.WriteLine("Error in communicating with Server");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IP = IPBox.Text;
                port = portBox.Text;
                string server = "http://" + IP + ":" + port + "/biprws/logon/long";
                Console.WriteLine("Operation: GET - URI: "+server);
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(server);
                GETRequest.Method = "GET";

                HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();
                Stream GETReponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETReponseStream);

                Console.WriteLine("Response from Server:");
                Console.WriteLine(sr.ReadToEnd());
            }
            catch(Exception) 
            {
                if (IP == "")
                {
                    Console.WriteLine("Please Enter a valid Server IP");
                }
                if (port == "")
                {
                    Console.WriteLine("Please Enter a valid port");
                }
                Console.WriteLine("Error in communicating with Server");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dformat = "";
            if(xmlRadio.Checked)
            {
                dformat = "xml";
            }
            else if (jsonRadio.Checked)
            {
                dformat = "json";
            }
            else
            {
                Console.WriteLine("Please select a data format type");
            }
            try
            {
                string server = "http://" + IP + ":" + port + "/biprws/raylight/v1/documents";
                Console.WriteLine("Operation: GET - URI: "+server);
                HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(server);
                GetRequest.Method = "GET";
                GetRequest.Accept = "application/"+dformat;
                GetRequest.Headers.Set("X-SAP-LogonToken", ltoken);
                HttpWebResponse GETResponse = (HttpWebResponse)GetRequest.GetResponse();
                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);
                Console.WriteLine("Response from Server:");
                Console.WriteLine(sr.ReadToEnd());
            }
            catch (Exception)
            {
                if (IP == "" || port == "")
                {
                    Console.WriteLine("Please test your server connection");
                }
                if (ltoken == "")
                {
                    Console.WriteLine("Please login");
                }
                Console.WriteLine("Error in communicating with Server");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string dformat = "";
            if (xml2Radio.Checked)
            {
                dformat = "xml";
            }
            else if (htmlRadio.Checked)
            {
                dformat = "html";
            }
            else
            {
                Console.WriteLine("Please select a data format type");
            }
            try
            {
                string CUID = rlcBox.Text;
                string reportID = ridBox.Text;
                string server = "http://" + IP + ":" + port + "/biprws/raylight/v1/documents/" + CUID + "/reports/" + reportID;
                Console.WriteLine("Operation: GET - URI: "+server);
                HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(server);
                GetRequest.Method = "GET";
                GetRequest.Accept = "text/"+dformat;
                GetRequest.Headers.Set("X-SAP-LogonToken", ltoken);
                HttpWebResponse GETResponse = (HttpWebResponse)GetRequest.GetResponse();
                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);
                string output = sr.ReadToEnd();
                Console.WriteLine("Response from Server:");
                Console.WriteLine(output);
                ReportView.DocumentText = output;
            }
            catch (Exception)
            {
                if (IP == "" || port == "")
                {
                    Console.WriteLine("Please test your server connection");
                }
                if (ltoken == "")
                {
                    Console.WriteLine("Please login");
                }
                Console.WriteLine("Error in communicating with Server");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                userName = userNameBox.Text;
                pw = passwordBox.Text;

                string server = "http://" + IP + ":" + port + "/biprws/logon/long";

                string log = "<attrs xmlns='http://www.sap.com/rws/bip/'><attr name='cms' type='string'>localhost</attr><attr name='userName' type='string'>"+userName+"</attr><attr name='password' type='string'>"+pw+"</attr><attr name='auth' type='string' possibilities='secEnterprise,secLDAP,secWinAD'>secEnterprise</attr></attrs>";
                string loginToken = "";
                Console.WriteLine("Operation: POST - URI: " + server);
                XmlDocument login = new XmlDocument();
                login.LoadXml(log);
                byte[] dataByte = Encoding.Default.GetBytes(login.OuterXml);
                Console.WriteLine(log);
                HttpWebRequest POSTRequest = (HttpWebRequest)WebRequest.Create(server);
                POSTRequest.Method = "POST";
                POSTRequest.ContentType = "application/xml";
                POSTRequest.Timeout = 5000;
                POSTRequest.KeepAlive = false;
                POSTRequest.ContentLength = dataByte.Length;

                Stream POSTstream = POSTRequest.GetRequestStream();
                POSTstream.Write(dataByte, 0, dataByte.Length);

                HttpWebResponse POSTResponse = (HttpWebResponse)POSTRequest.GetResponse();
                StreamReader reader = new StreamReader(POSTResponse.GetResponseStream(), Encoding.UTF8);
                Console.WriteLine("You have successfully logged in!");
                loginToken = reader.ReadToEnd().ToString();
                string loginToken2 = loginToken.Replace("&amp;", "&");

                StringBuilder output = new StringBuilder();
                using (XmlReader xreader = XmlReader.Create(new StringReader(loginToken)))
                {
                    xreader.ReadToFollowing("attr");
                    ltoken = "\"" + xreader.ReadElementContentAsString() + "\"";
                    output.AppendLine("Login Token: " + ltoken);

                }

            Console.WriteLine(output.ToString());
            }
            catch(Exception)
            {
                if (userName == "")
                {
                    Console.WriteLine("Please Enter a valid User Name");
                }
                if (pw == "") 
                {
                    Console.WriteLine("Please Enter a valud Password");
                }
                 
                if (IP == "" || port == "")
                {
                    Console.WriteLine("Please test your server connection");
                }
                Console.WriteLine("Error in communicating with Server");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
        }

        private void txtConsole_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string CUID = sCUID.Text;
                string server = "http://" + IP + ":" + port + "/biprws/raylight/v1/documents/"+CUID+"/schedules/";
                Console.WriteLine("Operation: GET - URI: "+server);
                HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(server);
                GetRequest.Method = "GET";
                GetRequest.Accept = "application/xml";
                GetRequest.Headers.Set("X-SAP-LogonToken", ltoken);
                HttpWebResponse GETResponse = (HttpWebResponse)GetRequest.GetResponse();
                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);
                Console.WriteLine("Response from Server:");
                Console.WriteLine(sr.ReadToEnd());
            }
            catch(Exception)
            {
                Console.WriteLine("Error in communicating with Server");
            
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
        }

        private void button6_Click(object sender, EventArgs e)
        {

            string sName = sNameBox.Text;
            string CUID = sCUID.Text;
            string server = "http://" + IP + ":" + port + "/biprws/raylight/v1/documents/" + CUID + "/schedules/";
            Console.WriteLine("Operation: POST - URI: " + server);
            string freq = "";
            string destination = "";
            string format = "";
            string retries = retriesBox.Text;
            string intervals = intBox.Text;

            sDateBox.Format = DateTimePickerFormat.Custom;
            sDateBox.CustomFormat = "yyyy MM dd hh mm ss";
            string rawsDate = sDateBox.Text;
            string sDate = rawsDate.Substring(0, 4) + "-" + rawsDate.Substring(5, 2) + "-" + rawsDate.Substring(8, 2);
            string sTime = rawsDate.Substring(11, 2) + ":" + rawsDate.Substring(14, 2) + ":" + rawsDate.Substring(17, 2);
            eDateBox.Format = DateTimePickerFormat.Custom;
            eDateBox.CustomFormat = "yyyy MM dd hh mm ss";
            string raweDate = eDateBox.Text;
            string eDate = raweDate.Substring(0, 4) + "-" + raweDate.Substring(5, 2) + "-" + raweDate.Substring(8, 2);
            string eTime = raweDate.Substring(11, 2) + ":" + raweDate.Substring(14, 2) + ":" + raweDate.Substring(17, 2);

            if (inboxRadio.Checked) 
            {
                destination = "inbox";
            }
            if (mailRadio.Checked)
            {
                destination = "mail";
            }
            if (webRadio.Checked)
            {
                format = "webi";
            }
            if (pdfRadio.Checked)
            {
                format = "pdf";
            }
            if (xlsRadio.Checked)
            {
                format = "xls";
            }
            if (csvRadio.Checked)
            {
                format = "csv";
            }
            if (onceRadio.Checked)
            {
                freq = "once";
            }

            string schInfo = "<schedule><name>"+sName+"</name><format type=\"" + format + "\" /><destination><" + destination + "/></destination><" + freq + " retriesAllowed=\"" + retries + "\" retryIntervalInSeconds=\"" + intervals + "\"><startdate>" + sDate + "T" + sTime + ".000+02:00</startdate><enddate>" + eDate + "T" + eTime + ".000+02:00</enddate></" + freq + "></schedule>";
            Console.WriteLine("XML Posted: "+schInfo);

            try
            {
                XmlDocument schXML = new XmlDocument();
                schXML.LoadXml(schInfo);
                byte[] dataByte = Encoding.Default.GetBytes(schXML.OuterXml);
                HttpWebRequest POSTRequest = (HttpWebRequest)WebRequest.Create(server);
                POSTRequest.Method = "POST";
                POSTRequest.ContentType = "application/xml";
                POSTRequest.Headers.Set("X-SAP-LogonToken", ltoken);
                POSTRequest.Timeout = 5000;
                POSTRequest.KeepAlive = false;
                POSTRequest.ContentLength = dataByte.Length;

                Stream POSTstream = POSTRequest.GetRequestStream();
                POSTstream.Write(dataByte, 0, dataByte.Length);

                HttpWebResponse POSTResponse = (HttpWebResponse)POSTRequest.GetResponse();
                StreamReader reader = new StreamReader(POSTResponse.GetResponseStream(), Encoding.UTF8);
                Console.WriteLine("Response from Server:");
                Console.WriteLine(reader.ReadToEnd().ToString());

            }
            
            catch (Exception)
            {
                if (userName == "")
                {
                    Console.WriteLine("Please Enter a valid User Name");
                }
                if (pw == "")
                {
                    Console.WriteLine("Please Enter a valid Password");
                }

                if (IP == "" || port == "")
                {
                    Console.WriteLine("Please test your server connection");
                }
                Console.WriteLine("Error in communicating with Server");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {


        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void Prompt_Click(object sender, EventArgs e)
        {
            try
            {
                string CUID = pCUID.Text;
                string server = "http://" + IP + ":" + port + "/biprws/raylight/v1/documents/"+CUID+"/parameters/";
                Console.WriteLine("Operation: GET - URI: "+server);
                HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(server);
                GetRequest.Method = "GET";
                GetRequest.Accept = "application/xml";
                GetRequest.Headers.Set("X-SAP-LogonToken", ltoken);
                HttpWebResponse GETResponse = (HttpWebResponse)GetRequest.GetResponse();
                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);
                Console.WriteLine("Response from Server:");
                Console.WriteLine(sr.ReadToEnd());
            }
            catch(Exception)
            {
                Console.WriteLine("Error in communicating with Server");

            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
        }



        private void ParRep_Click(object sender, EventArgs e)
        {
            string CUID = pCUID.Text;
            string prompt = ParBox.Text;
            string pvid = pvidBox.Text;
            string server = "http://" + IP + ":" + port + "/biprws/raylight/v1/documents/" + CUID + "/parameters/";
            Console.WriteLine("Operation: PUT - URI: "+server);
            string pContextInfo = "<parameters><parameter type=\"prompt\"><id>"+pvid+"</id><answer type=\"text\"><values><value>"+prompt+"</value></values></answer></parameter></parameters>";
            Console.WriteLine("XML Posted: "+pContextInfo);

            try
            {
                XmlDocument pXML = new XmlDocument();
                pXML.LoadXml(pContextInfo);
                byte[] dataByte = Encoding.Default.GetBytes(pXML.OuterXml);
                HttpWebRequest POSTRequest = (HttpWebRequest)WebRequest.Create(server);
                POSTRequest.Method = "PUT";
                POSTRequest.ContentType = "application/xml";
                POSTRequest.Headers.Set("X-SAP-LogonToken", ltoken);
                POSTRequest.Timeout = 5000;
                POSTRequest.KeepAlive = false;
                POSTRequest.ContentLength = dataByte.Length;

                Stream POSTstream = POSTRequest.GetRequestStream();
                POSTstream.Write(dataByte, 0, dataByte.Length);

                HttpWebResponse POSTResponse = (HttpWebResponse)POSTRequest.GetResponse();
                StreamReader reader = new StreamReader(POSTResponse.GetResponseStream(), Encoding.UTF8);
                Console.WriteLine("Response from Server:");
                Console.WriteLine(reader.ReadToEnd().ToString());
                exportR.PerformClick();
            }
            catch (Exception)
            {
                if (userName == "")
                {
                    Console.WriteLine("Please Enter a valid User Name");
                }
                if (pw == "")
                {
                    Console.WriteLine("Please Enter a valid Password");
                }

                if (IP == "" || port == "")
                {
                    Console.WriteLine("Please test your server connection");
                }
                Console.WriteLine("Error in communicating with Server");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");

        }


        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void pCUID_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void pvidBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void ReportView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
