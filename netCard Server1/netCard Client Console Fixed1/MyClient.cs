
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Threading;
using SmartCard.Runtime.Remoting.Channels.APDU;

// make sure you add the reference to your server stub dll or interface
// The stub file is automatically generated for you, under [Server Project Output]\Stub).
using MyCompany.MyOnCardApp;
using System.IO;
using SmartCard;
using System.Text;
using netCard_Client_Console_Fixed1;

namespace MyCompany.MyClientApp
{
        public class MyClient
        {
            private const string URL = "apdu://selfdiscover/MyService.uri";


            public static void Main()
            {
                APDUClientChannel channel = new APDUClientChannel();
                ChannelServices.RegisterChannel(channel);
                MyService service = (MyService)Activator.GetObject(typeof(MyService), URL);
                
                // 1. askToInsertCardAndCheckTheCard()
                Console.WriteLine("Please insert your Smartcard...");
                SmartCard.Runtime.Remoting.Channels.ChannelCardInsertionStateChangedEventHandler handler = new SmartCard.Runtime.Remoting.Channels.ChannelCardInsertionStateChangedEventHandler(channel_ChannelCardInsertionStateChangedEvent);
                channel.RegisterCardInsertionStateChangedEventHandler(handler);

                string action = Console.ReadLine();
                callMethodToGiveQuestions(action, service, channel);
            }

            public static void callMethodToGiveQuestions(string action, MyService service, APDUClientChannel channel)
            {
                Client[] zmones;
                int numberPeople;
                string numberCard;

                // 2. readFileMethod
                readFileMethod(out zmones, out numberPeople);

                switch (action)
                {
                    case "1":
                        // 5. Show all people
                        showAllUsers(zmones);
                        Console.WriteLine("");
                        Console.WriteLine("Would you like to continue? [ Y/N ]");
                        willWeContinue(service, channel);
                        break;

                    case "2":
                        showUsersDataByName(zmones, numberPeople, service, channel);
                        Console.WriteLine("");
                        Console.WriteLine("Would you like to continue? [ Y/N ]");
                        willWeContinue(service, channel);
                        break;

                    case "3":
                        // 3. Get data of the card
                        numberCard = getSerialNumberFromCard(service);
                        // 4. Check the user
                        identifyTheUser(numberCard, zmones, numberPeople);
                        Console.WriteLine("");
                        Console.WriteLine("Would you like to continue? [ Y/N ]");
                        willWeContinue(service, channel);
                        break;

                    case "4":
                        numberCard = getSerialNumberFromCard(service);
                        signTheDocument(numberCard, zmones, numberPeople, service, channel);
                        Console.WriteLine("Done, file was signed");
                        Console.WriteLine("");
                        Console.WriteLine("Would you like to continue? [ Y/N ]");
                        willWeContinue(service, channel);
                        break;
                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Terminating...");
                        channel.Dispose();
                        ChannelServices.UnregisterChannel(channel);
                        break;
                }
            }

            public static void signTheDocument(string numberCard, Client[] zmones, int numberPeople, MyService service, APDUClientChannel channel)
            {
                Console.WriteLine("Please choose a file [1-3]");
                Console.WriteLine(" 1. text_1.txt ");
                Console.WriteLine(" 2. text_2.txt ");
                Console.WriteLine(" 3. text_3.txt ");
                string file = Console.ReadLine();
                string updateFile = "";
                switch (file)
                {
                    case "1":
                        updateFile = "text_1.txt";
                        break;
                    case "2":
                        updateFile = "text_2.txt";
                        break;
                    case "3":
                        updateFile = "text_3.txt";
                        break;
                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Terminating...");
                        channel.Dispose();
                        ChannelServices.UnregisterChannel(channel);
                        break;
                }

                for (int i = 0; i < numberPeople; i++)
                {
                    if (numberCard == zmones[i].getNumber())
                    {
                        service.CreateDir("C:\\FileToShow");
                        var txtLines = File.ReadAllLines("C:\\FileToShow\\" + updateFile);
                        using (var stream = new StreamWriter("C:\\FileToShow\\" + updateFile))
                        {
                            for (int l = 0; l < txtLines.Length; l++)
                            {
                                stream.WriteLine(txtLines[l]);
                            }
                            stream.WriteLine("Name: {0}", zmones[i].getName());
                            stream.WriteLine("Surname: {0}", zmones[i].getLastName());
                            stream.WriteLine("Responsibilities: {0}", zmones[i].getResponsibilities());
                            stream.WriteLine("Parasas: {0}", zmones[i].getSign());
                        }

                        service.PutFile("C:\\FileToShow\\" + updateFile, File.ReadAllBytes("C:\\FileToShow\\" + updateFile));
                    }
                }
            }

            public static void showUsersDataByName(Client[] zmones, int numberPeople, MyService service, APDUClientChannel channel)
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter name");
                string name = Console.ReadLine();
                int search = 0;
                for (int i = 0; i < numberPeople; i++)
                {
                    if (zmones[i].getName() == name)
                    {
                        search++;
                        Console.WriteLine("");
                        Console.WriteLine(zmones[i].toStringGetUserInfo());
                    }
                }

