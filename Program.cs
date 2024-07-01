using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using helper;

namespace Test3
{
    public class Contact
    {
        private string ContactID;
        private string Name;
        private string Phone;
        private string Email;

        public string ID
        {
            get { return ContactID; }
            set { ContactID = value; }
        }

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        public string phone
        {
            get { return Phone; }
            set { Phone = value; }
        }

        public string email
        {
            get { return Email; }
            set { Email = value; }
        }

        public Contact(string contactID, string name, string phone, string email) {

            this.ID = contactID;
            this.name = name;
            this.phone = phone;
            this.email = email;
        }

        static List<Contact> FillList()
        {
            Contact A = new Contact("1234", "Shubhankar Sharma", "8558844667", "shubhankar4@gmail.com");
            Contact B = new Contact("1235", "Hugh Jkauk", "8558833667", "Hugh4@gmail.com");
            Contact C = new Contact("1236", "Walter White", "1234567890", "WalterWhite8@gmail.com");
            Contact D = new Contact("1237", "Gus Fring", "6969669696", "GusFring4@gmail.com");
            Contact E = new Contact("1238", "Mike Ehrmentraut", "8558860600", "Mike@gmail.com");

            List<Contact> contactList = new List<Contact>();
            contactList.Add(A);
            contactList.Add(B);
            contactList.Add(C);
            contactList.Add(D);
            contactList.Add(E);

            return contactList;
        }
           
        static void Main(string[] args)
        {
            List<Contact> contactList = FillList();
            Executor(contactList);
            Console.ReadKey();
        }

        static void Executor(List<Contact> contactList)
        {
            bool appState = true;
            Console.WriteLine("\nWelcome user! How can we assist you today? ");

            while (appState)
            {
                Console.WriteLine("\nPress 1: Add new contact \nPress 2: View all contacts \nPress 3: Search contact \nPress 4: Delete contact \nPress 5: Exit\n");
                Console.WriteLine("|---------------------------------------------------------------------------------------------------------|");
                char option = Console.ReadKey().KeyChar;

                switch (option)
                {
                    case '1':
                        AddContact(contactList);
                        break;
                    case '2':
                        ViewContacts(contactList);
                        break;
                    case '3':
                        SearchContact(contactList);
                        break;
                    case '4':
                        DeleteContact(contactList);
                        break;
                    case '5':
                        appState = false;
                        Console.WriteLine("\nBYE BYE! :)\n");
                        break;
                    default:
                        Console.WriteLine("\nAttention! Kindly choose from above options only.");
                        break;
                }
            }
        }

        static void AddContact(List<Contact> contactList)
        {
            string namePattern = @"^[A-Za-z]+ [A-Za-z]+$";
            string phonePattern = @"^\d{10}$";
            string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";

            Console.WriteLine("\nName (full name with 1 space in-between): ");
            string name = Console.ReadLine();
            Helper.RegexValidator(ref name, namePattern, "name");
            Helper.duplicateValidator(ref name, contactList, "name");

            Console.WriteLine("\nPhone number (10-digit numeric): ");
            string phone = Console.ReadLine();
            Helper.RegexValidator(ref phone, phonePattern, "phone number");
            Helper.duplicateValidator(ref phone, contactList, "phone number");

            Console.WriteLine("\nEmail (Ex: example@email.com): ");
            string email = Console.ReadLine();
            Helper.RegexValidator(ref email, emailPattern, "email ID");
            Helper.duplicateValidator(ref email, contactList, "email ID");

            string ID = Helper.IDParser(contactList[contactList.Count - 1].ID);

            Contact con = new Contact(ID, name, phone, email);

            contactList.Add(con);

            Console.WriteLine("\nUser added successfully with an Auto-generated ID: " + ID);
            Console.WriteLine("|---------------------------------------------------------------------------------------------------------|");
        }

