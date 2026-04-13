using System;
using System.Collections;
using System.Collections.Generic;
using CybersecurityChatbot.UI;

namespace CybersecurityChatbot.Logic
{
    public class ResponseLibrary
    {
        private readonly string _userName;

        // storing user name so we can personalize responses (like saying their name back)
        ArrayList answers = new ArrayList();
        ArrayList ignoring = new ArrayList();


        // this is basically our "smart matching system"
        // if user types certain keywords, we return a matching response
        private readonly List<(string[] keywords, Func<string[]> response)> _responseMap;

        public ResponseLibrary(string userName)
        {
            _userName = userName;
            LoadLecturerData();

            // keyword map for chatbot responses (this is the main brain of the bot)
            _responseMap = new List<(string[], Func<string[]>)>
            {
                (new[]{"how are you","how are u"},                          ResponseHowAreYou),
                (new[]{"your purpose","what do you do","what are you"},     ResponsePurpose),
                (new[]{"hello","hi ","hey","greetings","howzit"},           ResponseHello),
                (new[]{"thank","thanks","appreciate"},                      ResponseThanks),
                (new[]{"password","passphrase"},                            ResponsePassword),
                (new[]{"phishing","phish","suspicious email","fake email"}, ResponsePhishing),
                (new[]{"safe brows","browser","website","url"},             ResponseSafeBrowsing),
                (new[]{"malware","virus","ransomware","spyware","trojan"},  ResponseMalware),
                (new[]{"social engineer","manipulat","pretexting"},         ResponseSocialEngineering),
                (new[]{"privacy","personal info","popia"},                  ResponsePrivacy),
                (new[]{"two-factor","2fa","multi-factor","mfa"},            ResponseTwoFactor),
                (new[]{"backup","back up","data loss"},                     ResponseBackup),
                (new[]{"south africa","sa cyber","ecta"},                   ResponseSouthAfrica),
                (new[]{"vpn","virtual private","public wifi"},              ResponseVPN),
                (new[]{"scam","fraud","419","advance fee"},                 ResponseScam),
                (new[]{"firewall"},                                         ResponseFirewall),
                (new[]{"identity theft","identity"},                        ResponseIdentity),
                (new[]{"update","patch"},                                   ResponseUpdates),
            };
        }

        // answer responses 
        private void LoadLecturerData()
        {
            answers.Add("password::Passwords must be at least 8 characters long. Use uppercase, lowercase, numbers and symbols. Never reuse passwords and never share them.");
            answers.Add("phishing::Phishing is a fake email designed to steal your details. Never click links in unexpected emails. Type the website address yourself.");
            answers.Add("malware::Malware includes viruses, ransomware and spyware. Keep your antivirus updated and never open unknown email attachments.");
            answers.Add("virus::A virus spreads between files. Use Windows Defender, keep it updated, and never plug in unknown USB drives.");
            answers.Add("ransomware::Ransomware locks all your files and demands payment. Keep software updated, back up data, and never open unexpected attachments.");
            answers.Add("browsing::Safe browsing: Always check for HTTPS and the padlock. Never download pirated software as it contains malware.");
            answers.Add("vpn::A VPN encrypts your internet connection. Never do banking on public Wi-Fi without a VPN. Free options include ProtonVPN and Windscribe.");
            answers.Add("2fa::Two-Factor Authentication means you need your password and a one-time code. Even if hackers steal your password, they cannot get in.");
            answers.Add("scam::South African scams often impersonate SARS or banks. Never share OTPs, PINs, or passwords with anyone who calls you.");
            answers.Add("firewall::A firewall blocks unauthorised access. Windows has one built in — keep it ON at all times.");
            answers.Add("backup::Use the 3-2-1 rule: 3 copies, on 2 types of storage, with 1 copy offsite or in the cloud.");
            answers.Add("social::Social engineering tricks your mind. No legitimate company will ever ask for your password.");
            answers.Add("update::Keep all software updated. Most cyberattacks exploit flaws in outdated software. Enable automatic updates.");
            answers.Add("wifi::Public Wi-Fi is dangerous. Hackers can intercept everything. Always use a VPN on public networks.");
            answers.Add("bank::Always type your bank URL yourself. Never click email links to your bank.");
            answers.Add("cybercrime::Report cybercrime to SAPS at cybercrime@saps.gov.za or call 0861 000 272.");
            answers.Add("privacy::Use strong passwords, enable 2FA, limit social media sharing, and review app permissions.");
            answers.Add("identity::Never share your ID number, bank details, or OTPs online or over the phone.");

            // words we ignore because they don't help meaningfully in matching
            ignoring.Add("what"); ignoring.Add("is"); ignoring.Add("about");
            ignoring.Add("the"); ignoring.Add("a"); ignoring.Add("an");
            ignoring.Add("are"); ignoring.Add("how"); ignoring.Add("do");
            ignoring.Add("i"); ignoring.Add("can"); ignoring.Add("tell");
            ignoring.Add("me"); ignoring.Add("my"); ignoring.Add("please");
            ignoring.Add("you"); ignoring.Add("your"); ignoring.Add("give");
            ignoring.Add("explain"); ignoring.Add("more"); ignoring.Add("want");
        }

