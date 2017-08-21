using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper.Models
{
    public class A2SLogList : ObservableCollection<A2SLog>
    {
        public A2SLogList() : base()
        {

        }
    }

    public class A2SLog : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _action = string.Empty;
        private string _object = string.Empty;
        private string _option = string.Empty;

        public string Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
                this.OnPropertyChanged("Action");
            }
        }

        public string Object
        {
            get
            {
                return _object;
            }
            set
            {
                _object = value;
                this.OnPropertyChanged("Object");
            }
        }

        public string Option
        {
            get
            {
                return _option;
            }
            set
            {
                _option = value;
                this.OnPropertyChanged("Option");
            }
        }

        public A2SLog()
        {

        }

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
