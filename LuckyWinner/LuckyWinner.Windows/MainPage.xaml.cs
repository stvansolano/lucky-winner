namespace LuckyWinner.Windows
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new LuckyWinner.App());
        }
    }
}