using System;
using System.Windows.Forms;

namespace Caffeinated;

// Hidden window to receive Windows messages for shutdown handling
internal class MessageWindow : Form {
        private readonly AppContext appContext;

        public MessageWindow(AppContext context) {
            appContext = context;
            // Create hidden window
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            Opacity = 0;
            Width = 0;
            Height = 0;
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case 0x0011: // WM_QUERYENDSESSION
                    // Check if this is from Restart Manager
                    if (m.LParam.ToInt32() == 0x1) { // ENDSESSION_CLOSEAPP
                        // Return TRUE - we're ready to shutdown
                        m.Result = new IntPtr(1);
                        return;
                    }
                    break;

                case 0x0016: // WM_ENDSESSION
                    // Check if this is from Restart Manager and we should actually close
                    if (m.LParam.ToInt32() == 0x1 && // ENDSESSION_CLOSEAPP
                        m.WParam.ToInt32() != 0) {
                        // Perform quick shutdown
                        appContext.PerformGracefulShutdown();
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }
    }
