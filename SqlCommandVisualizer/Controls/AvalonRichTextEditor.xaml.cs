using System;
using System.Windows;
using System.Windows.Controls;

namespace SqlCommandVisualizer.Controls
{
    public partial class AvalonRichTextEditor : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(AvalonRichTextEditor), new PropertyMetadata(default(string), OnAvalonTextEditor));

        private static void OnAvalonTextEditor(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var textEditor = (AvalonRichTextEditor)dependencyObject;
            if (textEditor._updateTextFromControl == false)
            {
                textEditor.AvalonTextEditor.Text = (string)eventArgs.NewValue;
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private bool _updateTextFromControl;

        public AvalonRichTextEditor()
        {
            InitializeComponent();
            AvalonTextEditor.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            _updateTextFromControl = true;
            Text = AvalonTextEditor.Text;
            _updateTextFromControl = false;
        }
    }
}
