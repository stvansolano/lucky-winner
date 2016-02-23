namespace LuckyWinner.UWP
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
