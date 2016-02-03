using Simpleza.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simpleza.Helpers
{
    public class MessageBoxService : IMessageBoxService
    {
        public virtual bool ShowMessageWithOptions(string text, string caption)
        {
            MessageBoxResult res = System.Windows.MessageBox.Show(text, caption, MessageBoxButton.YesNo);

            switch (res)
            {
                case MessageBoxResult.Cancel:
                case MessageBoxResult.No:
                case MessageBoxResult.None:
                    return false;
                case MessageBoxResult.OK:
                case MessageBoxResult.Yes:
                    return true;
                default:
                    return false;
            }
        }

        public virtual void ShowMessage(string text, string caption)
        {
            System.Windows.MessageBox.Show(text, caption, MessageBoxButton.OK);

          
        }
    }
}
