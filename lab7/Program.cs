using System;
using static System.Console;
using System.Collections.Generic;

namespace Lab06
{
     class Program
    {
        static string[] months = { "січня", "лютого", "березня", "квітня", "травня", "червня", "липня", "серпня", "вересня", "жовтня", "листопада", "грудня" };
        struct Date
        {
            public int year { get; set; }
            public string month { get; set; }
            public int day { get; set; }
            public Date(int day, string month,int year)
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
            public override string ToString()=>day+" "+month+" "+year;
        }
        struct logins
        {
            public string login { get; set; }
            public string email { get; set; }
            public Date dateOfBirth { get; set; }
            public logins(string login, string email, Date dateOfBirth)
            {
                this.login = login;
                this.email = email;
                this.dateOfBirth = dateOfBirth;
            }
        }
        struct ageHashTable
        {
            private List<string>[] usersAndAge;
            public ageHashTable()
            {
                usersAndAge = new List<string>[4];
                for (int i = 0; i < usersAndAge.Length; i++)
                    usersAndAge[i] = new List<string>();
            }
            public void addNewUser(string nickname, int age)=> usersAndAge[f(age)].Add(nickname);
            public void deleteUser(string nickname, int age)=> usersAndAge[f(age)].Remove(nickname);
            private int f(int age)
            {
                int index=0;
                if (age - 25 <= 0)
                    index = 0;
                else if (age - 35 <= 0)
                    index = 1;
                else if (age - 50 <= 0)
                    index = 2;
                else
                    index = 3;
                return index;
            }
            public void Show()
            {
                string[] groupNames = new string[]{ "18-25:", "\n26-35:", "\n36-50:", "\n50+:" };
                for(int i =0; i < groupNames.Length; i++)
                {
                    WriteLine(groupNames[i]);
                    BackgroundColor = ConsoleColor.White;
                    for (int j = 0; j < Math.Round(usersAndAge[i].Count * 10.0 / (usersAndAge[0].Count + usersAndAge[1].Count + usersAndAge[2].Count + usersAndAge[3].Count)); j++)
                        Write("|");
                    BackgroundColor = ConsoleColor.Black;
                    CursorLeft = 10;
                    WriteLine($"-{Math.Round(usersAndAge[i].Count * 100.0 / (usersAndAge[0].Count + usersAndAge[1].Count + usersAndAge[2].Count + usersAndAge[3].Count), 1)}%");
                    WriteLine("Users: " + String.Join(", ", usersAndAge[i]));
                }
            }
        }
        struct hashTable
        {
            private string[] nicknames;
            private logins[] values;
            private string[] months;
            public int usersCount;
            public hashTable(int length, string[] months)
            {
                nicknames = new string[length];
                values = new logins[length];
                this.months = months;
                usersCount = 0;
            }                                                                                                            
            public bool addNewUser(string pass,string nickname)
            {
                int h = f(pass);
                int i = 1;
                while(nicknames[h] != null && nicknames[h] != nickname)
                {
                    h=(h+2*i-1)% nicknames.Length;
                    i++;
                }
                int h1 = f(nickname);
                i = 1;
                while (values[h1].login != nickname)
                {
                    if (values[h1].login == null)
                    {
                        nicknames[h] = nickname;
                        return true;
                    }
                    h1 = (h1 + 2 * i - 1) % values.Length;
                    i++;
                }
                WriteLine("Цей користувач вже зареєстрований");
                return false;
            }
            public void newUserInformation(string nickname, string email, Date dateOfBirth)
            {
                
                usersCount++;
                if(usersCount >= nicknames.Length/2)
                    reHashing();
                int h = f(nickname);
                int i = 1;
                while (values[h].email != null)
                {
                    h = (h + 2 * i - 1) % values.Length;
                    i++;
                }
                values[h] = new logins(nickname, email, dateOfBirth);
                Clear();
                Show(h);
            }
            private int f(string x)
            {
                int index = 0;
                for(int i = 0; i < x.Length; i++)
                {
                    index +=((int)x[i]-31)*(int)Math.Pow(27,x.Length-i-1);
                    index = index % nicknames.Length;
                }
                return index;
            }
            private void reHashing()
            {
                WriteLine("Фактор завантаженості досяг 50%. Відбувається перегешування.\n" +
                          "Натисніть клавішу, щоб продовжити");
                nicknames = new string[nicknames.Length*2];
                values = new logins[values.Length*2];
                ReadKey();
            }
            public void tryToLogin(string pass, string nickname)
            {
                int h = f(pass);
                int i = 1;
                while (nicknames[h]!=nickname)
                {
                    if (nicknames[h] == null)
                    {
                        WriteLine("Не правильний логін або пароль");
                        return;
                    }
                    h = (h + 2 * i - 1) % nicknames.Length;
                    i++;
                }
                h = f(nickname);
                i = 1;
                while (values[h].login != nickname)
                {
                    h = (h + 2 * i - 1) % values.Length;
                    i++;
                }
                Clear();
                Show(h);
            } 
            public void tryToDelete(string pass, string nickname,ageHashTable ageHash)
            {
                int h = f(pass);
                int i = 1;
                while (nicknames[h] != nickname)
                {
                    if (nicknames[h] == null)
                    {
                        WriteLine("Не правильний логін або пароль");
                        return;
                    }
                    h = (h + 2 * i - 1) % nicknames.Length;
                    i++;
                }
                nicknames[h] = null; 
                h = f(nickname);
                i = 1;
                while (values[h].login != nickname)
                {
                    h = (h + 2 * i - 1) % values.Length;
                    i++;
                }
                ageHash.deleteUser(nickname, CalcYear(values[h].dateOfBirth));
                values[h] = new logins();
                WriteLine($"Користувач за нікнеймом '{nickname}' був успішно видалений");
                usersCount--;
            }
            public void incorrectAge(string pass, string nickname)
            {
                int h = f(pass);
                int i = 1;
                while (nicknames[h] != nickname)
                {
                    if (nicknames[h] == null)
                        return;
                    h = (h + 2 * i - 1) % nicknames.Length;
                    i++;
                }
                nicknames[h] = null;
            }
            public void Show(int index)
            {
                int years = CalcYear(values[index].dateOfBirth);
                WriteLine("Інформація про користувача під логіном '" + values[index].login + "':\n" +
                    "E-mail: " + values[index].email + "\n" +
                    "Дата народження: "+values[index].dateOfBirth.day+" "+ values[index].dateOfBirth.month + " "+ values[index].dateOfBirth.year+" (Зараз "+years+" років)");
            }
            public void showEverything()
            {
                int num = 1;
                for (int i =0; i<values.Length; i++)
                    if(values[i].login != null)
                    {
                        WriteLine($"{num})Логін: {values[i].login},\n" +
                                  $"Email: {values[i].email},\n" +
                                  $"Дата народження: {values[i].dateOfBirth.ToString()};\n");
                        num++;
                    }
            }
            public int CalcYear(Date dateOfBirth)
            {
                int years = DateTime.Today.Year - dateOfBirth.year - 1;
                int month = 1;
                for(int i = 0; i < months.Length ; i++)
                {
                    if(months[i] == dateOfBirth.month)
                    {
                        month= i+1;
                        break;
                    }
                }
                if (month < DateTime.Now.Month ||(month == DateTime.Now.Month && dateOfBirth.day <= DateTime.Today.Day))
                    years++;
                return years;
            } 
        }
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            hashTable table = new hashTable(100, months);
            ageHashTable ageHash = new ageHashTable();
            string login, pass, email, date;
            Date dateOfBirth;
        a: WriteLine("Добрий день. Визначте який спосіб вам потрібен:\n" +
                     "1)З контрольним прикладом;\n" +
                     "2)З нуля;");
            switch (ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Clear();
                    Random rnd = new Random();
                    string newEmail="";
                    Date dateBirth;
                    string text = "Додамо 10 користувачів:\n" +
                              "1)login:Alex     password:Zavarzin\n" +
                              "2)login:Stuff     password:Maude\n" +
                              "3)login:keramzit     password:lakiLay\n" +
                              "4)login:Godzilla     password:DIFOR\n" +
                              "5)login:raMzes     password:Maude\n" +
                              "6)login:L0ki     password:Waldo\n" +
                              "7)login:believer     password:dRaGon\n" +
                              "8)login:allmall     password:Romra\n" +
                              "9)login:T0rM@Z     password:dergun\n" +
                              "10)login:White_Fox     password:kaban";
                    for(int i = 1; i < 11; i++)
                    {
                        table.addNewUser(text.Split("\n")[i].Split("     ")[1].Split(":")[1], text.Split("\n")[i].Split("     ")[0].Split(":")[1]);
                        for(int j = 0; j < rnd.Next(1,10); j++)
                        {
                            newEmail+=(char)rnd.Next(97,122);
                        }
                        newEmail += "@gmail.com";
                        dateBirth = new Date(rnd.Next(1, 28), months[rnd.Next(12)], rnd.Next(1960, 2003));
                        table.newUserInformation(text.Split("\n")[i].Split("     ")[0].Split(":")[1], newEmail,dateBirth);
                        ageHash.addNewUser(text.Split("\n")[i].Split("     ")[0].Split(":")[1], table.CalcYear(dateBirth));
                        newEmail = "";
                    }
                    Clear();
                    WriteLine(text);
                    WriteLine("Натисніть клавішу, щоб продовжити...");
                    ReadKey();
                    break;
                case ConsoleKey.D2:
                    Clear();
                    break;
                default:
                    Clear();
                    goto a;
            }
            while (true)
            {
                Clear();
                WriteLine($"Меню (Зараз в системі {table.usersCount} користувачів)\n" +
                          "1)Зареєструватися;\n" +
                          "2)Залогінитися;\n" +
                          "3)Видалити користувача");
                if (table.usersCount>0)
                {
                    WriteLine("4)Подивитися вікову статистику\n" +
                              "5)Подивитися інформацію про зареєстрованих користувачів");
                }
                switch (ReadKey().Key)
                {
                    case ConsoleKey.D1:
                    b: Clear();
                        Write("Ви обрали функцію зареєструватися.\nВведіть логін: ");
                        login = ReadLine();
                        if (login == null || login == "") goto b;
                    c: Write("Введіть пароль: ");
                        pass = ReadLine();
                        if (pass == null || pass == "")
                        {
                            Clear();
                            WriteLine("Ви обрали функцію зареєструватися.\nВведіть логін: " + login); goto c;
                        }
                        if(table.addNewUser(pass, login))
                        {
                       f:  Clear();
                            WriteLine("Введіть, будь ласка, персональну інформацію:");
                            Write("E-mail: ");email = ReadLine();
                            if (email == null||email=="") goto f;
                       g:   try
                            {
                                int index=-1;
                                Write("Дата народження (день місяць рік): ");
                                date = ReadLine();
                                for(int i =0; i < 12; i++)
                                {
                                    if (date.Split(" ")[1].ToLower() == months[i])
                                    {
                                        index =i;
                                        break;
                                    }                                        
                                }
                                dateOfBirth = new Date(Convert.ToInt32(date.Split(" ")[0]), months[index], Convert.ToInt32(date.Split(" ")[2]));
                            }
                            catch
                            {
                                Clear();
                                WriteLine("Введіть, будь ласка, персональну інформацію:");
                                WriteLine("E-mail: "+email);
                                goto g;
                            }
                            if (table.CalcYear(dateOfBirth) >= 18)
                            {
                                table.newUserInformation(login, email, dateOfBirth);
                                ageHash.addNewUser(login, table.CalcYear(dateOfBirth));
                            }
                            else
                            {
                                WriteLine("Користувачам, яким менше ніж 18, заборонена реєстрація");
                                table.incorrectAge(pass, login);
                            }
                            
                        }
                        WriteLine("\n\nНатисніть будь-яку клавішу, щоб повернутися...");
                        ReadKey();
                        break;
                    case ConsoleKey.D2:
                     d: Clear();
                        Write("Ви обрали функцію залогінитися.\nВведіть логін: ");
                        login = ReadLine();
                        if (login == null || login == "") goto d;
                     e: Write("Введіть пароль: ");
                        pass = ReadLine();
                        if (pass == null || login == "") {
                            Clear();
                            WriteLine("Ви обрали функцію залогінитися.\nВведіть логін: "+login); goto e; }
                        table.tryToLogin(pass, login);
                        WriteLine("\n\nНатисніть будь-яку клавішу, щоб повернутися...");
                        ReadKey();
                        break;
                    case ConsoleKey.D3:
                    x: Clear();
                        Write("Ви обрали функцію видалити користувача.\nВведіть логін: ");
                        login = ReadLine();
                        if (login == null || login == "") goto x;
                    n: Write("Введіть пароль: ");
                        pass = ReadLine();
                        if (pass == null || login == "")
                        {
                            Clear();
                            WriteLine("Ви обрали функцію видалити користувача.\nВведіть логін: " + login); goto n;
                        }
                        table.tryToDelete(pass, login, ageHash);
                        WriteLine("\n\nНатисніть будь-яку клавішу, щоб повернутися...");
                        ReadKey();
                        break;
                    case ConsoleKey.D4:
                        if (table.usersCount > 0)
                        {
                            Clear();
                            WriteLine("Кількість користувачів за віковими групами");
                            ageHash.Show();
                            WriteLine("\n\nНатисніть будь-яку клавішу, щоб повернутися...");
                            ReadKey();
                        }
                        break;
                    case ConsoleKey.D5:
                        if (table.usersCount > 0)
                        {
                            Clear();
                            WriteLine("Зареєстровані користувачі:");
                            table.showEverything();
                            WriteLine("\n\nНатисніть будь-яку клавішу, щоб повернутися...");
                            ReadKey();
                        }
                        break;
                }
            }        
        }
    }
}