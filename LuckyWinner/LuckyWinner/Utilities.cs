namespace LuckyWinner
{
    using System;
    using Xamarin.Forms;
    using System.Diagnostics;

    public static class Utilities
    {
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