        // tries to match lecturer-style answers based on keywords in user input
        private string? SearchLecturerAnswers(string question)
        {
            string[] find_WORDS = question.Split(' ');
            bool found = false;
            string message = string.Empty;

            foreach (string words in find_WORDS)
            {
                foreach (string find_answer in answers)
                {
                    string[] parts = find_answer.ToString()!
                        .Split(new string[] { "::" }, StringSplitOptions.None);

                    if (parts.Length < 2) continue;

                    string keyword = parts[0];
                    string response = parts[1];

                    // if word matches keyword and it's not in ignore list
                    if (keyword.Contains(words.ToLower()) &&
                        !ignoring.Contains(words.ToLower()))
                    {
                        message = response;
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }

            return found ? message : null;
        }

        // main function that decides what response to give the user
        public string[]? GetResponse(string normalisedInput)
        {
            // special case: help menu
            if (normalisedInput.Contains("help") || normalisedInput.Contains("topic"))
            {
                ConsoleUI.DisplayHelp();
                return Array.Empty<string>();
            }

            // check keyword-based responses first
            foreach (var (keywords, responseFunc) in _responseMap)
                foreach (var kw in keywords)
                    if (normalisedInput.Contains(kw))
                        return responseFunc();

            // fallback: try lecturer answers
            string? lecturerAnswer = SearchLecturerAnswers(normalisedInput);
            if (lecturerAnswer != null)
                return new[] { lecturerAnswer };

            // if nothing matches, return null so chatbot shows default response
            return null;
        }

        // simple friendly responses below (self-explanatory chatbot replies)

        private string[] ResponseHello() => new[]
        {
            "Hello there, " + _userName + ".",
            "  How can I help you stay safe online today?",
            "  Type 'help' to see all the cybersecurity topics I can assist with."
        };

        private string[] ResponseHowAreYou() => new[]
        {
            "I am doing well, thank you — always on guard against cyber threats.",
            "  How can I help you stay safe online today?"
        };

        private string[] ResponsePurpose() => new[]
        {
            "My purpose is to educate South African citizens about cybersecurity.",
            "  I can help you with phishing, passwords, malware, VPNs, 2FA, and more.",
            "  Type 'help' for the full list of topics."
        };

        private string[] ResponseThanks() => new[]
        {
            "You are very welcome, " + _userName + ".",
            "  Staying informed is the first step to staying safe online.",
            "  Is there anything else you would like to know?"
        };

        // password safety tips (very important for users)
        private string[] ResponsePassword() => new[]
        {
            "Here are key password safety tips:",
            "  [+]  Use at least 12 characters — longer is stronger.",
            "  [+]  Mix uppercase, lowercase, numbers, and symbols.",
            "  [+]  Use a passphrase like: Coffee-Sunset-River-2024",
            "  [-]  Never reuse passwords across different websites.",
            "  [-]  Never share your password — not even with IT support.",
            "  [*]  Consider a password manager such as Bitwarden which is free.",
            "  [*]  Enable two-factor authentication on every account."
        };

        // phishing explanation + warning signs
        private string[] ResponsePhishing() => new[]
        {
            "Phishing is one of the most common cyber threats in South Africa.",
            "  Warning signs in emails or messages:",
            "  [!]  Urgent language such as: Your account closes in 24 hours.",
            "  [!]  Generic greeting: Dear Customer instead of your actual name.",
            "  [!]  Suspicious sender address that does not match the real company.",
            "  [!]  Links that do not match the real website.",
            "  [!]  Unexpected attachments such as .exe or .zip files.",
            "  [+]  Go directly to the company official website instead.",
            "  [+]  Report phishing to: cybercrime@saps.gov.za"
        };

        // safe browsing tips
        private string[] ResponseSafeBrowsing() => new[]
        {
            "Safe browsing is essential for your online security.",
            "  [+]  Always check for HTTPS and the padlock before entering info.",
            "  [+]  Keep your browser and all software up to date.",
            "  [+]  Use an ad blocker such as uBlock Origin.",
            "  [-]  Never click on pop-up advertisements.",
            "  [-]  Never download software from unofficial sources.",
            "  [*]  Use a VPN on public Wi-Fi at malls, airports, and cafes."
        };

        // malware explanation
        private string[] ResponseMalware() => new[]
        {
            "Malware is malicious software designed to harm your device or steal data.",
            "  Types you should know:",
            "  [!]  Virus       — attaches to files and spreads to other computers.",
            "  [!]  Ransomware  — locks your files and demands payment to unlock them.",
            "  [!]  Spyware     — silently monitors your activity and steals information.",
            "  [!]  Trojan      — disguises itself as safe and legitimate software.",
            "  [+]  Keep your antivirus updated. Windows Defender is free and built in.",
            "  [+]  Never open email attachments from unknown senders.",
            "  [+]  Always scan USB drives before opening files on them."
        };

        // social engineering explanation
        private string[] ResponseSocialEngineering() => new[]
        {
            "Social engineering manipulates people rather than computers.",
            "  Common tactics used in South Africa:",
            "  [!]  Pretexting — attacker pretends to be your bank or SARS.",
            "  [!]  Baiting    — free movies or software downloads that contain malware.",
            "  [!]  Vishing    — phone call scams pretending to be from your bank.",
            "  [!]  Smishing   — SMS messages with fake links.",
            "  [*]  Legitimate organisations will NEVER ask for your password or OTP.",
            "  [+]  Always verify by calling the official number yourself."
        };

        // privacy tips (POPIA context)
        private string[] ResponsePrivacy() => new[]
        {
            "Protecting your personal information is your right in South Africa.",
            "  Under POPIA, organisations must handle your data responsibly.",
            "  [+]  Limit what personal information you share on social media.",
            "  [+]  Review app permissions carefully before installing new apps.",
            "  [-]  Never share your ID number, banking details, or OTP with anyone.",
            "  [*]  Check if your email was breached at: haveibeenpwned.com"
        };

        // 2FA explanation
        private string[] ResponseTwoFactor() => new[]
        {
            "Two-Factor Authentication adds a second layer of security to your accounts.",
            "  Even if someone steals your password, they still cannot get in.",
            "  [+]  Authenticator app is best: Google or Microsoft Authenticator.",
            "  [+]  SMS code is good but can be intercepted in some cases.",
            "  [+]  Hardware key is best for high security accounts such as YubiKey.",
            "  [+]  Enable 2FA on your email, banking, and social media now.",
            "  [-]  Never share your OTP with anyone — not even your bank."
        };

        // backup tips
        private string[] ResponseBackup() => new[]
        {
            "Regular backups protect you from ransomware and data loss.",
            "  Follow the 3-2-1 backup rule:",
            "  [1]  Keep 3 copies of your data.",
            "  [2]  Store them on 2 different types of media.",
            "  [3]  Keep 1 copy offsite such as Google Drive or OneDrive.",
            "  [+]  Test your backups regularly to make sure they actually work.",
            "  [+]  Automate your backups so you never forget to do them."
        };

        // South African cybersecurity context
        private string[] ResponseSouthAfrica() => new[]
        {
            "South Africa faces unique and serious cybersecurity challenges.",
            "  Key facts:",
            "  [*]  SA is one of the top targets for cybercrime in Africa.",
            "  [*]  POPIA protects your personal data rights as a citizen.",
            "  [*]  The Cybercrimes Act 19 of 2020 covers online crimes.",
            "  [+]  Report cybercrime: cybercrime@saps.gov.za or 0861 000 272",
            "  [+]  SABRIC banking fraud hotline: 011 847 3000",
            "  [+]  Cybersecurity Hub: www.cybersecurityhub.gov.za",
            "  [+]  CERT-SA: www.cert.gov.za"
        };

        // VPN explanation
        private string[] ResponseVPN() => new[]
        {
            "A VPN encrypts your internet connection and keeps your data private.",
            "  When you should use a VPN:",
            "  [+]  On public Wi-Fi at airports, malls, and coffee shops.",
            "  [+]  When accessing sensitive accounts away from home.",
            "  [*]  Reputable VPN options: ProtonVPN which is free, NordVPN, ExpressVPN.",
            "  [-]  Avoid unknown free VPNs — many of them log and sell your data.",
            "  [!]  A VPN does not make you completely anonymous online."
        };

        // scam awareness
        private string[] ResponseScam() => new[]
        {
            "Online scams are very common in South Africa — stay alert.",
            "  Common scams to be aware of:",
            "  [!]  419 advance-fee scam: Send money now to claim your prize.",
            "  [!]  Bank impersonation: Fake calls claiming to be from your bank.",
            "  [!]  Online shopping: Deals that seem too good to be true.",
            "  [!]  Job scams: Earn money from home but pay a registration fee first.",
            "  [*]  Rule: If it sounds too good to be true, it almost certainly is.",
            "  [+]  Report scams to SAPS or the South African Fraud Prevention Service."
        };

        // firewall explanation
        private string[] ResponseFirewall() => new[]
        {
            "A firewall is your first line of defence against network attacks.",
            "  [+]  Windows Firewall is built in — make sure it is always switched ON.",
            "  [+]  Check: Settings > Windows Security > Firewall and network protection.",
            "  [+]  Enable the firewall on your home router as well.",
            "  [-]  Never disable your firewall just because an application asks you to."
        };

        // identity theft awareness
        private string[] ResponseIdentity() => new[]
        {
            "Identity theft is a serious and growing crime in South Africa.",
            "  How criminals steal your identity:",
            "  [!]  Phishing emails that trick you into entering your personal details.",
            "  [!]  Going through your physical mail or discarded documents.",
            "  [+]  Never share your ID number or bank details with anyone.",
            "  [+]  Shred documents containing personal information before discarding.",
            "  [+]  Check your credit report regularly for suspicious activity."
        };

        // software update importance
        private string[] ResponseUpdates() => new[]
        {
            "Keeping your software updated is one of the most important security habits.",
            "  [+]  Enable automatic updates on Windows, your browser, and all apps.",
            "  [+]  Update your router firmware — most people forget this step.",
            "  [+]  Keep your antivirus definitions updated every day.",
            "  [-]  Never ignore notifications about critical security updates.",
            "  [!]  Most successful cyberattacks target outdated and unpatched software."
        };
    }
}