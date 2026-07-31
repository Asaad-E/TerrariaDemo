using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Raylib_cs;

namespace TerrariaDemo.Core;

public static class CustomAssert
{
    // Native Win32 API for OS message dialogs
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    private const uint MB_OK = 0x00000000;
    private const uint MB_ICONERROR = 0x00000010;

    /// <summary>
    /// Debug-only check. Stripped out in Release builds completely.
    /// Breaks into the IDE debugger on failure.
    /// </summary>
    [Conditional("DEBUG")]
    public static void Check(
        bool condition,
        string message = "",
        [CallerArgumentExpression(nameof(condition))] string expression = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        if (condition) return;

        string output = $"[DEBUG ASSERTION FAILED]\n" +
                        $"Expression: {expression}\n" +
                        $"Message:    {(string.IsNullOrEmpty(message) ? "N/A" : message)}\n" +
                        $"Location:   {file} (Line {line})";

        Console.Error.WriteLine(output);

        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }
    }

    /// <summary>
    /// Active in BOTH Debug and Release builds.
    /// Closes Raylib, shows a native OS error dialog, and terminates the app.
    /// </summary>
    public static void Assert(
        bool condition,
        string message = "",
        [CallerArgumentExpression(nameof(condition))] string expression = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        if (condition) return;

        string dialogBody = 
            $"Assertion failed\n\n" +
            $"File:\n{file}\n" +
            $"Line: {line}\n\n" +
            $"Expression: {expression}\n\n" +
            $"Comment: {(string.IsNullOrEmpty(message) ? "N/A" : message)}\n\n" +
            $"Please report this error to the developer.";

        Console.Error.WriteLine(dialogBody);

        // Break first if attached to an IDE during development
        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }

        // Close OpenGL context cleanly before popping up the dialog
        if (Raylib.IsWindowReady())
        {
            Program.CloseWindow();
        }

        // Pop up the OS error window
        MessageBox(IntPtr.Zero, dialogBody, "Game Error", MB_OK | MB_ICONERROR);

        // Terminate process
        Environment.Exit(1);
    }
}