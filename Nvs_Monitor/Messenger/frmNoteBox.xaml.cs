using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Nvs_Monitor
{
    /// <summary>
    /// Interaction logic for NoteBox.xaml
    /// </summary>
    public partial class frmNoteBox : Window
    {
        public frmNoteBox()
        {
            InitializeComponent();
        }

        public string Message;
        public string c_caption;
        public NoteBoxLevel c_level;

        public NoteBoxLevel level
        {
            set
            {
                c_level = value;

                if (c_level == NoteBoxLevel.Question)
                {
                    Style style = this.FindResource("QuestionStyle") as Style;
                    frm.Style = style;

                }
                else if (c_level == NoteBoxLevel.Error)
                {
                    Style style = this.FindResource("ErrorStyle") as Style;
                    frm.Style = style;
                }
                else
                {
                    Style style = this.FindResource("NoticeStyle") as Style;                  
                    frm.Style = style;
                }
            }
        }

        ControlShowButton c_ControlShowButton = new ControlShowButton();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //set focus cho nut
                object _objbtnOK;
                object _objbtnNo;
                _objbtnNo = this.Template.FindName("btnNo", this);
                _objbtnOK = this.Template.FindName("btnOK", this);

                if (c_level == NoteBoxLevel.Question)
                {
                    _objbtnNo = this.Template.FindName("btnNo", this);
                }
                else if (c_level == NoteBoxLevel.Error)
                {
                    _objbtnOK = this.Template.FindName("btnOK", this);
                }
                else
                {
                    _objbtnOK = this.Template.FindName("btnOK", this);
                }

                //
                this.DataContext = c_ControlShowButton;

                if (c_level == NoteBoxLevel.Question)
                {
                    c_ControlShowButton.Show2Button = Visibility.Visible;
                    c_ControlShowButton.Show1Button = Visibility.Collapsed;
                    // 
                    if (_objbtnNo != null)
                        ((Button)_objbtnNo).Focus();
                }
                else
                {
                    c_ControlShowButton.Show2Button = Visibility.Collapsed;
                    c_ControlShowButton.Show1Button = Visibility.Visible;

                    if (_objbtnOK != null)
                        ((Button)_objbtnOK).Focus();
                }
                //
                c_ControlShowButton.MsgContent = Message;
                this.Title = c_caption;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                    this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class NoteBox
    {
        public static MessageBoxResult Show(string messageBoxText)
        {
            try
            {
                frmNoteBox _frmNoteBox = new frmNoteBox();
                //
                _frmNoteBox.level = NoteBoxLevel.Note;

                _frmNoteBox.c_caption = "Thông báo";
                _frmNoteBox.Message = messageBoxText;
                //

                _frmNoteBox.Owner = Application.Current.MainWindow;
                _frmNoteBox.ShowDialog();
                return MessageBoxResult.OK;
            }
            catch
            {
                return NoteBox.Show(messageBoxText, "Thông báo");
            }
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            try
            {
                frmNoteBox _frmNoteBox = new frmNoteBox();
                //
                _frmNoteBox.level = NoteBoxLevel.Note;
                _frmNoteBox.c_caption = caption;
                _frmNoteBox.Message = messageBoxText;
                //
                _frmNoteBox.Owner = Application.Current.MainWindow;
                _frmNoteBox.ShowDialog();

                return MessageBoxResult.OK;
            }
            catch
            {
                return NoteBox.Show(messageBoxText, caption);
            }

        }

        public static MessageBoxResult Show(string messageBoxText, string caption, NoteBoxLevel p_level)
        {
            try
            {
                frmNoteBox _frmNoteBox = new frmNoteBox();
                //
                _frmNoteBox.level = p_level;
                _frmNoteBox.c_caption = caption;
                _frmNoteBox.Title = caption;
                _frmNoteBox.Message = messageBoxText;
                //
                bool? _result;
                _frmNoteBox.Owner = Application.Current.MainWindow;
                _result = _frmNoteBox.ShowDialog();
                //
                if (p_level == NoteBoxLevel.Question)
                //|| p_level == NoteBoxLevel.Warning)
                {
                    if (_result == true)
                        return MessageBoxResult.Yes;
                    else
                        return MessageBoxResult.No;
                }
                else
                {
                    return MessageBoxResult.OK;
                }
            }
            catch
            {
                return MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo);
            }
        }
    }


    public enum NoteBoxLevel
    {
        /// <summary>
        /// Thông báo Bình thường, chỉ có nút OK
        /// </summary>
        Note = 0,

        /// <summary>
        /// Thông báo lỗi, chỉ có nút ok
        /// </summary>
        Error = 1,

        /// <summary>
        /// Confirm 1 vấn đề nào đó, có nút Yes/No
        /// </summary>
        Question = 2,


        /// <summary>
        /// Cảnh báo nguy hiểm,có nút Yes/No
        /// </summary>
        //Warning = 3
        //

    }

    public class ControlShowButton : INotifyPropertyChanged
    {
        private Visibility _Show2Button;
        private Visibility _Show1Button;
        private string _MsgContent;
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;


        public Visibility Show2Button
        {
            get { return _Show2Button; }
            set
            {
                _Show2Button = value;
                OnPropertyChanged("Show2Button");
            }
        }

        public Visibility Show1Button
        {
            get { return _Show1Button; }
            set
            {
                _Show1Button = value;
                OnPropertyChanged("Show1Button");
            }
        }

        public string MsgContent
        {
            get { return _MsgContent; }
            set
            {
                _MsgContent = value;
                OnPropertyChanged("MsgContent");
            }
        }


        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
