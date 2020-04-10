﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPerf.Controls
{
    internal static class PaintSuspenderExtension
    {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        private const int WmSetRedraw = 11;

        public static IDisposable SuspendPainting(this Control ctrl)
        {
            return new PaintSuspender(ctrl);
        }

        private class PaintSuspender : IDisposable
        {
            private readonly Control _ctrl;

            public PaintSuspender(Control ctrl)
            {
                _ctrl = ctrl;
                SendMessage(_ctrl.Handle, WmSetRedraw, false, 0);
            }

            public void Dispose()
            {
                SendMessage(_ctrl.Handle, WmSetRedraw, true, 0);
                _ctrl.Refresh();
            }
        }
    }

}