using System;
using CybersecurityChatbot;
using CybersecurityChatbot.Logic;
using CybersecurityChatbot.UI;

namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Set console window title
                Console.Title = "Cybersecurity Awareness Bot";
            }
            catch { } // Ignore errors if console title cannot be set

            try
            {
                // Play startup audio greeting
                AudioPlayer.PlayGreeting();
            }
            catch { } // Prevent crash if audio fails

            try
            {
                // Display main ASCII/text logo
                ConsoleUI.DisplayLogo();
            }
            catch { } // Prevent crash if logo fails

            try
            {
                // Display ASCII image (converted logo.jpg)
                ConsoleUI.DisplayAsciiImage();
            }
            catch { } // Prevent crash if image rendering fails

            try
            {
                // Show welcome banner text
                ConsoleUI.DisplayWelcomeBanner();
            }
            catch { } // Prevent crash if banner fails

            try
            {
                // Get user name and start chatbot session
                string userName = ConsoleUI.GetUserName();

                // Create chat engine instance
                ChatEngine chatEngine = new ChatEngine(userName);

                // Start conversation loop
                chatEngine.StartChat();
            }
            catch (Exception ex)
            {
                // Final error handler (prevents app crash)
                Console.WriteLine();
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

            // ✅ THIS IS WHAT YOU NEEDED TO ADD (VERY END OF MAIN)
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}