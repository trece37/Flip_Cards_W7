using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Media;

namespace Flip_Cards_W7
{
    public class WindowManager
    {
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        
        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);
        
        private const int DWMWA_CLOAKED = 14;
        private const int SW_RESTORE = 9;
        
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        
        public List<WindowInfo> GetOpenWindows()
        {
            var windows = new List<WindowInfo>();
            
            EnumWindows((hWnd, lParam) =>
            {
                if (IsValidWindow(hWnd))
                {
                    int length = GetWindowTextLength(hWnd);
                    if (length > 0)
                    {
                        StringBuilder sb = new StringBuilder(length + 1);
                        GetWindowText(hWnd, sb, sb.Capacity);
                        
                        string title = sb.ToString();
                        if (!string.IsNullOrWhiteSpace(title) && 
                            title != "Flip_Cards-W7" && 
                            !title.Contains("MSCTFIME"))
                        {
                            var windowInfo = new WindowInfo
                            {
                                Handle = hWnd,
                                Title = title,
                                Thumbnail = CaptureWindow(hWnd)
                            };
                            windows.Add(windowInfo);
                        }
                    }
                }
                return true;
            }, IntPtr.Zero);
            
            return windows;
        }
        
        private bool IsValidWindow(IntPtr hWnd)
        {
            if (!IsWindowVisible(hWnd))
                return false;
            
            // Check if window is cloaked (hidden on other virtual desktop)
            if (DwmGetWindowAttribute(hWnd, DWMWA_CLOAKED, out bool isCloaked, sizeof(int)) == 0)
            {
                if (isCloaked)
                    return false;
            }
            
            return true;
        }
        
        private BitmapSource CaptureWindow(IntPtr hWnd)
        {
            try
            {
                // Simplified capture - returns null for fallback rendering
                // In production, use PrintWindow or DWM Thumbnail API
                return null;
            }
            catch
            {
                return null;
            }
        }
        
        public void ActivateWindow(WindowInfo window)
        {
            ShowWindow(window.Handle, SW_RESTORE);
            SetForegroundWindow(window.Handle);
        }
    }
}
