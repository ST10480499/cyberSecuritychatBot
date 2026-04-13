using System;
using CybersecurityChatbot.UI;

namespace CybersecurityChatbot.Logic
{
    /// <summary>
    /// ChatEngine controls the main chatbot loop,
    /// processes user input, and connects UI with responses.
    /// </summary>
    public class ChatEngine
    {
        // Stores the current user's name for personalized responses
        private readonly string _userName;

        // Handles all predefined chatbot responses
        private readonly ResponseLibrary _responses;

        /// <summary>
        /// Constructor initializes chatbot with user name and response library.
        /// </summary>
        public ChatEngine(string userName)
        {
            _userName = userName;
            _responses = new ResponseLibrary(userName);
        }

        /// <summary>
        /// Starts the main chat loop where user interacts with chatbot.
        /// </summary>
        public void StartChat()
        {
            bool running = true; // Controls loop execution

            // Main chatbot loop
            while (running)
            {
                // Get input from user via UI layer
                string input = ConsoleUI.PromptUserInput(_userName);

                // Validate empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    ConsoleUI.DisplayError(
                        "I did not receive any input. Could you please type your question?");
                    continue; // Skip to next loop iteration
                }

                // Normalize input for easier comparison
                string normalised = input.ToLower().Trim();

                // Check if user wants to exit chatbot
                if (normalised is "exit" or "quit" or "bye" or "goodbye")
                {
                    ConsoleUI.DisplayGoodbye(_userName);
                    running = false; // Stop loop
                    continue;
                }

                // Get response from response library based on user input
                string[]? response = _responses.GetResponse(normalised);

                // If valid response exists, display it
                if (response != null && response.Length > 0)
                    ConsoleUI.DisplayBotBlock(response);

                // If response is empty array (used for help menu already printed)
                else if (response != null && response.Length == 0)
                {
                    // Do nothing (help already handled in ResponseLibrary)
                }

                // If no matching response found
                else
                    ConsoleUI.DisplayBotMessage(
                        "I did not quite understand that. Could you rephrase? " +
                        "Try typing 'help' for a list of topics I can assist with.");
            }
        }
    }
}