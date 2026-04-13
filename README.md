# Cybersecurity Awareness Chatbot
## Student: Roby Manhica| Student Number: ST10480499

---

## About This Project

This is a console-based Cybersecurity Awareness Chatbot built in C# for South African citizens.
It was developed as part of the Department of Cybersecurity awareness campaign.

---

## How to Run the Program

1. Open the solution file in Visual Studio 2022
2. Make sure the NuGet package Microsoft.Windows.Compatibility 8.0.0 is installed
3. Place greeting.wav inside the Audio folder
4. Press F5 to build and run

---

## Features

- Voice greeting plays on startup
- ASCII art logo displayed as header
- ASCII image generated from logo.jpg using the lecturer code
- Interactive question and answer about cybersecurity topics
- Colour coded console interface with typewriter effect
- Input validation handles empty and unknown inputs
- Personalised responses using the user name
- Lecturer ArrayList logic included from ai_response project

---

## Topics the Bot Can Help With

- Password safety
- Phishing awareness
- Safe browsing
- Malware protection
- Social engineering
- Privacy and POPIA
- Two factor authentication
- Data backup
- VPN and public Wi-Fi
- South African cybercrime resources
- Online scams
- Firewall protection
- Identity theft prevention
- Software updates

---

## Project Structure

- Program.cs          Entry point and startup
- AudioPlayer.cs      Plays the WAV voice greeting
- ConsoleUI.cs        All display formatting and user input
- ChatEngine.cs       Main chat loop and input handling
- ResponseLibrary.cs  All responses including lecturer ArrayList logic
- Audio folder        Contains greeting.wav
- logo.jpg            Place in project root for ASCII image display

---

## NuGet Package Required

Microsoft.Windows.Compatibility Version 8.0.0

Install using Package Manager Console:
Install-Package Microsoft.Windows.Compatibility -Version 8.0.0

---

## GitHub Actions CI

This project uses GitHub Actions for Continuous Integration.
Every push to main triggers an automated build check.
A green tick confirms the build passed successfully.

---

## References

Pieterse H. 2021. The Cyber Threat Landscape in South Africa A 10-Year Review.
The African Journal of Information and Communication 28(28).
Available at www.scielo.org.za
