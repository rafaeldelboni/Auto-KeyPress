using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Auto_KeyPress
{
    //Class used to call user32 dll and simulate an user click on the selected process
    class UserInterface
    {
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
    }
}
