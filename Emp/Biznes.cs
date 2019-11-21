using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Linq;

namespace Personal
{
    public class Biznes<T> where T : Emploee
    {


        public T[] emploees;
        public string Name { get; private set; }

        public Biznes(string name)
        {
            this.Name = name;
        }

        static int count = 0;

        T newEmploee = null;

        string path = @"D:\C#\Main";
        public void Add(EmploeeStateHandler addStateHandler, EmploeeStateHandler deleteStateHendler, EmploeeStateHandler editStateHandler, EmploeeStateHandler showstateHandler)
        {

            
            newEmploee = new Emploee() as T;
            
            
                Console.WriteLine("Вкажіть ім'я працівниика");
                newEmploee.FirstName = Console.ReadLine();
                Console.WriteLine("Вкажіть прізвище працівниика");
                newEmploee.SecondName = Console.ReadLine();
                Console.WriteLine("Вкажіть дату народження працівниика");
                newEmploee.DateOfBirth = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Вкажіть посаду працівниика");
                newEmploee.Status = Console.ReadLine();
                Console.WriteLine("Вкажіть відділ працівниика");
                newEmploee.Department = Console.ReadLine();
                Console.WriteLine("Вкажіть номер кімнати працівниика");
                newEmploee.RoomNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Вкажіть телефон працівниика");
                newEmploee.Phone = Console.ReadLine();
                Console.WriteLine("Вкажіть імейл працівниика");
                newEmploee.Email = Console.ReadLine();
                Console.WriteLine("Вкажіть зарплату працівниика");
                newEmploee.Selary = Convert.ToInt32( Console.ReadLine());
                Console.WriteLine("Нотатки");
                newEmploee.Notes = Console.ReadLine();
            newEmploee.Id = ++count;


            if (newEmploee == null)
                throw new Exception("Помилка створення рахунку");
            if (emploees == null)
                emploees = new T[] { newEmploee };
            else
            {
                T[] tempEmploees = new T[emploees.Length + 1];
                for (int i = 0; i < emploees.Length; i++)
                    tempEmploees[i] = emploees[i];

                tempEmploees[tempEmploees.Length - 1] = newEmploee;
                emploees = tempEmploees;

            }
            newEmploee.Added += addStateHandler;
            newEmploee.Deleted += deleteStateHendler;
            newEmploee.Edited += editStateHandler;
            newEmploee.ShowedEmploee += showstateHandler;

            newEmploee.Add();
            WriteIntoFile();



        }

        internal void ShowAll(object emploees)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            int index;
            T emploee = FindEmploee(id, out index);
            if (emploee == null)
                throw new Exception("Рахунок не знайдено");

