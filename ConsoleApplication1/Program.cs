using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Quake {
    class Program {
        public const int ConsoleErrorTimeout = 60;
        public const int PauseSleep = 50;
        public const int NotFocusSleep = 20;

        
        public static bool IsDedicated { get; set; }
        private static bool ScReturnOnEnter { get; set; }



        static void Main(string[] args) {
            try {
                var cwd = Directory.GetCurrentDirectory();
            } catch (Exception ex) {
                Error("Couldn't determine current directory");
            }
        }


        private static bool __sysError0;
        private static bool __sysError1;
        private static bool __sysError2;
        private static bool __sysError3;
        private static void Error(string error, params object[] args) {

            if (!__sysError3) {
                __sysError3 = true;
                Video.ForceUnlockedAndReturnState();
            }

            var text = String.Format(error, args);

            if (IsDedicated) {
                Console.WriteLine();
                Console.WriteLine("***********************************");
                Console.WriteLine("ERROR: " + text);
                Console.WriteLine("Press Enter to exit");
                Console.WriteLine("***********************************");

                var startTime = Stopwatch.GetTimestamp();
                ScReturnOnEnter = true;

                while (string.IsNullOrEmpty(ConsoleInput()) && ((Stopwatch.GetTimestamp() - startTime)/Stopwatch.Frequency) < ConsoleErrorTimeout) {
                    // wait
                }
            } else {
                if (!__sysError0) {
                    __sysError0 = true;
                    Video.SetDefaultMode();
                    MessageBox.Show(text, "Quake Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                } else {
                    MessageBox.Show(text, "Double Quake Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            if (!__sysError1) {
                __sysError1 = true;
                Host.Shutdown();
            }
            if (!__sysError2) {
                __sysError2 = true;
                QHost.DeinitConProc();
            }

            Environment.Exit(1);
        }

        private static int __len = 0;
        private static char[] __text = new char[256];
        private static string ConsoleInput() {
            if (!IsDedicated) {
                return null;
            }
            for (;;) {
                if (!Console.KeyAvailable) {
                    break;
                }

                var key = new ConsoleKeyInfo();
                try {
                    key = Console.ReadKey(true);
                } catch (Exception ex) {
                    Error("Couldn't read console input");
                }

                var ch = key.KeyChar;
                switch (ch) {
                    case '\r':
                        Console.Write("\r\n");
                        if (__len > 0) {
                            __text[__len] = (char) 0;
                            __len = 0;
                            return __text.ToString();
                        } else if (ScReturnOnEnter) {
                            __text[0] = '\r';
                            __len = 0;
                            return __text.ToString();
                        }
                        break;
                    case '\b':
                        Console.Write("\b \b");
                        if (__len > 0) {
                            __len--;
                        }
                        break;
                    default:
                        if (ch >= ' ') {
                            Console.Write(ch);
                            __text[__len] = ch;
                            __len = (__len + 1) & 0xff;
                        }
                        break;
                }

            }
            return null;
        }
    }
}
