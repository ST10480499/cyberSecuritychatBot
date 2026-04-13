using System.Media;
using System.IO;

namespace CybersecurityChatbot
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "greeting.wav"
                );

                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.PlaySync(); // waits until audio finishes
                }
            }
            catch
            {
                // do nothing if audio fails
            }
        }
    }
}