            emploee.Delete();
            if (emploees.Length <= 1)
                emploees = null;
            else
            {
                T[] tempEmploees = new T[emploees.Length - 1];
                for (int i = 0, j = 0; i < emploees.Length; i++)
                {
                    if (i != index)
                        tempEmploees[j++] = emploees[i];
                }
                emploees = tempEmploees;
                foreach (T e in emploees)
                {
                    if (e.Id > emploee.Id)
                        --e.Id;
                }
            }
        }
        public void Edit(int id)
        {
            

            T emploee = FindEmploee(id);
            if (emploee == null)
                throw new Exception("Рахунок не знайдено");
            Console.WriteLine("Виберіть номер інформацї яку потрібно змінити");
            Console.WriteLine("1. Ім'я\n" +
                "2. Фамілія\n" +
                "3. Дата народження\n" +
                "4. Статус\n" +
                "5. Департамент\n" +
                "6. номер кімнати\n" +
                "7. Телефон\n" +
                "8. Емейл\n" +
                "9. Зарплата\n" +
                "10. Нотатки\n");
            int number = Convert.ToInt32(Console.ReadLine());
            try
            {

                switch (number)
                {
                    case 1:
                        Console.WriteLine("Вкажіть нове Ім'я");
                        emploee.FirstName= Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Вкажіть нову фамілію");
                        emploee.SecondName = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Вкажіть нову дату народження");
                        emploee.DateOfBirth =DateTime.Parse( Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Вкажіть новий статус");
                        emploee.Status = Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Вкажіть новий департамент");
                        emploee.Department = Console.ReadLine();
                        break;
                    case 6:
                        Console.WriteLine("Вкажіть новий номер кімнати");
                        emploee.RoomNumber =Convert.ToInt32( Console.ReadLine());
                        break;
                    case 7:
                        Console.WriteLine("Вкажіть нове Ім'я");
                        emploee.Phone = Console.ReadLine();
                        break;
                    case 8:
                        Console.WriteLine("Вкажіть нову Електронну пошту");
                        emploee.Email = Console.ReadLine();
                        break;
                    case 9:
                        Console.WriteLine("Вкажіть нову зарплатню");
                        emploee.Selary = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 10:
                        Console.WriteLine("Вкажіть нову нотатку");
                        emploee.Notes = Console.ReadLine();
                        break;
                }
                emploee.Edit();
            }
            catch 
            {
                throw new Exception("Невірно вказано номер");
            }

        }

        public void Show(int id)
        {
            T emploee = FindEmploee(id);
            if (emploee == null)
                throw new Exception("Рахунок не знайдено");
            emploee.Show();
            GetFromFile();

        }

        public T FindEmploee(int id)
        {


            for (int i = 0; i < emploees.Length; i++)
            {
                if (emploees[i].Id == id)
                    return emploees[i];

            }
            return null;
        }


        public T FindEmploee(int id, out int index)
        {
            for (int i = 0; i < emploees.Length; i++)
            {
                if (emploees[i].Id == id)
                {
                    index = i;
                    return emploees[i];
                }
            }
            index = -1;
            return null;
        }


        public void WriteIntoFile()
        {

            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();

            }

            using (FileStream fileStream = new FileStream($@"{path}\note.txt", FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(newEmploee.ToString());
                fileStream.Write(array, 0, array.Length);
                Console.WriteLine("працівника записано в файл");
            }



        }

        public void GetFromFile()
        {
            using (FileStream fs = File.OpenRead($@"{path}\note.txt"))
            {

                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                string[] separator = new string[] { "; " };
                string[] emploeeData = textFromFile.Split(separator, StringSplitOptions.None);


            }
        }

        public void ShowAll(Emploee[] emploees)
        {
            int n, s, dofb, stat, dep, numb, phon, mail, many, note;
            n = s = dofb = stat = dep = numb = phon = mail = many = note = 0;
            

            foreach (Emploee e in emploees)
            {
                if (e.FirstName.Length > n)
                {
                    n = e.FirstName.Length;
                }
                if (e.SecondName.Length > s)
                {
                    s = e.SecondName.Length;
                }
                if ( e.DateOfBirth.ToShortDateString().Length > dofb)
                {
                    dofb =e.DateOfBirth.ToShortDateString().Length;
                }
                if (e.Status.Length > stat)
                {
                    stat = e.Status.Length;
                }
                if (e.Department.Length > dep)
                {
                    dep = e.Department.Length;
                }
                
                if (Convert.ToString( e.RoomNumber).Length > numb)
                {
                    numb = Convert.ToString(e.RoomNumber).Length;
                }
                if (e.Phone.Length > phon)
                {
                    phon = e.Phone.Length;
                }
                if (e.Email.Length > mail)
                {
                    mail = e.Email.Length;
                }
                if (Convert.ToString( e.Selary).Length > many)
                {
                    many = Convert.ToString(e.Selary).Length;
                }
                if (e.Notes.Length > note)
                {
                    note = e.Notes.Length;
                }


            }
            
            Console.WriteLine(new String('-', 4) + new String('-', n + 4) + new String('-', s + 4) + new String('-', dofb + 4) + new String('-', stat + 4) + new String('-', dep + 4)
                    + new String('-', numb + 4) + new String('-', phon + 4) + new String('-', mail + 4) + new String('-', many + 4) + new String('-', note + 4));


            foreach (Emploee e in emploees)
            {
                string space = new String(' ', n - e.FirstName.Length + 2);
                string spaces = new String(' ', s - e.SecondName.Length + 2);
                string spacedofb = new String(' ', dofb - e.DateOfBirth.ToShortDateString().Length + 2);
                string statSpaces = new String(' ', stat - e.Status.Length + 2);
                string depSpaces = new String(' ', dep - e.Department.Length + 2);
                string numbSpaces = new String(' ', numb - Convert.ToString( e.RoomNumber).Length + 2);
                string phonSpaces = new String(' ', phon - e.Phone.Length + 2);
                string mailSpaces = new String(' ', mail - e.Email.Length + 2);
                string manySpaces = new String(' ', many - Convert.ToString(e.Selary).Length + 2);
                string noteSpaces = new String(' ', note - e.Notes.Length + 2);





                Console.WriteLine("|" + e.Id + " |" + ' ' + e.FirstName + space + '|' + ' ' + e.SecondName + spaces + '|'
                   + ' ' + e.DateOfBirth.ToShortDateString() + spacedofb + '|' + ' ' + e.Status + statSpaces + '|' + ' ' + e.Department + depSpaces + '|'
                   + ' ' + e.RoomNumber + numbSpaces + '|' + ' ' + e.Phone + phonSpaces + '|' + ' ' + e.Email + mailSpaces + '|' + ' ' + e.Selary + manySpaces + '|' + ' ' + e.Notes + noteSpaces + '|');


                Console.WriteLine(new String('-', 4) + new String('-', n + 4) + new String('-', s + 4) + new String('-', dofb + 4) + new String('-', stat + 4) + new String('-', dep + 4)
                     + new String('-', numb + 4) + new String('-', phon + 4) + new String('-', mail + 4) + new String('-', many + 4) + new String('-', note + 4));

            }
        }

        public T[] ShowEmploees(string parametr)
        {
            string value;

            switch(parametr)
            {
                case "1":
                    Console.WriteLine("Введіть ім'я працівника");
                    value = Console.ReadLine();
                    var selectedEmploees = from empl in emploees
                                           where empl.FirstName.ToUpper().StartsWith(value.ToUpper())
                                           select empl;

                    if (selectedEmploees.Count() > 0)
                        return selectedEmploees.ToArray();
                    else
                    {
                        Console.WriteLine("Працівника з даним ім'ям не знайдено");
                        return selectedEmploees.ToArray();
                    }
                    break;

                case "2":
                    Console.WriteLine("Введіть фамілію працівника");
                    value = Console.ReadLine();
                    selectedEmploees = from empl in emploees
                                           where empl.SecondName.ToUpper().StartsWith(value.ToUpper())
                                           select empl;

                    if (selectedEmploees.Count() > 0)
                        return selectedEmploees.ToArray();
                    else
                    {
                        Console.WriteLine("Працівника з даною фамілією не знайдено");
                        return selectedEmploees.ToArray();
                    }
                    break;
                case "3":
                    Console.WriteLine("Введіть вік працівника");
                    value = Console.ReadLine();
                    selectedEmploees = from empl in emploees
                                       where (DateTime.Now.Year- empl.DateOfBirth.Year) == Convert.ToInt32( value)
                                       select empl;

                    if (selectedEmploees.Count() > 0)
                        return selectedEmploees.ToArray();
                    else
                    {
                        Console.WriteLine($"Працівника віком {value} не знайдено");
                        return selectedEmploees.ToArray();
                    }
                    break;
                case "4":
                    Console.WriteLine("Введіть посаду на якій працює даний працівник");
                    value = Console.ReadLine();
                    selectedEmploees = from empl in emploees
                                       where empl.Status.ToUpper().StartsWith(value.ToUpper())
                                       select empl;

                    if (selectedEmploees.Count() > 0)
                        return selectedEmploees.ToArray();
                    else
                    {
                        Console.WriteLine("Працівника з даною посадою не знайдено");
                        return selectedEmploees.ToArray();
                    }
                    break;

                case "5":
                    Console.WriteLine("Введіть відділ у якому працює даний працівник");
                    value = Console.ReadLine();
                    selectedEmploees = from empl in emploees
                                       where empl.Department.ToUpper().StartsWith(value.ToUpper())
                                       select empl;

                    if (selectedEmploees.Count() > 0)
                        return selectedEmploees.ToArray();
                    else
                    {
                        Console.WriteLine("Працівника не знайдено");
                        return selectedEmploees.ToArray();
                    }
                    break;

                case "6":
                    Console.WriteLine("Введіть номер кімнати у якому знаходиться працівник");
                    value = Console.ReadLine();
                    selectedEmploees = from empl in emploees
                                       where empl.RoomNumber== Convert.ToInt32(value)
                                       select empl;

                    if (selectedEmploees.Count() > 0)
                        return selectedEmploees.ToArray();
                    else
                    {
                        Console.WriteLine("Працівника не знайдено");
                        return selectedEmploees.ToArray();
                    }
                    break;

                    int minValue, maxValue;
                case "7":
                    Console.WriteLine("Введіть діапазон пшуку зарплати");
                    Console.Write("Мінімальне значення - ");
                    minValue = Convert.ToInt32 (Console.ReadLine());
                    Console.Write("Максимальне значення - ");
                    maxValue = Convert.ToInt32(Console.ReadLine());
                    selectedEmploees = from empl in emploees
                                       where Convert.ToInt32( empl.Selary)>minValue&&Convert.ToInt32(empl.Selary)<maxValue
                                       select empl;

                    if (selectedEmploees.Count() > 0)
                        return selectedEmploees.ToArray();
                    else
                    {
                        Console.WriteLine("Працівника не знайдено");
                        return selectedEmploees.ToArray();
                    }
                    break;

                default:
                    return null;
                    break;


            }

            
            

        }
    }
}