                if (search == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("User was not found");
                    Console.WriteLine("Would you like to continue? [ Y/N ]");
                    willWeContinue(service, channel);
                }
            }

            public static string getSerialNumberFromCard(MyService service)
            {
                string[] intNumber = new string[12];
                byte[] numberDigits = service.getSerialNumber();
                for (int i = 0; i < numberDigits.Length; i++)
                {
                    intNumber[i] = Convert.ToInt32(numberDigits[i]).ToString();
                }
                string numberCard = string.Join("-", intNumber);
                return numberCard;
            }

            public static void showAllUsers(Client[] zmones)
            {
                Console.WriteLine("");
                Console.WriteLine("Users");
                for (int i = 0; i < zmones.Length; i++)
                {
                    Console.WriteLine(zmones[i].toStringGetUserInfo());
                }
            }

            private static void identifyTheUser(string numberCard, Client[] zmones, int numberPeople)
            {
                for (int i = 0; i < numberPeople; i++)
                {
                    if (numberCard == zmones[i].getNumber())
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Card owner data");
                        Console.WriteLine(zmones[i].toStringGetUserInfo());
                    }
                }
            }
            
            private static void channel_ChannelCardInsertionStateChangedEvent(object sender, SmartCard.Runtime.Remoting.Channels.ChannelCardInsertionEventArgs e)
            {
                Client[] zmones;
                int numberPeople;
                string numberCard;
                readFileMethod(out zmones, out numberPeople);
                MyService service = (MyService)Activator.GetObject(typeof(MyService), URL);

                if (e.Inserted)
                {
                    if (APDUClientChannel.IsNetCard(e.ReaderName))
                    {
                        Console.WriteLine("Loading.");
                        Thread.Sleep(400);
                        Console.WriteLine("Loading..");
                        Thread.Sleep(400);
                        Console.WriteLine("Loading...");
                        Thread.Sleep(400);
                        Console.WriteLine("Loading....");
                        Thread.Sleep(400);
                        Console.WriteLine("");
                        Console.WriteLine("Card inserted");
                        numberCard = getSerialNumberFromCard(service);
                        // 4. Check the user
                        string name = findUserByCardReturnName(numberCard, zmones, numberPeople);
                        Console.WriteLine("");
                        Console.WriteLine("Hello ,{0}!", name);
                        Console.WriteLine("");
                        Console.WriteLine("Please select action");
                        Console.WriteLine(" Pres 1 - Check all users ");
                        Console.WriteLine(" Pres 2 - Check user by name ");
                        Console.WriteLine(" Pres 3 - Check your data ");
                        Console.WriteLine(" Pres 4 - Sign text file ");
                        Console.WriteLine(" Press anything else will be cancel ");
                        
                    }
                    else
                    {
                        Console.WriteLine("Card in the reader {0} is not a Gemalto.NET Smart card", e.ReaderName);
                    }
                }
            }

            public static string findUserByCardReturnName(string numberCard, Client[] zmones, int numberPeople)
            {
                string name = "";
                for (int i = 0; i < numberPeople; i++)
                {
                    if (numberCard == zmones[i].getNumber())
                    {
                        name = zmones[i].getName();
                    }
                }

                return name;
            }

            public static void willWeContinue(MyService service, APDUClientChannel channel)
            {
                string type = Console.ReadLine();
                switch (type)
                {
                    case "Y":
                    case "Yes":
                    case "yes":
                    case "y":
                        Console.WriteLine("");
                        Console.WriteLine("Please select action");
                        Console.WriteLine(" Pres 1 - Check all users ");
                        Console.WriteLine(" Pres 2 - Check user by name ");
                        Console.WriteLine(" Pres 3 - Check your data ");
                        Console.WriteLine(" Pres 4 - Sign text file ");
                        Console.WriteLine(" Press anything else will be cancel ");
                        string action = Console.ReadLine();
                        callMethodToGiveQuestions(action, service, channel);
                        break;
                    case "N":
                    case "No":
                    case "no":
                    case "n":
                        Console.WriteLine("");
                        Console.WriteLine("Terminating...");
                        channel.Dispose();
                        ChannelServices.UnregisterChannel(channel);
                        break;
                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Terminating...");
                        channel.Dispose();
                        ChannelServices.UnregisterChannel(channel);
                        break;
                }
            }

            public static void readFileMethod(out Client[] zmones, out int numberPeople)
            {
                zmones = new Client[2];
                numberPeople = 0;
                using (StreamReader Skaitymas = new StreamReader(@"C:\\test2\\test.txt", Encoding.GetEncoding(1257)))
                {
                    string eilute = null;
                    while (null != (eilute = Skaitymas.ReadLine()))
                    {
                        string[] reiksmes = eilute.Split(';');
                        string name = reiksmes[0];
                        string lastName = reiksmes[1];
                        string responsibility = reiksmes[2];
                        string sign = reiksmes[3];
                        string number = reiksmes[4];

                        Client zmogus = new Client(name, lastName, responsibility, sign, number);
                        zmones[numberPeople++] = zmogus;

                    }
                }
            }
        }
}

