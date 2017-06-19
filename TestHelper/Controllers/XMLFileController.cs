using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Specialized;

namespace TestHelper.Controllers
{
    public class XMLFileController
    {
        private void XMLCreate()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "urf-8", "yesy"));

            xmlDoc.Save("setting.xml");
        }
    }
}
