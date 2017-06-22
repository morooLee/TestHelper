using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Controls;
using TestHelper;

namespace TestHelper.Controllers
{
    public class WebDriverController
    {
        public void GnbCheck(string url)
        {
            try
            {
                string contentType = "";
                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url));
                //req.Method = "GET";
                ////req.ContentType = "multipart/form-data";
                //////req.Method = "HEAD";
                //HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                //StreamReader streamReader = new StreamReader(res.GetResponseStream(), Encoding.Default);
                //contentType = streamReader.ReadToEnd();
                //streamReader.Close();
                //res.Close();

                //Console.WriteLine(contentType);

                //WebClient webClient = new WebClient();
                //contentType = webClient.DownloadString(url);
                //Stream stream = webClient.OpenRead(url);

                //StreamReader streamReader = new StreamReader(stream);

                //contentType = streamReader.ReadToEnd();
                //stream.Close();
                //stream.Dispose();
                //webClient.Dispose();

                //string[] arr = contentType.Split('\n');
                //foreach (string item in arr)
                //{
                //    if (item.Contains("id=\"InspectionTime\""))
                //    {
                //        Console.WriteLine(item);
                //    }
                //}

                //WebBrowser wb = new WebBrowser();
                //wb.Source = new Uri(url);
                //wb.Navigate(new Uri(url));
                
                //if (wb != null)
                //{
                //    Console.WriteLine(contentType);
                //}
                
                //var doc = (wb.Document as mshtml.HTMLDocument).getElementById("InspectionTime");
                ////var doc = (wb.Document as mshtml.HTMLDocument).getElementById("InspectionTime");
                //Console.WriteLine(doc.innerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
