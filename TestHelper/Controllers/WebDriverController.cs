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
        private bool status = false;
        private int statusCode = -1;
        private string exceptionName;

        public async Task<int> GnbCheck(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            int errorCount = 0;

            foreach (GNBPageInfo item in gnbPageInfoList)
            {
                if (item.IsChecked)
                {
                    statusCode = -1;

                    GNBPageInfo tmp = new GNBPageInfo();
                    tmp = item;

                    try
                    {
                        await GnbCheck(tmp);
                    }
                    catch (Exception e)
                    {
                        status = false;
                        errorCount++;
                        item.HasGNB = null;
                        exceptionName = e.GetType().Name + " : \r\n" + e.Message;
                    }

                    item.Status = status;

                    if (statusCode == -1)
                    {
                        item.StatusReason = exceptionName;
                        //item.Code = null;
                        item.HasGNB = null;
                        item.IsPCHub = null;
                        item.IsMyBanner = null;
                        item.IsCheckedA2S = null;
                    }
                    else
                    {
                        item.StatusReason = statusCode.ToString();
                    }
                }
            }

            return errorCount;
        }

        private async Task<GNBPageInfo> GnbCheck(GNBPageInfo gnbPageInfo)
        {
            try
            {
                HtmlDocument doc = await WebDocumentParser(gnbPageInfo.Url, gnbPageInfo.Name);

                if (doc != null)
                {
                    bool isCheckedA2S = false;
                    HtmlNodeCollection nodeCol = doc.DocumentNode.SelectNodes("//script");

                    if (nodeCol != null)
                    {
                        foreach (HtmlNode node in nodeCol)
                        {
                            if (node.Attributes["src"] != null)
                            {
                                if (node.Attributes["src"].Value.Contains("gnb.min.js") || node.Attributes["src"].Value.Contains("gnb.js"))
                                {
                                    gnbPageInfo.HasGNB = true;

                                    if (node.Attributes["data-gamecode"] != null)
                                    {
                                        if (gnbPageInfo.Code != node.Attributes["data-gamecode"].Value)
                                        {
                                            gnbPageInfo.Code = node.Attributes["data-gamecode"].Value;
                                        }
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
                                else if (node.Attributes["src"].Value.Contains("ngb_head.js") || node.Attributes["src"].Value.Contains("playlog.min.js") || node.Attributes["src"].Value.Contains("playlog.mobile.min.js"))
                                {
                                    isCheckedA2S = true;
                                }
                                else if (node.Attributes["src"].Value.Contains("adjustPath.js"))
                                {
                                    HtmlDocument subDoc = await WebDocumentParser(@"http://help.nexon.com" + node.Attributes["src"].Value, gnbPageInfo.Name);
                                    string[] sprits = subDoc.DocumentNode.InnerHtml.Split(' ');

                                    foreach (string item in sprits)
                                    {
                                        bool? isPCHub = null;
                                        bool? isMyBanner = null;
                                        if (item.Contains("gnb.min.js") || item.Contains("gnb.js"))
                                        {
                                            gnbPageInfo.HasGNB = true;
                                        }
                                        else if (item.Contains("data-gamecode"))
                                        {
                                            if (System.Text.RegularExpressions.Regex.IsMatch(item, @"\D"))
                                            {
                                                if (gnbPageInfo.Code != System.Text.RegularExpressions.Regex.Replace(item, @"\D", ""))
                                                {
                                                    gnbPageInfo.Code = System.Text.RegularExpressions.Regex.Replace(item, @"\D", "");
                                                }
                                            }
                                        }
                                        else if (item.Contains("data-ispchub'"))
                                        {
                                            if (item.Contains("true"))
                                            {
                                                gnbPageInfo.IsPCHub = true;
                                                isPCHub = true;
                                            }
                                            else if (item.Contains("false"))
                                            {
                                                gnbPageInfo.IsPCHub = false;
                                                isPCHub = true;
                                            }
                                        }
                                        else if (item.Contains("data-ismybanner"))
                                        {
                                            if (item.Contains("true"))
                                            {
                                                gnbPageInfo.IsMyBanner = true;
                                                isMyBanner = true;
                                            }
                                            else if (item.Contains("false"))
                                            {
                                                gnbPageInfo.IsMyBanner = false;
                                                isMyBanner = false;
                                            }
                                        }
                                        if (isPCHub == null)
                                        {
                                            gnbPageInfo.IsPCHub = true;
                                        }
                                        if (isMyBanner == null)
                                        {
                                            gnbPageInfo.IsMyBanner = true;
                                        }
                                    }

                                }
                            }
                        }
                        if (gnbPageInfo.HasGNB != true)
                        {
                            //gnbPageInfo.Code = null;
                            gnbPageInfo.HasGNB = false;
                            gnbPageInfo.IsPCHub = false;
                            gnbPageInfo.IsMyBanner = false;
                        }
                        gnbPageInfo.IsCheckedA2S = isCheckedA2S;

                    }
                }
            }
            catch
            {
                throw;
            }

            return gnbPageInfo;
        }

        private async Task<HtmlDocument> WebDocumentParser(string url, string name)
        {
            HtmlDocument doc = new HtmlDocument();

            try
            {
                string content = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));

                request.UserAgent = ".NET Framework Application";
                request.Method = "GET";
                request.ContentType = "text/xml";
                request.Timeout = 1000;

                WebResponse response = await request.GetResponseAsync();

                if (new Uri(url).Host != response.ResponseUri.Host)
                {
                    if (response.ResponseUri.Host == "bulletin.nexon.com")
                    {
                        MessageBox.Show(new Uri(url) + "\r\n페이지가 삭제되었거나 접근할 수 없습니다.", "Error - " + name, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(new Uri(url) + " 에서\r\n" + response.ResponseUri + " 로 이동되었습니다.", "Error - " + name, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    throw new HttpListenerException(1355);
                }

                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    status = true;
                }
                statusCode = HttpStatusCode.OK.GetHashCode();
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                content = streamReader.ReadToEnd();
                doc.LoadHtml(content);


                stream.Close();
                streamReader.Close();
                response.Close();
            }
            catch
            {
                throw;
                //status = false;
                //errorCount++;
                //exceptionName = e.GetType().Name + " : \r\n" + e.Message;
            }

            return doc;
        }
    }
}
