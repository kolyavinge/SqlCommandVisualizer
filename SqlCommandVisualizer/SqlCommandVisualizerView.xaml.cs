using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Reflection;
using System.Windows;
using System.Xml;

namespace SqlCommandVisualizer
{
    public partial class SqlCommandVisualizerView
    {
        public SqlCommandVisualizerView()
        {
            InitializeComponent();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SqlCommandVisualizer.Resources.sql.xshd"))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
        }
    }
}