        static void ViewContacts(List<Contact> contactList)
        {
            for(int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine("\nContact ID: " + contactList[i].ID);
                Console.WriteLine("Name: " + contactList[i].name);
                Console.WriteLine("Phone: " + contactList[i].phone);
                Console.WriteLine("Email: " + contactList[i].email + "\n");
            }

            Console.WriteLine("Total contacts stored: " + contactList.Count +"\n");
            Console.WriteLine("|---------------------------------------------------------------------------------------------------------|");
        }

        static void SearchContact(List<Contact> contactList)
        {
            bool functionState = true;

            while (functionState)
            {
                Console.WriteLine("\nSearch by: \n\nPress 1: name \nPress 2: email \nPress 3: Main Menu\n");
                Console.WriteLine("|---------------------------------------------------------------------------------------------------------|");
                char option = Console.ReadLine()[0];

                switch (option)
                {
                    case '1':
                        Console.WriteLine("\nName: ");
                        Searcher(contactList, option);
                        break;
                    case '2':
                        Console.WriteLine("\nEmail: ");
                        Searcher(contactList, option);
                        break;
                    case '3':
                        functionState = false;
                        break;
                    default:
                        Console.WriteLine("\nAttention! Kindly choose from above options only.");
                        break;
                }
            }
        }

        static void Searcher(List<Contact> contactList, char option)
        {
            string input = Console.ReadLine();

            for(int i = 0; i < contactList.Count; i++)
            {
                if(option == '1' && contactList[i].name == input)
                {
                    Console.WriteLine("\nContact ID: " + contactList[i].ID);
                    Console.WriteLine("Name: " + contactList[i].name);
                    Console.WriteLine("Phone: " + contactList[i].phone);
                    Console.WriteLine("Email: " + contactList[i].email);
                    Console.WriteLine("\n|---------------------------------------------------------------------------------------------------------|");
                    return;
                }
                if (option == '2' && contactList[i].email == input)
                {
                    Console.WriteLine("\nContact ID: " + contactList[i].ID);
                    Console.WriteLine("Name: " + contactList[i].name);
                    Console.WriteLine("Phone: " + contactList[i].phone);
                    Console.WriteLine("Email: " + contactList[i].email);
                    Console.WriteLine("\n|---------------------------------------------------------------------------------------------------------|");
                    return;
                }
            }

            Console.WriteLine("\nUser not found! Try again");
        }

        static void DeleteContact(List<Contact> contactList)
        {
            bool functionState = true;

            while (functionState)
            {
                Console.WriteLine("\nDelete by: \n\nPress 1: name \nPress 2: email \nPress 3: Main Menu\n");
                Console.WriteLine("|---------------------------------------------------------------------------------------------------------|");
                char option = Console.ReadLine()[0];

                switch (option)
                {
                    case '1':
                        Console.WriteLine("\nName: ");
                        Deleter(contactList, option);
                        break;
                    case '2':
                        Console.WriteLine("\nEmail: ");
                        Deleter(contactList, option);
                        break;
                    case '3':
                        functionState = false;
                        break;
                    default:
                        Console.WriteLine("\nAttention! Kindly choose from above options only.");
                        break;
                }
            }
        }

        static void Deleter(List<Contact> contactList, char option)
        {
            string input = Console.ReadLine();

            for (int i = 0; i < contactList.Count; i++)
            {
                if (option == '1' && contactList[i].name == input)
                {
                    contactList.Remove(contactList[i]);
                    Console.WriteLine("\nUser removed successfully!");
                    Console.WriteLine("\n|---------------------------------------------------------------------------------------------------------|");
                    return;
                }
                if (option == '2' && contactList[i].email == input)
                {
                    contactList.Remove(contactList[i]);
                    Console.WriteLine("\nUser removed successfully!");
                    Console.WriteLine("\n|---------------------------------------------------------------------------------------------------------|");
                    return;
                }
            }

            Console.WriteLine("\nUser not found! Try again");
        }
    }
}
