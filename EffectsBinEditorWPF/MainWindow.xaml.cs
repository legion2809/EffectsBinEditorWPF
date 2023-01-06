namespace EffectsBinEditorWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
    }

    private void CreateFile_Click(object sender, EventArgs e)
    {
        CEffectsParser.CreateFile(EffectsList, Insert, ApplyButton, SaveFile, SaveFileAs, StatusLabel, DeleteButton);
    }

    private void OpenFile_Click(object sender, EventArgs e)
    {
        CEffectsParser.OpenFile(EffectsList, Insert, ApplyButton, SaveFile, SaveFileAs, StatusLabel, DeleteButton);
    }

    private void SaveFile_Click(Object sender, EventArgs e)
    {
        CEffectsParser.SaveFile(StatusLabel);
    }

    private void SaveFileAs_Click(Object sender, EventArgs e)
    {
        CEffectsParser.SaveFileAs(StatusLabel);
    }

    private void InsertEffect_Click(object sender, EventArgs e)
    {
        CEffectsParser.InsertEffect(EffectsList, StatusLabel);
    }

    private void Apply_Click(object sender, EventArgs e)
    {
        CEffectsParser.ApplyProperties(EffectsList, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        CEffectsParser.DeleteEffect(EffectsList);
    }

    private void EffectsList_IndexChanged(object sender, EventArgs e)
    {
        CEffectsParser.VisualProperties(EffectsList, EffectsList.SelectedIndex, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        e.Handled = (e.Key == Key.Space);
        base.OnPreviewKeyDown(e);
    }

    private void FloatOnly(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !FloatOrDoubleCharChecker(Convert.ToChar(e.Text));
        base.OnTextInput(e);
    }

    private void NumericOnly(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IntegerCharChecker(Convert.ToChar(e.Text));
        base.OnTextInput(e);
    }

    private bool FloatOrDoubleCharChecker(char str)

    {
        if (char.IsDigit(str) || str == '-' || str == '.' || str == ',')
            return true;
        else
            return false;
    }

    private bool IntegerCharChecker(char str)

    {
        if (char.IsDigit(str))
            return true;
        else
            return false;
    }

    private void About_Click(object sender, EventArgs e)
    {
        MessageBox.Show($"Effects.bin Editor\nAuthors: Smelson and Legion.\n(С) {DateTime.Now.Year}. From Russia and Kazakhstan with love!", "About Us",
                    MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        Application.Current.MainWindow.Close();
    }
}

