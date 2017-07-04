using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper.Models
{
    public class GNBPageInfoList : ObservableCollection<GNBPageInfo>
    {
        public GNBPageInfoList() : base()
        {
            //Add(new InspectionPageInfo("Nexon_PC", @"http://bulletin.nexon.com/nxk/service_checking.html", string.Empty));
            //Add(new InspectionPageInfo("Nexon_Mobile", @"http://bulletin.nexon.com/nxk/service_checking_mobile.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Nexon", @"http://bulletin.nexon.com/eventmgr/inspection.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Daum", @"http://gamebulletin.nexon.game.daum.net/eventmgr/inspection.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Naver", @"http://bulletin.nexon.game.naver.com/eventmgr/inspection.html", string.Empty));
            //Add(new InspectionPageInfo("GWMS_Tooniland", @"http://nxbulletin.tooniland.com/eventmgr/inspection.html", string.Empty));
        }
    }

    public class GNBPageInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Category _category = Category.None;
        private string _name = string.Empty;
        private string _url = string.Empty;
        private string _code = string.Empty;
        private bool _ispchub = false;
        private bool _ismybanner = false;
        private bool _isCheckedA2S = false;

        public Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                this.OnPropertyChanged("Category");
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                this.OnPropertyChanged("Name");
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

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                this.OnPropertyChanged("Code");
            }
        }

        public bool IsPCHub
        {
            get
            {
                return _ispchub;
            }
            set
            {
                _ispchub = value;
                this.OnPropertyChanged("IsPCHub");
            }
        }

        public bool IsMyBanner
        {
            get
            {
                return _ismybanner;
            }
            set
            {
                _ismybanner = value;
                this.OnPropertyChanged("IsMyBanner");
            }
        }

        public bool IsCheckedA2S
        {
            get
            {
                return _isCheckedA2S;
            }
            set
            {
                _isCheckedA2S = value;
                this.OnPropertyChanged("IsCheckedA2S");
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public GNBPageInfo()
        {

        }

        public GNBPageInfo(Category category, string name, string url, string code)
        {
            this._category = category;
            this._name = name;
            this._url = url;
            this._code = code;
        }

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public enum Category
    {
        None = -1,
        Common = 0,
        PCOnline = 1,
        Mobile = 2
    }
}
