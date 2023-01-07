namespace EffectsBinEditorWPF;

public partial class Message : Window
{
    public Message()
    {
        InitializeComponent();
    }

    public void SetFields(string TextHeader, string Text, byte a, byte r, byte g, byte b)
    {
        Title.Content = TextHeader;
        Description.Content = Text;
        this.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}
