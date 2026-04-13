using System.Media;
using System.IO;

namespace CybersecurityChatbot
{
    public static class AudioPlayer
    {
        // Method to play the greeting audio file
        public static void PlayGreeting()
        {
            try
            {
                // Build the full file path to greeting.wav in the application directory
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,  // Get the base directory where the app is running
                    "greeting.wav"                          // The audio file name
                );

                // Check if the audio file exists at the specified path
                if (File.Exists(path))
                {
                    // Create a new SoundPlayer instance with the audio file
                    SoundPlayer player = new SoundPlayer(path);

                    // Play the audio synchronously (blocks execution until audio finishes)
                    player.PlaySync(); // waits until audio finishes
                }
            }
            catch
            {
                // Silently handle any exceptions (file not found, invalid format, etc.)
                // do nothing if audio fails
            }
        }
    }
}