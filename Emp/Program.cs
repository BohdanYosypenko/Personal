using System;
using System.Text;

namespace Personal
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Biznes<Emploee> biznes = new Biznes<Emploee>("BizBiz");
            bool alive = true;

            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Додати працівника \n2. Редагувати працівника \n3. Показати працівників \n4. Видалити працівника \n5. Завершити роботу");
                Console.ForegroundColor = color;
                try
                {
                    
                    int number = Convert.ToInt32(Console.ReadLine());

                    switch (number)
                    {
                        case 1:

                            AddEmploee(biznes);
                            break;
                        case 2:
                            EditEmploee(biznes);
                            break;
                        case 3:
                            ShowEmploee(biznes);
                            break;
                        case 4:
                            DeleteEmploee(biznes);
                            break;
                        case 5:
                            alive = false;
                            break;


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                }
            }


        }

        private static void EditEmploee(Biznes<Emploee> biznes)
        {
            Console.WriteLine("Введіть Id працівника, якого потрібно редагувати");
            int id = Convert.ToInt32(Console.ReadLine());

            biznes.Edit(id);
        }
        private static void ShowEmploee(Biznes<Emploee> biznes)
        {
            biznes.ShowAll(biznes.emploees);
            bool alive = true;
            while (alive)
            {
                Console.WriteLine("1. Пошук працівника \n2. Повернутися до головного меню");
                string change = Console.ReadLine();
                if (change == "1")
                {
                    

                    Console.WriteLine("Оберіть параметр за яким хочете знайти працівника");
                    Console.WriteLine("1. Ім'я\n" +
                    "2. Фамілія\n" +
                    "3. Дата народження\n" +
                    "4. Статус\n" +
                    "5. Департамент\n" +
                    "6. номер кімнати\n" +
                    "7. Зарплата\n" );
                    string parametr = Console.ReadLine();

                    biznes.ShowAll(biznes.ShowEmploees(parametr));

                }
                else
                {
                    Console.WriteLine("Повернення до меню");
                    alive = false;
                }
            }
       
              
        }

        private static void AddEmploee(Biznes<Emploee> biznes)
        {
            biznes.Add(AddEmploeeHendler, DeleteEmploeeHendler, EditEmploeeHendler, ShowEmploeeHendler);
        }

        private static void DeleteEmploee(Biznes<Emploee> biznes)
        {
           
            Console.WriteLine("Введіть Id працівника, якого потрібно видалити");
            int id = Convert.ToInt32(Console.ReadLine());

            biznes.Delete(id);

        }

       
        private static void AddEmploeeHendler(object sender, EmploeeEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void DeleteEmploeeHendler(object sender, EmploeeEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void EditEmploeeHendler(object sender, EmploeeEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void ShowEmploeeHendler(object sender, EmploeeEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
