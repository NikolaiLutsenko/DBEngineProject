using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Diagnostics
{

    #region Class: ProfilerUtilities

    public class ProfilerUtilities
    {

        public static TimeSpan CheckTime(Action action)
        {
            return CheckTime(action, String.Empty);
        }

        public static TimeSpan CheckTime(Action action, string profileName)
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            action();
            watcher.Stop();
            TimeSpan ts = watcher.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Debug.WriteLine("{0}: {1}",
                String.IsNullOrEmpty(profileName) ? "Runtime" : profileName + " runtime",
                elapsedTime);
            return ts;
        }

        public static void DebugWrite(string message)
        {
            DebugWrite(message, "");
        }

        public static void DebugWrite(string str, params object[] messages)
        {
            string debugMessage = string.Format("{0}{1}{2}{1}{0}",
                        new String('-', 60),
                        Environment.NewLine,
                        String.Format(str, messages));
            Debug.WriteLine(debugMessage);
        }

    }

    #endregion

}
