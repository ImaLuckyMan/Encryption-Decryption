using System;

namespace Encrypt_Decrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");

            string plain_text = Util.GetPlainText();
            string single_key = Util.GetSingleKey();
            string multi_key = Util.GetMultiKey();
            Console.WriteLine();

            Console.WriteLine($"You entered [{plain_text}] in plaintext");
            Console.WriteLine($"You entered [{single_key}] as your singlekey");
            Console.WriteLine($"You entered [{multi_key}] as your multikey");
            Console.WriteLine();

            int[] clean_text = Util.Clean(plain_text);
            int[] clean_skey = Util.Clean(single_key);
            int[] clean_mkey = Util.Clean(multi_key);

            string enc_single = Util.SingleEnc(clean_text, clean_skey);
            string enc_multi = Util.MultiEnc(clean_text, clean_mkey);
            string enc_conti = Util.ContiEnc(clean_text, clean_mkey);

            Console.WriteLine($"Encrypted message with singlekey is [{enc_single}]");
            Console.WriteLine($"Encrypted message with multikey is [{enc_multi}]");
            Console.WriteLine($"Encrypted message with continuous key is [{enc_conti}]");
            Console.WriteLine();

            string dec_single = Util.SingleDec(enc_single, clean_skey);
            string dec_multi = Util.MultiDec(enc_multi, clean_mkey);
            string dec_conti = Util.ContiDec(enc_conti, clean_mkey);

            Console.WriteLine($"Decrypted message with singlekey is [{dec_single}]");
            Console.WriteLine($"Decrypted message with multikey is [{dec_multi}]");
            Console.WriteLine($"Decrypted message with continuous key is [{dec_conti}]");
            Console.WriteLine();
        }
    }

    public class Util
    {
        public static int GetPosition(int character) => character - 'A' + 1;

        public static void GetInRange(ref int character)
        {
            while (character < 'A')
            {
                character += 26;
            }
            while (character > 'Z')
            {
                character -= 26;
            }
        }

        public static string GetPlainText()
        {
            Console.Write("Please enter your message: ");
            string message = Console.ReadLine();
            return message;
        }

        public static string GetSingleKey()
        {
            string userInput;
            do
            {
                Console.Write("Enter a single key as an alpha character: ");
                userInput = Console.ReadLine();
                string input = userInput.ToUpper();
                if (input[0] < 'A' || input[0] > 'Z' || input.Length > 1)
                {
                    Console.WriteLine("[INVALID] Please enter a single alpha character [A-Z]");
                    userInput = "-1";
                }
            } while (userInput.Equals("-1"));
            return userInput;
        }

        public static string GetMultiKey()
        {
            Console.Write("Enter your multi key as an alpha character: ");
            string input = Console.ReadLine();
            return input;
        }

        public static int[] Clean(string input)
        {
            input = input.ToUpper();
            string cleanedString = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    cleanedString += input[i];
                }
            }
            int[] result = new int[cleanedString.Length];

            for (int i = 0; i < cleanedString.Length; i++)
            {
                result[i] = cleanedString[i];
            }
            return result;
        }

        public static string SingleEnc(int[] text, int[] singlekey)
        {
            int character;                      
            int textLength = text.Length;      
            string result = "";                 

            for (int i = 0; i < textLength; i++)
            {
                character = text[i] + GetPosition(singlekey[0]);
                GetInRange(ref character);
                result += (char)character;
            }
            return result;
        }
        public static string MultiEnc(int[] text, int[] multikey)
        {
            int character;                     
            int keyLength = multikey.Length;    
            int textLength = text.Length;       
            string result = "";                

            for (int i = 0; i < textLength; i++)
            {
                
                character = text[i] + GetPosition(multikey[i % keyLength]);
                GetInRange(ref character);
                result += (char)character;
            }
            return result;
        }
        public static string ContiEnc(int[] text, int[] multikey)
        {
            int character;                      
            int keyLength = multikey.Length;    
            int textLength = text.Length;       
            string result = "";                 

            for (int i = 0; i < textLength; i++)
            {
                if (i < keyLength)
                {
                    character = text[i] + GetPosition(multikey[i]);
                }
                else
                {
                    character = text[i] + GetPosition(text[i - keyLength]);
                }
                GetInRange(ref character);
                result += (char)(character);
            }
            return result;
        }

        public static string SingleDec(string text, int[] singlekey)
        {
            int character;                     
            int textLength = text.Length;       
            string result = "";                 

            for (int i = 0; i < textLength; i++)
            {
                character = text[i] + GetPosition(singlekey[0]) * -1;
                GetInRange(ref character);
                result += (char)(character);
            }
            return result;
        }
        public static string MultiDec(string text, int[] multikey)
        {
            int character;                      
            int keyLength = multikey.Length;    
            int textLength = text.Length;       
            string result = "";                

            for (int i = 0; i < textLength; i++)
            {
                
                character = text[i] + GetPosition(multikey[i % keyLength]) * -1;
                GetInRange(ref character);
                result += (char)(character);
            }
            return result;
        }
        public static string ContiDec(string text, int[] multikey)
        {
            int character;                      
            int keyLength = multikey.Length;    
            int textLength = text.Length;       
            string result = "";                 


            for (int i = 0; i < textLength; i++)
            {
                
                if (i < keyLength)
                {
                    character = text[i] + GetPosition(multikey[i]) * -1;
                }
                
                else
                {
                    character = text[i] + GetPosition(result[i - keyLength]) * -1;
                }
                GetInRange(ref character);
                result += (char)character;
            }
            return result;
        }
    } 
} 
