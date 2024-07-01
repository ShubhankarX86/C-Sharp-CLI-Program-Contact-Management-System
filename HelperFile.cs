using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Test3;
using static System.Collections.Specialized.BitVector32;

namespace helper
{
    public partial class Helper
    {
        public static DateTime now = DateTime.Now;

        public static string GenLogDir()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo Dinfo = new DirectoryInfo(path).Parent.Parent.Parent;
            string myloc = Path.Combine(Dinfo.FullName, "logs");

            Directory.CreateDirectory(myloc);

            string finPath = Path.Combine(myloc, "logs.txt");

            return finPath;
        }

        public static void WriteLog(string message)
        {
            FileStream fs = new FileStream(GenLogDir(), FileMode.Append, FileAccess.Write);

            message += "\n\n";
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            fs.Write(bytes, 0, message.Length);
            fs.Close();
        }

        public static void RegexValidator(ref string input, string pattern, string action)
        {
            while (!Regex.IsMatch(input, pattern))
            {
                Console.WriteLine("Invalid input! Try again!");
                WriteLog(now.ToString() + " - User entered an invalid input while adding " + action + ". User input(" + input + ")");
                input = Console.ReadLine();
            }
        }

        public static string IDParser(string ID)
        {
            int i = ID.Length - 1;
            int multiplier = 1;
            int preID = 1;
            string postID = "";
            string finID = "";

            while (i >= 0)
            {
                preID += (ID[i] - '0') * multiplier;
                multiplier *= 10;
                i--;
            }

            while (preID > 0)
            {
                postID += (char)(preID % 10 + '0');
                preID /= 10;
            }

            for (i = postID.Length - 1; i >= 0; i--)
            {
                finID += postID[i];
            }

            return finID;
        }

        public static void duplicateValidator(ref string input, List<Contact> contactList, string action)
        {
            bool functionState = true;

            while (functionState)
            {
                int i = 0;
                for (i = 0; i < contactList.Count; i++)
                {
                    if (action == "name" && input == contactList[i].name)
                    {
                        break;
                    }
                    if (action == "phone number" && input == contactList[i].phone)
                    {
                        break;
                    }
                    if (action == "email ID" && input == contactList[i].email)
                    {
                        break;
                    }
                }

                if(i < contactList.Count)
                {
                    Console.WriteLine(action + " already exists! Try again!");
                    WriteLog(now.ToString() + " - User entered a " + action + " that already exists!" + ". User input(" + input + ")");
                    input = Console.ReadLine();
                }
                else
                {
                  
                    functionState = false;
                }
            }
        }
    }
}
