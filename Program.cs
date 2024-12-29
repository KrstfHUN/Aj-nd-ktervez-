namespace Ajandektervezo
{
    internal class Program
    {
        static List<string> Nev = new List<string>();
        static List<string> Kat = new List<string>();
        static List<int> Ar = new List<int>();
        static int Koltsegvetes;

        static void Main(string[] args)
        {
            SetInitialBudget();
            bool fut = true;
            while (fut)
            {
                Console.WriteLine("1. Ajándékok kezelése\t2. Költségvetés ellenőrzése\n3. Ajándékok megtekintése és kategorizálása\t4. Statisztikák\n5. Kilépés");
                Console.Write("Válassz egy lehetőséget: ");

                try
                {
                    int menupont1 = Convert.ToInt32(Console.ReadLine());
                    switch (menupont1)
                    {
                        case 1:
                            ManageGifts();
                            break;
                        case 2:
                            CheckBudget();
                            break;
                        case 3:
                            ViewAndCategorizeGifts();
                            break;
                        case 4:
                            ShowStatistics();
                            break;
                        case 5:
                            fut = false;
                            Console.WriteLine("Kellemes karácsonyt!");
                            break;
                        default:
                            Console.WriteLine("Érvénytelen menüpont. Próbáld újra.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Hibás bemenet. Számot adj meg!");
                }
            }
        }

        static void SetInitialBudget()
        {
            Console.Clear();
            Console.Write("Add meg a teljes költségvetést: ");
            try
            {
                Koltsegvetes = Convert.ToInt32(Console.ReadLine());
                if (Koltsegvetes < 0)
                {
                    Console.WriteLine("A költségvetés nem lehet negatív. Próbáld újra.");
                    SetInitialBudget();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Hibás bemenet. Számot adj meg!");
                SetInitialBudget();
            }
        }

        static void CheckBudget()
        {
            Console.Clear();
            int totalSpent = Ar.Sum();
            int remainingBudget = Koltsegvetes - totalSpent;

            Console.WriteLine($"Eddig elköltött összeg: {totalSpent} Ft");
            Console.WriteLine($"Hátralévő költségvetés: {remainingBudget} Ft");

            if (remainingBudget < 0)
            {
                Console.WriteLine("FIGYELEM: Túllépted a költségvetést!");
            }
            else if (remainingBudget == 0)
            {
                Console.WriteLine("Pontosan annyit költöttél, mint a költségvetésed.");
            }

            Console.WriteLine("Nyomj egy gombot a folytatáshoz...");
            Console.ReadKey();
        }

        static void ManageGifts()
        {
            Console.Clear();
            Console.WriteLine("1. Ajándék hozzáadása\n2. Ajándék szerkesztése\n3. Ajándék eltávolítása");
            Console.Write("Válassz egy lehetőséget: ");

            try
            {
                int menupont2 = Convert.ToInt32(Console.ReadLine());
                switch (menupont2)
                {
                    case 1:
                        AddGift();
                        break;
                    case 2:
                        EditGift();
                        break;
                    case 3:
                        RemoveGift();
                        break;
                    default:
                        Console.WriteLine("Érvénytelen opció. Próbáld újra.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Hibás bemenet. Számot adj meg!");
            }
        }

        static void AddGift()
        {
            Console.Clear();
            Console.Write("Ajándék neve: ");
            string nev = Console.ReadLine();

            Console.Write("Ajándék ára: ");
            try
            {
                int ar = Convert.ToInt32(Console.ReadLine());
                if (ar <= 0)
                {
                    Console.WriteLine("Az ár nem lehet negatív vagy nulla. Próbáld újra.");
                    return;
                }

                Console.Write("Ajándék kategóriája: ");
                string kat = Console.ReadLine();

                Nev.Add(nev);
                Kat.Add(kat);
                Ar.Add(ar);

                Console.WriteLine("Ajándék sikeresen hozzáadva!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Hibás bemenet. Az árnak számnak kell lennie.");
            }
        }

        static void EditGift()
        {
            Console.Clear();
            ViewGifts();
            Console.Write("Add meg a módosítandó ajándék indexét: ");
            try
            {
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                if (index < 0 || index >= Nev.Count)
                {
                    Console.WriteLine("Érvénytelen index.");
                    return;
                }

                Console.Write("Új név: ");
                string ujNev = Console.ReadLine();

                Console.Write("Új ár: ");
                int ujAr = Convert.ToInt32(Console.ReadLine());
                if (ujAr <= 0)
                {
                    throw new Exception("Az ár nem lehet kisebb mint 1 Ft. Semmi nincs ingyen.");
                }

                Console.Write("Új kategória: ");
                string ujKat = Console.ReadLine();

                Nev[index] = ujNev;
                Ar[index] = ujAr;
                Kat[index] = ujKat;

                Console.WriteLine("Ajándék sikeresen módosítva!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Hibás bemenet. Számot adj meg a megfelelő helyeken!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }


        static void RemoveGift()
        {
            Console.Clear();
            ViewGifts();
            Console.Write("Add meg az eltávolítandó ajándék indexét: ");
            try
            {
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                if (index < 0 || index >= Nev.Count)
                {
                    Console.WriteLine("Érvénytelen index.");
                    return;
                }

                Nev.RemoveAt(index);
                Kat.RemoveAt(index);
                Ar.RemoveAt(index);

                Console.WriteLine("Ajándék sikeresen eltávolítva!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Hibás bemenet. Számot adj meg!");
            }
        }

        static void ViewAndCategorizeGifts()
        {
            Console.Clear();
            ViewGifts();

            Console.WriteLine("\nAjándékok kategorizálása:");
            var categorized = Kat.Distinct();
            foreach (var category in categorized)
            {
                Console.Write($"{category}: ");
                var itemsInCategory = Nev
                    .Select((name, index) => new { Name = name, Category = Kat[index], Price = Ar[index] })
                    .Where(item => item.Category == category);

                Console.WriteLine(string.Join(", ", itemsInCategory.Select(item => $"{item.Name} ({item.Price} Ft)")));
            }

            Console.WriteLine("\nNyomj egy gombot a folytatáshoz...");
            Console.ReadKey();
        }



        static void ViewGifts()
        {
            Console.Clear();
            Console.WriteLine("Ajándéklista:");
            for (int i = 0; i < Nev.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Név: {Nev[i]}, Ár: {Ar[i]} Ft, Kategória: {Kat[i]}");
            }
        }

        static void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine("Statisztikák:");

            if (Nev.Count == 0)
            {
                Console.WriteLine("Nincs ajándék a listában. Nem tudok statisztikákat megjeleníteni.");
                Console.WriteLine("Nyomj egy gombot a folytatáshoz...");
                Console.ReadKey();
                return;
            }

            int totalCost = Ar.Sum();
            Console.WriteLine($"Ajándékok száma: {Nev.Count}");
            Console.WriteLine($"Ajándékok összértéke: {totalCost} Ft");

            int maxPriceIndex = Ar.IndexOf(Ar.Max());
            int minPriceIndex = Ar.IndexOf(Ar.Min());
            Console.WriteLine($"Legdrágább ajándék: {Nev[maxPriceIndex]} ({Ar[maxPriceIndex]} Ft)");
            Console.WriteLine($"Legolcsóbb ajándék: {Nev[minPriceIndex]} ({Ar[minPriceIndex]} Ft)");

            if (totalCost > Koltsegvetes)
            {
                Console.WriteLine("FIGYELEM: A költségvetést túllépted!");
            }
            else
            {
                Console.WriteLine($"Még elérhető költségkeret: {Koltsegvetes - totalCost} Ft");
            }

            Console.WriteLine("Nyomj egy gombot a folytatáshoz...");
            Console.ReadKey();
        }

    }
}