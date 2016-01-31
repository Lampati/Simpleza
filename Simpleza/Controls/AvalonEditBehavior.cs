using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Simpleza.Controls
{
    public sealed class AvalonEditBehaviour : Behavior<TextEditor>
    {
        public static readonly DependencyProperty CaretOffsetProperty =
            DependencyProperty.Register("CaretOffset", typeof(int), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

        public int CaretOffset
        {
            get { return (int)GetValue(CaretOffsetProperty); }
            set { SetValue(CaretOffsetProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextArea.Caret.PositionChanged += Caret_PositionChanged;
            }
        }

       

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextArea.Caret.PositionChanged -= Caret_PositionChanged;
            }
        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            var caret = sender as Caret;
            if (caret != null)
            {
                CaretOffset = caret.Offset;
               
            }
        }



        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditBehaviour;
            if (behavior.AssociatedObject != null)
            {
                //var editor = behavior.AssociatedObject as TextEditor;
                //if (editor.Document != null)
                //{
                //    var caretOffset = editor.CaretOffset;
                //    editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
                //    editor.CaretOffset = caretOffset;
                //}
            }
        }
    }
}
