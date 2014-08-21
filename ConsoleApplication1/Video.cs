using System.Windows.Forms;

namespace Quake {
    
#define GL

#if GL
    public class Video {
        public static int ForceUnlockedAndReturnState() {
            return 0;
        }

        public static void SetDefaultMode() {
            Input.DeactivateMouse();
        }
    }

#endif

#if SOFTWARE
    public class Video {
        public static int ForceUnlockedAndReturnState() {
            return 0;
        }
        public static void SetDefaultMode() {
            Input.DeactivateMouse();
        }
    }
#endif
}