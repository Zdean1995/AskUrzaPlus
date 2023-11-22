namespace AskUrzaPlus
{
    public partial class App : Application
    {
        public static UrzaRepository UrzaRepo { get; private set; }
        public App(UrzaRepository repo)
        {
            InitializeComponent();

            MainPage = new AppShell();

            UrzaRepo = repo;
        }
    }
}
