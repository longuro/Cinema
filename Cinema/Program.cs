using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema
{
    class Program
    {
        static int CinemaSizeChoose()//Выбор размера зала
        {
            Console.WriteLine("Выберите зал: 1, 2, 3");//Пользователь выбирает размер зала
            int size = int.Parse(Console.ReadLine());//Создается целочисленная переменная size, в которую заносится число, введенное с консоли.
            return size;
        }
        static int[,] hall = new int[,] { };//Создаётся двумерный массив, для дальнейнего заполнения, либо 0 или 1.
        static int seat, row;//Хранение мест и рядов с помощью int
        static void HallSize(int size)//Функция для дальнейшего создания зала
        {
            Random rnd = new Random();//Импорт рандома для генерации мест в зале
            if (size == 1)
            {
                seat = 20;
                row = 9;
            }
            else if (size == 2)
            {
                seat = 20;
                row = 19;
            }
            else
            {
                seat = 30;
                row = 19;
            }
            hall = new int[row, seat];//Ввод значений в массив
            for (int roww = 0; roww < row; roww++)//Цикл для последовательной генерации рядов
            {
                for (int seatt = 0; seatt < seat; seatt++)//Цикл для последовательной генерации мест
                {
                    hall[roww, seatt] = rnd.Next(0, 2);//Рандомная генерация места, занято или свободно
                }
            }
        }

        static bool Person()//Функция для выбора пользователем места
        {
            Console.WriteLine("Ряд");
            int ticket_roww = int.Parse(Console.ReadLine());//Считывает ряд, введённый пользователем в переменную
            Console.WriteLine("Сиденье");
            int ticket_seatt = int.Parse(Console.ReadLine());//Считывает место, введённый пользователем в переменную
            if (hall[ticket_roww - 1, ticket_seatt - 1] == 0)//Если место свободно
            {
                hall[ticket_roww - 1, ticket_seatt - 1] = 1;//Переписывает значение выбранного места, чтобы значение стало "1" и считалось как занятым
                Console.WriteLine("");
                return true;
            }
            else//Если на данном месте значение "1"
            {
                Console.WriteLine("Это место занято");
                return false;
            }
        }

        static void HallSize()//Рисование зала
        {
            Console.Write("      ");
            for (int seatt = 0; seatt < seat; seatt++)//Цикл для перечисления номеров мест
            {
                if (seatt.ToString().Length == 2)//Если номер места двузначный
                {
                    Console.Write(" {0}", seatt + 1); //Уменьшается отступ до одного пробела и увеличение номера места на 1
                }
                else
                {
                    Console.Write("  {0}", seatt + 1);//Отступ в два пробела и увеличение номера места на 1
                }
            }
            Console.WriteLine("\n");//Новая строка
            for (int roww = 0; roww < row; roww++)//Цикл для  перечисления рядов
            {
                Console.Write($"{roww + 1} Ряд:  ");//Пишет номер ряда
                for (int seatt = 0; seatt < seat; seatt++)//Цикл для вписывания места в каждый ряд под каждым номером
                {
                    Console.Write(hall[roww, seatt] + "  ");//Вписывание значения места с помощью ранее созданного двухмерного массива
                }
                Console.WriteLine("");
            }
        }

        static bool AutoTicketBuy()//Функция покупки билетов
        {
            Console.WriteLine("Ряд");
            int ticket_row = int.Parse(Console.ReadLine());//Считывает вписанный пользователем ряд и записывает в переменную
            Console.WriteLine("Количество билетов:");
            int howmuch = int.Parse(Console.ReadLine());//Считывает количество покупаемых пользователем билетов и записывает в переменную
            int seatposition = (seat / 2);//Вычисление номера места в середине ряда
            bool t = false;//Возвращает логическое значение, если возвращается true - покупаются места
            int k = 0;//Ввод переменной изначальных купленных билетов
            Console.Clear();//Очистка консоли
            while (howmuch != k)//Работа цикла, пока места не будут выбраны
            {
                if (hall[ticket_row, seatposition] == 0)//Если место в середине ряда свободно
                {
                    Console.WriteLine($"{seatposition} место свободно");
                    t = true;//Возвращает положительное значение t
                    seatposition++;//Увеличение номера места
                    k++;//Увеличение значения купленных билетов
                }
                else//Если место в середине ряда занято
                {
                    Console.WriteLine($"{seatposition} место занято");
                    t = false;//Возвращает отрицательное значение t
                    break;//Остановка функции, чтобы не повторялось бесконечно
                }
            }

            if (t == true)//Если указанное количество пользователем мест в середине свободно
            {
                for (int i = 0; i < (howmuch + 1); i++)
                {
                    int middleseat = seat / 2;
                    hall[ticket_row - 1, middleseat - 1] = 1;
                    middleseat++;
                }
                Console.WriteLine("Ваши места:");
                for (int userseat = seat / 2; userseat < (seat / 2) + howmuch; userseat++)//Цикл для перечисления купленных мест
                {
                    Console.WriteLine($"{userseat}");//Пишет купленные места
                }
            }
            else//Если указанное количество пользователем мест в середине не свободно
            {
                Console.WriteLine("В этом ряду нет свободных мест посередине, попробуйте поискать в другом ряду");
            }
            return t;//Возвращает переменную t в false
        }

        public static void Main(string[] args)
        {

            int size = CinemaSizeChoose();//Переменная с данными о выборе размера зала
            HallSize(size);//Запуск функции сгенерированного зала

            while (true)
            {
                HallSize();//Рисует зал
                Console.WriteLine();

                int userseat = 0;//Число купленных билетов
                Console.WriteLine("Выбрать самому - 1\nАвто выбор - 2\nЗакончить - 3");//Перечисление вариантов для пользователя во время покупки билетов
                int userchoice = int.Parse(Console.ReadLine());//Записывание введённого пользователем варианта покупки мест в переменную
                Console.WriteLine("");
                if (userchoice == 1)//Если пользователь выбирает 1, то срабатывает функция Person(), где в дальнейшем человек выбирает себе место
                {
                    Console.WriteLine("Количество покупаемых билетов?");
                    int quantity = int.Parse(Console.ReadLine());//Считывает и записывает значение, введённое пользователем в переменную для хранения количества покупаемых билетов
                    for (int i = 0; i < (seat - 1) * (row - 1); i++)//Цикл для покупки билетов
                    {
                        if (Person())//Запуск функции покупки билетов пользователем
                        {
                            userseat++;//Увеличение числа купленных билетов
                        }
                        if (userseat == quantity)//Если все билеты куплены и места выбраны
                        {
                            break;//Остановка функции
                        }
                    }
                    Console.Clear();//Очистка консоли

                }
                else if (userchoice == 2)//Авто выбор мест с помощью функции AutoTicketBuy
                {
                    AutoTicketBuy();//Запуск функции покупки билетов
                }
                else if (userchoice == 3)//Завершить покупку мест
                {
                    return;//Закрытие программы
                }
            }
        }
    }
}
