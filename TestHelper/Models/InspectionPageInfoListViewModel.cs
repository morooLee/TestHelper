using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper.Models
{
    public class InspectionPageInfoList : ObservableCollection<InspectionPageInfo> { }

    public class InspectionPageInfo
    {
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
            }
        }

        public override string ToString()
        {
            return PageName;
        }

        public InspectionPageInfo(string pageName, string url, string inspectionDate)
        {
            this._pageName = pageName;
            this._url = url;
            this._inspectionDate = inspectionDate;
        }
    }
}
