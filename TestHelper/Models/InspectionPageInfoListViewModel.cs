using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TestHelper.Models
{
    public class InspectionPageInfoList : ObservableCollection<InspectionPageInfo>
    {
        public InspectionPageInfoList() : base()
        {
            //Add(new InspectionPageInfo("Nexon_PC", @"http://bulletin.nexon.com/nxk/service_checking.html", string.Empty));
            //Add(new InspectionPageInfo("Nexon_Mobile", @"http://bulletin.nexon.com/nxk/service_checking_mobile.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Nexon", @"http://bulletin.nexon.com/eventmgr/inspection.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Daum", @"http://gamebulletin.nexon.game.daum.net/eventmgr/inspection.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Naver", @"http://bulletin.nexon.game.naver.com/eventmgr/inspection.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Tooniland", @"http://nxbulletin.tooniland.com/eventmgr/inspection.html", string.Empty));
        }
    }

    public class InspectionPageInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _pageName = string.Empty;
        private string _url = string.Empty;
        private string _inspectionDate = string.Empty;

        public string PageName
        {
            get
            {
                return _pageName;
            }
            set
            {
                _pageName = value;
                this.OnPropertyChanged("PageName");
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                this.OnPropertyChanged("Url");
            }
        }

        public string InspectionDate
        {
            get
            {
                return _inspectionDate;
            }
            set
            {
                _inspectionDate = value;
                this.OnPropertyChanged("InspectionDate");
            }
        }

        public override string ToString()
        {
            return PageName;
        }

        public InspectionPageInfo()
        {

        }

        public InspectionPageInfo(string pageName, string url, string inspectionDate)
        {
            this._pageName = pageName;
            this._url = url;
            this._inspectionDate = inspectionDate;
        }

        private void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
