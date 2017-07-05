using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Controls;
using TestHelper;
using System.Collections.ObjectModel;
using TestHelper.Models;
using System.Windows;
using mshtml;

namespace TestHelper.Controllers
{
    public class WebDriverController
    {
        public void GnbCheck(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            foreach (GNBPageInfo item in gnbPageInfoList)
            {
                if (item.IsChecked)
                {
                    GNBPageInfo tmp = new GNBPageInfo();
                    tmp = item;
                    GnbCheck(tmp);
                }
            }
        }

        private GNBPageInfo GnbCheck(GNBPageInfo gnbPageInfo)
        {
            string url = gnbPageInfo.Url;
            HTMLDocument doc = WebDocumentParser(url);

            try
            {
                IHTMLTable table = (IHTMLTable)doc.getElementById("stript");
                IHTMLElementCollection co = table.rows;
                Console.WriteLine(co.length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error - GnbCheck", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return gnbPageInfo;
        }

        private HTMLDocument WebDocumentParser(string url)
        {
            HTMLDocument doc = new HTMLDocument();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                string content = streamReader.ReadToEnd();

                doc.designMode = "on";
                object[] oPageText = { content };
                IHTMLDocument2 oMyDoc = doc as IHTMLDocument2;
                oMyDoc.write(oPageText);

                stream.Close();
                streamReader.Close();
                response.Close();
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message + e.Data, "Error - WebEcveption", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.Data, "Error - WebDocumentParser", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return doc;
        }
    }
}
