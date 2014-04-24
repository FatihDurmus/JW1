using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Net;

namespace jw1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        WebBrowser mywebBrowser;
        string url;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            string file="";
            string text = "";
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
                text = File.ReadAllText(file);
                size = text.Length;
            }
            StreamReader reader = new StreamReader(file);
            while ((text = reader.ReadLine())!=null)
            {
                MessageBox.Show(text.ToString());
                var something = DetectWordpress(text);
                

            }
        }
        protected string DetectJoomla(string url)
        {
            string system="";
            HtmlWeb client = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = client.Load(url);
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//a[@href]");
            int dongu = 0;
            foreach (var link in Nodes)
            {
                if (link.Attributes["href"].Value.Contains("joomla") && dongu < 1)
                {
                    dongu += 1;
                    listView1.Items.Add(url + " joomla bir site");
                }
                
            }
            return system;
        }
        protected string DetectWordpress(string url)
        {
            string system="bos";
            string html;
            using (WebClient client = new WebClient())
            {
                html = client.DownloadString(url);
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            foreach (HtmlNode img in doc.DocumentNode.SelectNodes("//img"))
            {
                if (img.OuterHtml.Contains("wp-content"))
                {
                    listView1.Items.Add(url+" Wordpress bir site");
                }
                else
                {
                    var anotherthing = DetectJoomla(url);

                }
            }


            return system;
            }

       
        }
       
        }

