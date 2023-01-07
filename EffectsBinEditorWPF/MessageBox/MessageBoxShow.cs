namespace EffectsBinEditorWPF;
public class CMessageBoxShow
{
    public static void MessageShow(Window mainWindow, string Header, string Text, byte a, byte r, byte g, byte b)
    {
        Message messagebox = new Message();
        messagebox.Owner = mainWindow;
        messagebox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        messagebox.SetFields(Header, Text, a, r, g, b);
        messagebox.ShowDialog();
        messagebox.Close();
    }
}
