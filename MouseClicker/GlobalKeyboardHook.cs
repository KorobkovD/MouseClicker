using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseClicker;

internal class GlobalKeyboardHook
{
    private struct KeyboardHookStruct
    {
        public int VkCode;
        public int ScanCode;
        public int Flags;
        public int Time;
        public int DwExtraInfo;
    }

    const int WH_KEYBOARD_LL = 13;
    const int WM_KEYDOWN = 0x100;
    const int WM_KEYUP = 0x101;
    const int WM_SYSKEYDOWN = 0x104;
    const int WM_SYSKEYUP = 0x105;

    /// <summary>
    /// The collections of keys to watch for
    /// </summary>
    public readonly List<Keys> HookedKeys = new();
        
    /// <summary>
    /// Handle to the hook, need this to unhook and call the next hook
    /// </summary>
    private IntPtr _hookHandlePointer = IntPtr.Zero;
        
    /// <summary>
    /// Occurs when one of the hooked keys is pressed
    /// </summary>
    public event KeyEventHandler KeyDown = null!;
    
    /// <summary>
    /// Occurs when one of the hooked keys is released
    /// </summary>
    public event KeyEventHandler KeyUp = null!;
        
    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalKeyboardHook"/> class and installs the keyboard hook.
    /// </summary>
    public GlobalKeyboardHook()
    {
        Hook();
    }

    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations before the
    /// <see cref="GlobalKeyboardHook"/> is reclaimed by garbage collection and uninstalls the keyboard hook.
    /// </summary>
    ~GlobalKeyboardHook()
    {
        Unhook();
    }
        
    /// <summary>
    /// Installs the global hook
    /// </summary>
    private void Hook()
    {

        var hInstance = LoadLibrary("User32");
        _delegateHookProc = HookProc;
        _hookHandlePointer = SetWindowsHookEx(WH_KEYBOARD_LL, _delegateHookProc, hInstance, 0);
    }

    private delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);
    private KeyboardHookProc _delegateHookProc = null!;
    
    /// <summary>
    /// Uninstalls the global hook
    /// </summary>
    private void Unhook()
    {
        UnhookWindowsHookEx(_hookHandlePointer);
    }

    /// <summary>
    /// The callback for the keyboard hook
    /// </summary>
    /// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anything</param>
    /// <param name="wParam">The event type</param>
    /// <param name="lParam">The keyhook event information</param>
    /// <returns></returns>
    private int HookProc(int code, int wParam, ref KeyboardHookStruct lParam)
    {
        if (code >= 0)
        {
            var key = (Keys)lParam.VkCode;
            if (HookedKeys.Contains(key))
            {
                var kea = new KeyEventArgs(key);
                switch (wParam)
                {
                    case WM_KEYDOWN or WM_SYSKEYDOWN:
                        KeyDown(this, kea);
                        break;
                    case WM_KEYUP or WM_SYSKEYUP:
                        KeyUp(this, kea);
                        break;
                }
                if (kea.Handled)
                    return 1;
            }
        }
        return CallNextHookEx(_hookHandlePointer, code, wParam, ref lParam);
    }
        
    /// <summary>
    /// Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
    /// </summary>
    /// <param name="idHook">The id of the event you want to hook</param>
    /// <param name="callback">The callback.</param>
    /// <param name="hInstance">The handle you want to attach the event to, can be null</param>
    /// <param name="threadId">The thread you want to attach the event to, can be null</param>
    /// <returns>a handle to the desired hook</returns>
    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

    /// <summary>
    /// Unhooks the windows hook.
    /// </summary>
    /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
    /// <returns>True if successful, false otherwise</returns>
    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

    /// <summary>
    /// Calls the next hook.
    /// </summary>
    /// <param name="idHook">The hook id</param>
    /// <param name="nCode">The hook code</param>
    /// <param name="wParam">The wparam.</param>
    /// <param name="lParam">The lparam.</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    private static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);

    /// <summary>
    /// Loads the library.
    /// </summary>
    /// <param name="lpFileName">Name of the library</param>
    /// <returns>A handle to the library</returns>
    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string lpFileName);
}