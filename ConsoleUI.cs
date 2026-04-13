using System;
using System.IO;
using System.Threading;

namespace CybersecurityChatbot.UI
{
    /// <summary>
    /// Handles all console UI functionality such as displaying text,
    /// ASCII images, user prompts, and formatting.
    /// </summary>
    public static class ConsoleUI
    {
        // ─── Console colour settings for consistent UI styling ───
        private static readonly ConsoleColor PrimaryColor = ConsoleColor.Cyan;
        private static readonly ConsoleColor AccentColor = ConsoleColor.Green;
        private static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
        private static readonly ConsoleColor BotColor = ConsoleColor.Cyan;
        private static readonly ConsoleColor UserColor = ConsoleColor.White;
        private static readonly ConsoleColor DimColor = ConsoleColor.DarkGray;

        /// <summary>
        /// Displays the main chatbot header/logo using text formatting.
        /// </summary>
        public static void DisplayLogo()
        {
            Console.Clear(); // Clears console for fresh display
            Console.ForegroundColor = AccentColor;

            // Decorative header box
            Console.WriteLine();
            Console.WriteLine("  +----------------------------------------------------------+");
            Console.WriteLine("  |                                                          |");
            Console.WriteLine("  |        CYBERSECURITY AWARENESS ASSISTANT                 |");
            Console.WriteLine("  |          Protecting South African Citizens               |");
            Console.WriteLine("  |                                                          |");
            Console.WriteLine("  +----------------------------------------------------------+");
            Console.WriteLine();

            // Subheading text
            Console.ForegroundColor = DimColor;
            Console.WriteLine("  Department of Cybersecurity  |  South Africa");
            Console.WriteLine();
            Console.ResetColor();
        }

