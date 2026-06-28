namespace SistemaComercial
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Data.Database.CriarTabela();
            Application.Run(new FormSplash());
        }
    }
}
