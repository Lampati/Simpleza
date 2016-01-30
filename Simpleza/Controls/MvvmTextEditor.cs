using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simpleza.Controls
{
    public class MvvmTextEditor : TextEditor, INotifyPropertyChanged
    {
        public static DependencyProperty CaretOffsetProperty =
             DependencyProperty.Register("CaretOffset", typeof(int), typeof(MvvmTextEditor)
            // binding changed callback: set value of underlying property
        );


        public new int CaretOffset
        {
            get { return base.CaretOffset; }
            set { base.CaretOffset = value; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            RaisePropertyChanged("CaretOffset");
            base.OnTextChanged(e);
            
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
