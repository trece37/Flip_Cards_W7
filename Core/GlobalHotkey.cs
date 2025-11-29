using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Flip_Cards_W7
{
    public static class GlobalHotkey
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        
        private const int HOTKEY_ID = 9000;
        private const int WM_HOTKEY = 0x0312;
        
        private static IntPtr windowHandle;
        private static HwndSource source;
        private static Action callback;
        
        public static void RegisterHotkey(Window window, ModifierKeys modifiers, Key key, Action onHotkeyPressed)
        {
            var helper = new WindowInteropHelper(window);
            windowHandle = helper.Handle;
            
            source = HwndSource.FromHwnd(windowHandle);
            source.AddHook(HwndHook);
            
            callback = onHotkeyPressed;
            
            uint mod = 0;
            if ((modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                mod |= 0x0002; // MOD_CONTROL
            if ((modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                mod |= 0x0004; // MOD_SHIFT
            if ((modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                mod |= 0x0001; // MOD_ALT
            if ((modifiers & ModifierKeys.Windows) == ModifierKeys.Windows)
                mod |= 0x0008; // MOD_WIN
            
            uint vk = (uint)KeyInterop.VirtualKeyFromKey(key);
            
            if (!RegisterHotKey(windowHandle, HOTKEY_ID, mod, vk))
            {
                MessageBox.Show("No se pudo registrar el atajo de teclado Ctrl+Shift+Tab.\n" +
                               "Puede que esté en uso por otra aplicación.", 
                               "Flip_Cards-W7", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        public static void UnregisterHotkey(Window window)
        {
            if (source != null)
            {
                source.RemoveHook(HwndHook);
                source = null;
            }
            
            if (windowHandle != IntPtr.Zero)
            {
                UnregisterHotKey(windowHandle, HOTKEY_ID);
                windowHandle = IntPtr.Zero;
            }
        }
        
        private static IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY && wParam.ToInt32() == HOTKEY_ID)
            {
                callback?.Invoke();
                handled = true;
            }
            return IntPtr.Zero;
        }
    }
}
