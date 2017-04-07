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
        public string Password { get; set; }

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

        string _display;
        [DataMember]
        public string Display
        {
            get
            {
                return _display;
            }
            set
            {
                _display = value;
            }
        }

        public List<Member_Info> List_Member { get; set; }

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

        int _IsGroup;
        [DataMember]
        public int IsGroup
        {
            get { return _IsGroup; }
            set
            {
                if (_IsGroup != value)
                {
                    _IsGroup = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsGroup"));
                    }
                }
            }
        }

       
        public event PropertyChangedEventHandler PropertyChanged;
    }

    [DataContract]
    public class Member_Info
    {
        [DataMember]
        public string Member_Name { get; set; }
    }

    [DataContract]
    public class Group_User_Info
    {
        [DataMember]
        public decimal Group_Id { get; set; }

        [DataMember]
        public decimal User_Id { get; set; }

        [DataMember]
        public DateTime Joindate { get; set; }
    }

    [DataContract]
    public class GroupsInfo
    {
        [DataMember]
        public decimal Group_Id { get; set; }

        [DataMember]
        public string Group_Name { get; set; }

        [DataMember]
        public DateTime Createdate { get; set; }

        [DataMember]
        public string Notes { get; set; }
    }

    [DataContract]
    public class User_Friends_Info
    {
        [DataMember]
        public decimal User_Id { get; set; }

        [DataMember]
        public decimal User_Friend_Id { get; set; }
    }

}
