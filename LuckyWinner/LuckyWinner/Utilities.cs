namespace LuckyWinner
{
    using System;
    using Xamarin.Forms;
    using System.Diagnostics;
	using System.Threading.Tasks;

    public static class Utilities
    {
		public static Task RunSafeAsync(this Page page, Action action)
		{
			return Task.Factory.StartNew(() =>RunSafe (page, action));
		}

        public static void RunSafe(this Page page, Action action)
        {
            page.IsBusy = true;

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                page.IsBusy = false;
            }
        }
    }
}