        /// <summary>
        /// Converts the image (logo.jpg) into ASCII characters
        /// and displays it in the console.
        /// </summary>
        public static void DisplayAsciiImage()
        {
            try
            {
                // Get full path to logo.jpg inside output directory
                string full_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg");

                // Debug: prints path being used
                Console.WriteLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg"));

                // Debug: checks if file exists (true/false)
                Console.WriteLine(File.Exists(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg")
));

                // Check if image file exists before loading
                if (!File.Exists(full_path))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  [Image] logo.jpg not found.");
                    Console.ResetColor();
                    Console.WriteLine();
                    return;
                }

                // Additional safety check (prevents crashes)
                if (!File.Exists(full_path))
                {
                    Console.ForegroundColor = DimColor;
                    Console.WriteLine("  [Image] logo.jpg not found - place it in the project root.");
                    Console.ResetColor();
                    Console.WriteLine();
                    return;
                }

                // Load the image into a Bitmap object
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(full_path);

                // Resize image for console display
                System.Drawing.Bitmap resized = new System.Drawing.Bitmap(
                    image, new System.Drawing.Size(100, 40));

                // ASCII characters used for grayscale mapping
                string asciiChars = "@#S%?*+;:,. ";
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Loop through each pixel row
                for (int y = 0; y < resized.Height; y++)
                {
                    Console.Write("  "); // Left margin

                    // Loop through each pixel column
                    for (int x = 0; x < resized.Width; x++)
                    {
                        // Get pixel color
                        System.Drawing.Color pixel = resized.GetPixel(x, y);

                        // Convert RGB to grayscale
                        int gray = (pixel.R + pixel.G + pixel.B) / 3;

                        // Map grayscale to ASCII character index
                        int index = (gray * (asciiChars.Length - 1)) / 255;

                        // Print ASCII character
                        Console.Write(asciiChars[index]);
                    }

                    Console.WriteLine(); // Move to next line
                }

                Console.ResetColor();

                // Free memory resources
                resized.Dispose();
                image.Dispose();

                Console.WriteLine();
            }
            catch
            {
                // Handles any unexpected errors silently
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays welcome banner text.
        /// </summary>
        public static void DisplayWelcomeBanner()
        {
            PrintDivider();
            PrintColored("  Welcome. I am your Cybersecurity Awareness Assistant.", PrimaryColor);
            PrintColored("  I am here to help you stay safe online in South Africa.", PrimaryColor);
            PrintDivider();
            Console.WriteLine();
        }

        /// <summary>
        /// Prompts user to enter their name and validates input.
        /// </summary>
        public static string GetUserName()
        {
            string name = string.Empty;

            // Loop until valid input is entered
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = BotColor;
                Console.Write("  Bot: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Before we begin, what is your name?  ");
                Console.ResetColor();

                // Read user input
                name = Console.ReadLine() ?? string.Empty;
                name = name.Trim();

                // Validate input
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = WarningColor;
                    Console.WriteLine("  Please enter your name to continue.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            Console.WriteLine();

            // Display greeting messages with typing effect
            TypewriterEffect("  Bot: Hello, " + name + ". It is great to meet you.", BotColor);
            TypewriterEffect("  Bot: You may ask me about phishing, passwords, safe browsing,", BotColor);
            TypewriterEffect("       malware, social engineering, or type help for all topics.", BotColor);
            TypewriterEffect("       Type exit or quit at any time to leave.", BotColor);

            Console.WriteLine();
            PrintDivider();
            Console.WriteLine();

            return name;
        }

        /// <summary>
        /// Displays a chatbot message (with optional typing effect).
        /// </summary>
        public static void DisplayBotMessage(string message, bool typewriter = true)
        {
            Console.WriteLine();

            if (typewriter)
                TypewriterEffect("  Bot: " + message, BotColor);
            else
                PrintColored("  Bot: " + message, BotColor);

            Console.WriteLine();
        }

        /// <summary>
        /// Displays multiple chatbot messages (used for paragraphs or lists).
        /// </summary>
        public static void DisplayBotBlock(string[] lines)
        {
            Console.WriteLine();

            foreach (string line in lines)
            {
                if (line.StartsWith("  "))
                    PrintColored(line, ConsoleColor.White);
                else
                    TypewriterEffect("  Bot: " + line, BotColor, delayMs: 18);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prompts user for input during chat.
        /// </summary>
        public static string PromptUserInput(string userName)
        {
            Console.ForegroundColor = UserColor;
            Console.Write("  " + userName + ": ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine() ?? string.Empty;
            Console.ResetColor();

            return input.Trim();
        }

        /// <summary>
        /// Displays warning or error message.
        /// </summary>
        public static void DisplayError(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = WarningColor;
            Console.WriteLine("  Notice: " + message);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays goodbye message and waits for user input before closing.
        /// </summary>
        public static void DisplayGoodbye(string userName)
        {
            Console.WriteLine();
            PrintDivider();

            TypewriterEffect("  Bot: Goodbye, " + userName + ". Stay safe online.", BotColor);
            TypewriterEffect("  Bot: Remember - think before you click.", AccentColor);

            PrintDivider();
            Console.WriteLine();

            Console.ForegroundColor = DimColor;
            Console.WriteLine("  Press any key to close...");
            Console.ResetColor();

            Console.ReadKey(); // Pause before closing
        }

        /// <summary>
        /// Displays help menu with available chatbot topics.
        /// </summary>
        public static void DisplayHelp()
        {
            Console.WriteLine();
            PrintDivider();

            Console.ForegroundColor = AccentColor;
            Console.WriteLine("  TOPICS YOU CAN ASK ME ABOUT");

            PrintDivider();
            Console.WriteLine();

            // Topics stored as arrays (ID, keyword, description)
            string[][] topics = new string[][]
            {
                new string[]{"01", "password",           "Safe password practices"},
                new string[]{"02", "phishing",           "Recognising phishing scams"},
                new string[]{"03", "safe browsing",      "How to browse the internet safely"},
                new string[]{"04", "malware",            "Avoiding viruses and ransomware"},
                new string[]{"05", "social engineering", "Recognising manipulation tactics"},
                new string[]{"06", "privacy",            "Protecting your personal information"},
                new string[]{"07", "two-factor or 2fa",  "Two-factor authentication"},
                new string[]{"08", "backup",             "Data backup best practices"},
                new string[]{"09", "south africa",       "SA-specific cybersecurity information"},
                new string[]{"10", "vpn",                "VPN and public Wi-Fi safety"},
                new string[]{"11", "scam",               "Common South African online scams"},
                new string[]{"12", "firewall",           "Firewall protection"},
                new string[]{"13", "identity",           "Preventing identity theft"},
                new string[]{"14", "update",             "Why software updates matter"},
                new string[]{"--", "help",               "Show this topic list again"},
                new string[]{"--", "exit or quit",       "Exit the chatbot"},
            };

            // Display each topic
            foreach (string[] topic in topics)
            {
                Console.ForegroundColor = DimColor;
                Console.Write("    [" + topic[0] + "]  ");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(topic[1].PadRight(24));

                Console.ForegroundColor = DimColor;
                Console.WriteLine("  " + topic[2]);
            }

            Console.ResetColor();
            Console.WriteLine();

            PrintDivider();
            Console.WriteLine();
        }

        /// <summary>
        /// Helper method to print colored text.
        /// </summary>
        private static void PrintColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Helper method to print divider line.
        /// </summary>
        private static void PrintDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  " + new string('-', 60));
            Console.ResetColor();
        }

        /// <summary>
        /// Displays text character-by-character to simulate typing.
        /// </summary>
        private static void TypewriterEffect(string text, ConsoleColor color, int delayMs = 22)
        {
            Console.ForegroundColor = color;

            foreach (char ch in text)
            {
                Console.Write(ch);
                Thread.Sleep(delayMs); // Delay between characters
            }

            Console.WriteLine();
            Console.ResetColor();
        }
    }
}