using GalaSoft.MvvmLight.Messaging;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
using Simpleza.Resources;
using Simpleza.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Simpleza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);

            SearchPanel.Install(textEditor);
            

             IHighlightingDefinition customHighlighting;
             using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Simpleza.Resources.GarGar.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource");
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            // and register it in the HighlightingManager
            HighlightingManager.Instance.RegisterHighlighting("GarGar", new string[] { ".gar" }, customHighlighting);

            textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("GarGar");
            
            
            
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "ShowAboutUs")
            {
                AboutUsWindow acercaDeWindow = new AboutUsWindow();
                acercaDeWindow.Owner = this.Owner;

                acercaDeWindow.ShowDialog();
            }
        }
    }
}
