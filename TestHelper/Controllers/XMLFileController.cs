using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Windows;
using TestHelper.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestHelper.Controllers
{
    public class XMLFileController
    {
        string path = Directory.GetCurrentDirectory() + @"\Setting.xml";

        public void FileCheck()
        {
            try
            {
                if (!File.Exists(path))
                {
                    XMLCreate();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void XMLCreate()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            XmlNode root = xmlDoc.CreateElement("Settings");
            xmlDoc.AppendChild(root);

            XmlNode inspectionNode = xmlDoc.CreateElement("Inspections");
            SetInspectionXml(inspectionNode, "Nexon_PC", @"http://bulletin.nexon.com/nxk/service_checking.html");
            SetInspectionXml(inspectionNode, "Nexon_Mobile", @"http://bulletin.nexon.com/nxk/service_checking_mobile.html");
            SetInspectionXml(inspectionNode, "GWMS_Nexon", @"http://bulletin.nexon.com/eventmgr/inspection.html");
            SetInspectionXml(inspectionNode, "GWMS_Daum", @"http://gamebulletin.nexon.game.daum.net/eventmgr/inspection.html");
            SetInspectionXml(inspectionNode, "GWMS_Naver", @"http://bulletin.nexon.game.naver.com/eventmgr/inspection.html");
            SetInspectionXml(inspectionNode, "GWMS_Tooniland", @"http://nxbulletin.tooniland.com/eventmgr/inspection.html");

            root.AppendChild(inspectionNode);

            XmlNode gnbNode = xmlDoc.CreateElement("GNBList");
            root.AppendChild(gnbNode);

            xmlDoc.Save(path);
        }

        public void SetInspectionXml(XmlNode _xmlNode, string _name, string _url)
        {
            XmlNode inspection = _xmlNode.OwnerDocument.CreateElement("Inspection");
            XmlNode name = _xmlNode.OwnerDocument.CreateElement("Name");
            name.InnerText = _name;
            inspection.AppendChild(name);
            XmlNode url = _xmlNode.OwnerDocument.CreateElement("URL");
            url.InnerText = _url;
            inspection.AppendChild(url);
            _xmlNode.AppendChild(inspection);
        }

        public void GetInspectionList(ObservableCollection<InspectionPageInfo> inspectionPageInfoList)
        {
            WebDriverController webDriverController = new WebDriverController();
            ObservableCollection<InspectionPageInfo> tmp = new ObservableCollection<InspectionPageInfo>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//Inspections/Inspection");

                foreach (XmlNode item in xmlNodeList)
                {
                    InspectionPageInfo inspectionPageInfo = new InspectionPageInfo();
                    inspectionPageInfo.PageName = item.SelectSingleNode("Name").InnerText;
                    inspectionPageInfo.Url = item.SelectSingleNode("URL").InnerText;

                    inspectionPageInfoList.Add(inspectionPageInfo);

                    webDriverController.GnbCheck(item.SelectSingleNode("URL").InnerText);
                    //var doc = (webBrowser.Document as mshtml.HTMLDocument).getElementById("InspectionTime");
                    //inspectionPageInfo.InspectionDate = doc.toString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.Data, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SetInspectionList(ObservableCollection<InspectionPageInfo> inspectionPageInfoList)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNode parentNode = xmlDoc.GetElementsByTagName("Inspections")[0];

            parentNode.RemoveAll();

            foreach (InspectionPageInfo item in inspectionPageInfoList)
            {
                SetInspectionXml(parentNode, item.PageName, item.Url);
            }

            xmlDoc.Save(path);
        }
    }
}
