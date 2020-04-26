// MonoGame Generated Namespace References
using System;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // MonoGame Generated Run Statement
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
