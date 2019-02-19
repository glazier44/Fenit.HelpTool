using System.Threading;

namespace Fenit.Toolbox.Core.Utils
{
    public static class Clipboard
    {
        public static void SetText(string text)
        {
            var thread = new Thread(() => SetText(text));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
        }
        //public static string GetText()
        //{
        //    string ReturnValue = string.Empty;
        //    Thread STAThread = new Thread(
        //        delegate ()
        //        {
        //            // Use a fully qualified name for Clipboard otherwise it
        //            // will end up calling itself.
        //            ReturnValue = System.Windows.Forms.Clipboard.GetText();
        //        });
        //    STAThread.SetApartmentState(ApartmentState.STA);
        //    STAThread.Start();
        //    STAThread.Join();

        //    return ReturnValue;
        //}
    }
}