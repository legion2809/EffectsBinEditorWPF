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
        CEffectsParser.CreateFile(EffectsList, Insert, ApplyButton, SaveFile, SaveFileAs);
    }

    private void OpenFile_Click(object sender, EventArgs e)
    {
        CEffectsParser.OpenFile(EffectsList, Insert, ApplyButton, SaveFile, SaveFileAs);
    }

    private void SaveFile_Click(Object sender, EventArgs e)
    {
        CEffectsParser.SaveFile();
    }

    private void SaveFileAs_Click(Object sender, EventArgs e)
    {
        CEffectsParser.SaveFileAs();
    }

    private void InsertEffect_Click(object sender, EventArgs e)
    {
        CEffectsParser.InsertEffect(EffectsList);
    }

    private void EffectsList_IndexChanged(object sender, EventArgs e)
    {
        CEffectsParser.VisualProperties(EffectsList, EffectsList.SelectedIndex, XCoordTextBox, YCoordTextBox, ZCoordTextBox, EffectIDTextBox);
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

    private void SaveFile_Click_1(object sender, RoutedEventArgs e)
    {

    }
}

