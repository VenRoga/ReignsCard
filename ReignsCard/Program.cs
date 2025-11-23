using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1Kurs2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Reigns[] res = new Reigns[15];
                ReadFile("Cards.txt", res);
                Console.WriteLine("\n=== СОРТИРОВКА МАССИВА ===");
                SortCards(res);
                Console.WriteLine(new string('-', 80));
                Res(res);
            }
            catch (Exception ex)
            {
                Exeptionss(ex);
            }
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        public static void Exeptionss(Exception ex)
        {
            if (ex is FileNotFoundException fileEx)
            {
                Console.WriteLine($"Файл не найден: {fileEx.FileName}");
            }
            else if (ex is UnauthorizedAccessException)
            {
                Console.WriteLine("Отсутствуют права доступа к файлу.");
            }
            else if (ex is CardException cardEx)
            {
                Console.WriteLine(cardEx.ToString());
            }
            else if (ex is ArgumentException)
            {
                Console.WriteLine($"Ошибка в аргументах: {ex.Message}");
            }
            else
            {
                Console.WriteLine("Произошла непредвиденная ошибка.");
            }
            LogException(ex);
        }
        public static void ReadFile(string name, Reigns[] arr)
        {
            string[] lines = File.ReadAllLines(name);
            int successCount = 0;
            int errorCount = 0;
            for (int i = 0; i < Math.Min(lines.Length, arr.Length); i++)
            {
                string line = lines[i];
                try
                {
                    if (string.IsNullOrWhiteSpace(line)) throw new CardException("Пустая строка в файле", 0, "Unknown");
                    string[] parts = line.Split('|');
                    if (parts.Length < 2) throw new CardException($"Недостаточно данных ({parts.Length} полей вместо минимум 2)", 0, "Unknown");
                    string type = parts[0];
                    if (parts.Length < 3) throw new CardException($"Недостаточно данных ({parts.Length} полей вместо минимум 3)", 0, "Unknown");
                    string cardName = parts[2];
                    if (parts.Length < 9) throw new CardException($"Недостаточно данных ({parts.Length} вместо 9)", 0, cardName);
                    if (string.IsNullOrWhiteSpace(cardName)) throw CardException.InvalidName(0, cardName);
                    switch (type)
                    {
                        case "Kingdom":
                            arr[successCount] = new KingdomCard(
                                parts[8],
                                int.Parse(parts[4]),
                                int.Parse(parts[5]),
                                int.Parse(parts[6]),
                                int.Parse(parts[7])
                            );
                            successCount++; break;
                        case "Magic":
                            arr[successCount] = new MagicCard(
                                int.Parse(parts[1]),
                                cardName,
                                parts[3],
                                int.Parse(parts[8]),
                                int.Parse(parts[4]),
                                int.Parse(parts[5]),
                                int.Parse(parts[6]),
                                int.Parse(parts[7])
                            );
                            successCount++; break;
                        case "BattleMagic":
                            if (parts.Length < 10) throw new CardException($"Для BattleMagic нужно 10 параметров ({parts.Length} вместо 10)", 0, cardName);

                            arr[successCount] = new BattleMagicCard(
                                int.Parse(parts[1]),
                                cardName,
                                parts[3],
                                int.Parse(parts[8]),
                                int.Parse(parts[9]),
                                int.Parse(parts[4]),
                                int.Parse(parts[5]),
                                int.Parse(parts[6]),
                                int.Parse(parts[7])
                            );
                            successCount++; break;
                        default: throw new CardException($"Неизвестный тип карты '{type}'", 0, cardName);
                    }
                }
                catch (FormatException formatEx)
                {
                    var ex = CardException.InvalidIdType(0, "Unknown", formatEx);
                    Console.WriteLine($"Строка {i + 1}: {ex}");
                    errorCount++;
                    LogException(ex);
                }
                catch (CardException cardEx)
                {
                    Console.WriteLine($"Строка {i + 1}: {cardEx}");
                    errorCount++;
                    LogException(cardEx);
                }
                catch (Exception ex)
                {
                    var cardEx = new CardException($"Непредвиденная ошибка: {ex.Message}", 0, "Unknown", ex);
                    Console.WriteLine($"Строка {i + 1}: {cardEx}");
                    errorCount++;
                    LogException(cardEx);
                }
            }
            Console.WriteLine($"Успешно загружено карт: {successCount} из {lines.Length}");
            if (errorCount > 0) Console.WriteLine($"Пропущено карт с ошибками: {errorCount}");
        }
        public static void Res(Reigns[] arr3)
        {
            KingdomCard testKingdom = new KingdomCard();
            Console.WriteLine("\n=== МЕТОДЫ ===\n");
            var workcard = GetValidCards(arr3);
            for (int i = 0; i < workcard.Count; i++)
            {
                var card = workcard[i];
                Console.WriteLine($"\n----------- карта {i + 1}: {card.GetType().Name} -----------");
                card.PrintCard();
                Console.WriteLine($"ToString(): {card}");
                Console.WriteLine($"Положительная карта: {card.IsPositiveCard()}");
                Console.WriteLine($"Общее влияние: {card.GetTotalIzm()}");
                if (card is MagicCard)
                {
                    Console.WriteLine("До применения карты:");
                    testKingdom.PrintCard();

                    card.UseCard(testKingdom);

                    Console.WriteLine("После применения карты:");
                    testKingdom.PrintCard();

                    testKingdom.ResetToBaseValues();
                }
                if (i > 0)
                {
                    Console.WriteLine($"Сравнение с предыдущим: {card.CompareTo(workcard[i - 1])}");
                }
                Console.WriteLine("----------------------------------------");
            }
            Console.WriteLine("\n=== ОПЕРАТОРЫ ===\n");
            if (workcard.Count >= 2)
            {
                Console.WriteLine($"Карта 1 > Карта 2: {workcard[0] > workcard[1]}");
                Console.WriteLine($"Карта 1 == Карта 2: {workcard[0] == workcard[1]}");
            }
            else
            {
                Console.WriteLine("Недостаточно карт для сравнения операторов");
            }
        }
        public static List<Reigns> SortCards(Reigns[] cards)
        {
            var workcard = GetValidCards(cards);
            if (workcard.Count == 0)
            {
                Console.WriteLine("Нет валидных карт для сортировки!");
                return workcard;
            }
            workcard.Sort();
            Console.WriteLine("Отсортированный список карт (тип - ID - церковь - народ - армия - казна - название):");
            Console.WriteLine(new string('-', 80));
            foreach (var card in workcard)
            {
                Console.WriteLine(
                    $"- {card.Name} (ID: {card.Id}, Тип: {card.GetType().Name}, Ц:{card.Church} Н:{card.People} А:{card.Army} К:{card.Money})"
                );
            }
            Console.WriteLine($"Всего отсортировано: {workcard.Count} карт");
            return workcard;
        }
        private static List<Reigns> GetValidCards(Reigns[] cards)
        {
            var workcard = new List<Reigns>();
            foreach (var card in cards)
            {
                if (card != null && !string.IsNullOrWhiteSpace(card.Name))
                    workcard.Add(card);
            }
            return workcard;
        }
        public static void LogException(Exception ex)
        {
            try
            {
                string logMessage;
                if (ex is CardException cardEx)
                {
                    logMessage =
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm}] {ex.GetType().Name}\n" +
                        $"Сообщение: {ex.Message}\n" +
                        $"ID карты: {cardEx.CardId}\n" +
                        $"Название карты: {cardEx.CardName}\n" +
                        $"Stack Trace: {ex.StackTrace}\n" +
                        new string('-', 50) + "\n";
                }
                else
                {
                    logMessage =
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm}] {ex.GetType().Name}\n" +
                        $"Сообщение: {ex.Message}\n" +
                        $"Stack Trace: {ex.StackTrace}\n" +
                        new string('-', 50) + "\n";
                }
                File.AppendAllText("error_log.txt", logMessage);
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Не удалось записать в лог: {logEx.Message}");
            }
        }
    }
}
