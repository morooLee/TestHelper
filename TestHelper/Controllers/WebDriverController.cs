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
using System.Xml.XPath;
using System.Web;
using HtmlAgilityPack;

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

            try
            {
                HtmlDocument doc = WebDocumentParser(url);
                HtmlNodeCollection nodeCol = doc.DocumentNode.SelectNodes("//script");

                foreach (HtmlNode node in nodeCol)
                {
                    if (node.Attributes["src"] != null)
                    {
                        if (node.Attributes["src"].Value.Contains("gnb.min.js") || node.Attributes["src"].Value.Contains("gnb.js"))
                        {
                            if (node.Attributes["data-gamecode"] != null)
                            {
                                gnbPageInfo.Code = node.Attributes["data-gamecode"].Value;
                            }
                            if (node.Attributes["data-ispchub"] != null)
                            {
                                if (node.Attributes["data-ispchub"].Value == "true")
                                {
                                    gnbPageInfo.IsPCHub = true;
                                }
                                else if ((node.Attributes["data-ispchub"].Value == "false"))
                                {
                                    gnbPageInfo.IsPCHub = false;
                                }
                            }
                            else
                            {
                                gnbPageInfo.IsPCHub = true;
                            }
                            if (node.Attributes["data-ismybanner"] != null)
                            {
                                if (node.Attributes["data-ismybanner"].Value == "true")
                                {
                                    gnbPageInfo.IsMyBanner = true;
                                }
                                else if ((node.Attributes["data-ismybanner"].Value == "false"))
                                {
                                    gnbPageInfo.IsMyBanner = false;
                                }
                            }
                            else
                            {
                                gnbPageInfo.IsMyBanner = true;
                            }
                        }
                        else if (node.Attributes["src"].Value.Contains("ngb_head.js"))
                        {
                            gnbPageInfo.IsCheckedA2S = true;
                        }
                        else if (node.Attributes["src"].Value.Contains("gnb_ext.js"))
                        {
                            gnbPageInfo.IsCheckedA2S = true;
                        }
                    }
                    Console.WriteLine(gnbPageInfo.Url);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.Data, "Error - GnbCheck", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return gnbPageInfo;
        }

        private HtmlDocument WebDocumentParser(string url)
        {
            HtmlDocument doc = new HtmlDocument();

            try
            {
                string content = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));

                request.UserAgent = ".NET Framework Application";
                request.Method = "GET";
                request.ContentType = "text/xml";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                content = streamReader.ReadToEnd();

                doc.LoadHtml(content);


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
