using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows;

namespace ObjInfo
{
    [DataContract]
    public class User_Info : INotifyPropertyChanged
    {
        [DataMember]
        public string User_Name { get; set; }

        [DataMember]
        public string Pass { get; set; }

        [DataMember]
        public string Ip { get; set; }

        int _Online_Status;
        [DataMember]
        public int Online_Status
        {
            get { return _Online_Status; }
            set
            {
                if (_Online_Status != value)
                {
                    _Online_Status = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Online_Status"));
                    }
                }
            }
        }

        string _Status;
        [DataMember]
        public string Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                    }
                }
            }
        }

        Visibility _showimage_onilne = Visibility.Hidden;
        public Visibility ShowOnline
        {
            get
            {
                return _showimage_onilne;
            }
            set
            {
                if (_showimage_onilne != value)
                {
                    _showimage_onilne = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ShowOnline"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
