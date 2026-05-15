namespace PDV.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            global::System.Windows.Forms.Application.Run(new FrmFrenteCaixaPDV());
        }
    }
}
