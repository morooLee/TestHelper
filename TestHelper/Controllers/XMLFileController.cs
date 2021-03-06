﻿using System;
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
            SetGNBListXml(gnbNode, Category.Common, "넥슨닷컴 메인", @"http://www.nexon.com/Home/Game.aspx", "65533", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "이벤트", @"http://event.nexon.com/event/ongoinglist.aspx", "65531", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "무료충전소", @"http://freecash.nexon.com/", "65530", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "넥슨캐시", @"https://user.nexon.com/mypage/page/nx.aspx?url=cash/main", "65528", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "내정보", @"https://user.nexon.com/mypage/page/nx.aspx?url=home/index", "", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "보안센터", @"http://security.nexon.com/main/index.aspx", "65527", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "고객센터", @"http://help.nexon.com/", "65526", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "공지사항", @"http://notice.nexon.com/", "65525", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "스쿨존", @"http://schoolzone.nexon.com/page/nx.aspx?URL=Home/Index", "65524", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "사이트맵", @"http://www.nexon.com/sitemap/index.aspx", "65523", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "넥슨플레이", @"http://nexonplay.nexon.com/index.aspx", "65521", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "넥슨 아레나", @"http://arena.nexon.com/", "65520", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "지스타", @"http://gstar.nexon.com/index.html", "65519", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "에러페이지", @"http://bulletin.nexon.com/nxk/error.html", "65516", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "캡챠 로그인", @"https://clogin.nexon.com/common/clogin.aspx", "65515", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "통합 로그인", @"http://nxlogin.nexon.com/common/login.aspx", "65510", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "넥슨별", @"http://nexonplay.nexon.com/starshop/index.aspx", "65509", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "일회용로그인", @"http://nexonplay.nexon.com/disposable_login.aspx", "65508", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "공친소개", @"http://nexonplay.nexon.com/officialfriend/index.aspx", "65507", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "네코제", @"http://necoje.nexon.com/main/index.aspx", "65506", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "www2", @"http://www2.nexon.com/", "65505", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "XP/IE7,8 사용자 보안 취약성 안내 페이지(메인)", @"http://www.nexon.com/Events/vulnerability.html", "65504", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "XP/IE7,8 사용자 보안 취약성 안내 페이지(서브)", @"http://www.nexon.com/services/vulnerability_common.html", "65499", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "개인정보갱신 캠페인 페이지", @"http://gwevent.nexon.com/Campaign/Home ", "65503", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "길드 종료 안내페이지", @"http://bulletin.nexon.com/guild/close.html", "65502", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "넥슨달력", @"http://event.nexon.com/event/CalendarMonth.aspx", "65501", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "네코드", @"", "65500", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Common, "푸르메", @"", "65498", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "탱고파이브 : 더 라스트 댄스", @"", "106556", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "아키에이지", @"", "106548", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "TITANFALL™ ONLINE", @"", "106543", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "아스텔리아", @"", "106547", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "로브레이커즈", @"", "106545", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "천애명월도", @"", "106534", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "드래곤네스트", @"", "106540", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "파이널판타지14", @"", "106533", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "이카루스", @"", "106535", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "마비노기듀얼", @"", "106525", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "테라", @"", "106529", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "NEED FOR SPEED™ EDGE", @"", "106521", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "하이퍼유니버스", @"", "106516", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "최강의군단", @"", "106510", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "트리오브세이비어", @"", "106506", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "아르피엘", @"", "106504", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "메이플스토리2", @"", "106498", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "클로저스", @"", "106497", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "크리티카", @"", "94249", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "테일즈런너", @"", "94248", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "로스트사가", @"", "94247", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "프리스타일2", @"", "94244", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "EA SPORTS™ FIFA ONLINE 3", @"", "94242", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "카운터스트라이크온라인2", @"", "74268", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "바람의나라", @"", "127018", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "카오스온라인", @"", "94239", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "사이퍼즈", @"", "74264", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "마비노기영웅전", @"", "73739", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "버블파이터", @"", "74255", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "엘소드", @"", "94224", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "카운터스트라이크온라인", @"", "73737", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "던전앤파이터", @"", "74257", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "서든어택", @"", "94227", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "마비노기", @"", "74245", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "카트라이더", @"", "73985", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "테일즈위버", @"", "74248", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "메이플스토리", @"", "589824", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "아스가르드", @"", "74274", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "크레이지아케이드", @"", "720896", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "일랜시아", @"", "74276", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "어둠의전설", @"", "74275", null, null, null, null);
            SetGNBListXml(gnbNode, Category.PCOnline, "바람의나라", @"", "131072", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "스페셜솔저", @"", "1000134", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "카이저", @"", "1000132", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "액스", @"", "1000131", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "토탈클래시", @"", "1000130", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "다크어벤저3", @"", "1000129", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "탱크 커맨더즈", @"", "1000128", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "엘소드M 루나의 그림자", @"", "1000127", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "드래곤네스트2 레전드", @"", "1000126", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "진 삼국무쌍: 언리쉬드", @"", "1000125", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "레고® 퀘스트앤콜렉트", @"", "1000124", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "테일즈런너 리볼트", @"", "1000123", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "탱고파이브 : 더 라스트 댄스", @"", "1000122", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "엘소드 슬래시", @"", "1000121", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "던전앤파이터: 혼", @"", "1000120", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "아레나 마스터즈", @"", "1000119", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "메이플블리츠X", @"", "1000117", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "카오스 크로니클", @"", "1000116", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "삼검호2", @"", "1000115", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "건파이 어드벤처", @"", "1000112", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "리터너즈", @"", "1000108", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "M.O.E.", @"", "1000105", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "메달마스터즈", @"", "1000104", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "메이플스토리M", @"", "1000103", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "히트", @"", "1000102", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "도미네이션즈", @"", "1000099", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "삼국지조조전 Online", @"", "1000097", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "천룡팔부", @"", "1000096", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "포켓 메이플스토리", @"", "1000088", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "판타지워택틱스R", @"", "1000086", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "마비노기듀얼", @"", "1000080", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "삼검호", @"", "1000077", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "야생의 땅: 듀랑고", @"", "1000076", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "영웅의 군단", @"", "1000073", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "FIFA Online 3 M by EA SPORTS™", @"", "1000072", null, null, null, null);
            SetGNBListXml(gnbNode, Category.Mobile, "열혈강호M:강호쟁패", @"", "1000133", null, null, null, null);

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
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.Data, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool SetInspectionList(ObservableCollection<InspectionPageInfo> inspectionPageInfoList)
        {
            bool isSaved = false;

            try
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
                isSaved = true;
            }
            catch (Exception e)
            {
                isSaved = false;
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return isSaved;
        }

        public void SetGNBListXml(XmlNode gnbNode, Category _category, string _name, string _url, string _code, bool? _hasGNB, bool? _isPCHub, bool? _isMyBanner, bool? _isCheckedA2S)
        {
            XmlNode gnb = gnbNode.OwnerDocument.CreateElement("GNBPage");

            XmlNode category = gnbNode.OwnerDocument.CreateElement("Category");
            category.InnerText = _category.ToString();
            gnb.AppendChild(category);

            XmlNode name = gnbNode.OwnerDocument.CreateElement("Name");
            name.InnerText = _name;
            gnb.AppendChild(name);

            XmlNode url = gnbNode.OwnerDocument.CreateElement("URL");
            url.InnerText = _url;
            gnb.AppendChild(url);

            XmlNode code = gnbNode.OwnerDocument.CreateElement("Code");
            code.InnerText = _code;
            gnb.AppendChild(code);

            XmlNode hasGnb = gnbNode.OwnerDocument.CreateElement("HasGNB");
            if (_hasGNB == null)
            {
                hasGnb.InnerText = "null";
            }
            else if (_hasGNB == true)
            {
                hasGnb.InnerText = "true";
            }
            else
            {
                hasGnb.InnerText = "false";
            }
            gnb.AppendChild(hasGnb);

            XmlNode isPCHub = gnbNode.OwnerDocument.CreateElement("IsPCHub");
            if (_isPCHub == null)
            {
                isPCHub.InnerText = "null";
            }
            else if (_isPCHub == true)
            {
                isPCHub.InnerText = "true";
            }
            else
            {
                isPCHub.InnerText = "false";
            }
            gnb.AppendChild(isPCHub);

            XmlNode isMyBanner = gnbNode.OwnerDocument.CreateElement("IsMyBanner");
            if (_isMyBanner == null)
            {
                isMyBanner.InnerText = "null";
            }
            else if (_isMyBanner == true)
            {
                isMyBanner.InnerText = "true";
            }
            else
            {
                isMyBanner.InnerText = "false";
            }
            gnb.AppendChild(isMyBanner);

            XmlNode isCheckedA2S = gnbNode.OwnerDocument.CreateElement("IsCheckedA2S");
            if (_isCheckedA2S == null)
            {
                isCheckedA2S.InnerText = "null";
            }
            else if (_isCheckedA2S == true)
            {
                isCheckedA2S.InnerText = "true";
            }
            else
            {
                isCheckedA2S.InnerText = "false";
            }
            gnb.AppendChild(isCheckedA2S);

            gnbNode.AppendChild(gnb);
        }

        public void GetGNBList(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            gnbPageInfoList.Clear();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//GNBList/GNBPage");

                foreach (XmlNode item in xmlNodeList)
                {
                    GNBPageInfo gnbPageInfo = new GNBPageInfo();

                    if(Category.Common.ToString() == item.SelectSingleNode("Category").InnerText)
                    {
                        gnbPageInfo.Category = Category.Common;
                    }
                    else if (Category.PCOnline.ToString() == item.SelectSingleNode("Category").InnerText)
                    {
                        gnbPageInfo.Category = Category.PCOnline;
                    }
                    else if (Category.Mobile.ToString() == item.SelectSingleNode("Category").InnerText)
                    {
                        gnbPageInfo.Category = Category.Mobile;
                    }
                    else
                    {
                        gnbPageInfo.Category = Category.None;
                    }

                    gnbPageInfo.Name = item.SelectSingleNode("Name").InnerText;
                    gnbPageInfo.Url = item.SelectSingleNode("URL").InnerText;
                    gnbPageInfo.Code = item.SelectSingleNode("Code").InnerText;

                    if(item.SelectSingleNode("HasGNB").InnerText == "null")
                    {
                        gnbPageInfo.HasGNB = null;
                    }
                    else
                    {
                        gnbPageInfo.HasGNB = Convert.ToBoolean(item.SelectSingleNode("HasGNB").InnerText);
                    }

                    if (item.SelectSingleNode("IsPCHub").InnerText == "null")
                    {
                        gnbPageInfo.IsPCHub = null;
                    }
                    else
                    {
                        gnbPageInfo.IsPCHub = Convert.ToBoolean(item.SelectSingleNode("IsPCHub").InnerText);
                    }

                    if (item.SelectSingleNode("IsMyBanner").InnerText == "null")
                    {
                        gnbPageInfo.IsMyBanner = null;
                    }
                    else
                    {
                        gnbPageInfo.IsMyBanner = Convert.ToBoolean(item.SelectSingleNode("IsMyBanner").InnerText);
                    }

                    if (item.SelectSingleNode("IsCheckedA2S").InnerText == "null")
                    {
                        gnbPageInfo.IsCheckedA2S = null;
                    }
                    else
                    {
                        gnbPageInfo.IsCheckedA2S = Convert.ToBoolean(item.SelectSingleNode("IsCheckedA2S").InnerText);
                    }

                    gnbPageInfoList.Add(gnbPageInfo);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.Data, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool SetGNBList(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            bool isSaved = false;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNode parentNode = xmlDoc.GetElementsByTagName("GNBList")[0];

                parentNode.RemoveAll();

                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    SetGNBListXml(parentNode, item.Category, item.Name, item.Url, item.Code, item.HasGNB, item.IsPCHub, item.IsMyBanner, item.IsCheckedA2S);
                }

                xmlDoc.Save(path);
                isSaved = true;
            }
            catch (Exception e)
            {
                isSaved = false;
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return isSaved;
        }
    }
}
