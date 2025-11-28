using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace PublicHolidayTracker
{
    class Holiday
    {
        public string date { get; set; }
        public string localName { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public bool @fixed { get; set; } // 'fixed' c# da özel kelime olduğu için başına @ koydum
        public bool global { get; set; }
    }

    class Program
    {
        static List<Holiday> allHolidays = new List<Holiday>();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Veriler yükleniyor...");
            await LoadData();
            Console.WriteLine("Veriler hazır!");

            // menü döngüsü
            while (true)
            {
                Console.WriteLine("\n===== PublicHolidayTracker =====");
                Console.WriteLine("1. Tatil listesini göster (yıl seçmeli)");
                Console.WriteLine("2. Tarihe göre tatil ara (gg-aa formatı)");
                Console.WriteLine("3. İsme göre tatil ara");
                Console.WriteLine("4. Tüm tatilleri 3 yıl boyunca göster (2023–2025)");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        ListByYear();
                        break;
                    case "2":
                        SearchByDate();
                        break;
                    case "3":
                        SearchByName();
                        break;
                    case "4":
                        ListAll();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Geçersiz işlem.");
                        break;
                }
            }
        }
        static async Task LoadData()
        {
            string[] urls = {
                "https://date.nager.at/api/v3/PublicHolidays/2023/TR",
                "https://date.nager.at/api/v3/PublicHolidays/2024/TR",
                "https://date.nager.at/api/v3/PublicHolidays/2025/TR"
            };

            using (HttpClient client = new HttpClient())
            {
                foreach (string url in urls)
                {
                    try
                    {
                        string jsonVerisi = await client.GetStringAsync(url);

                        List<Holiday> gelenTatiller = JsonSerializer.Deserialize<List<Holiday>>(jsonVerisi);

                        foreach (Holiday h in gelenTatiller)
                        {
                            allHolidays.Add(h);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Veri çekilirken hata oluştu: " + url);
                    }
                }
            }
        }

        // yıla göre listeleme
        static void ListByYear()
        {
            Console.Write("Yıl girin (2023, 2024, 2025): ");
            string yil = Console.ReadLine();

            Console.WriteLine("\n--- " + yil + " Tatilleri ---");

            foreach (Holiday h in allHolidays)
            {
                if (h.date.StartsWith(yil))
                {
                    Console.WriteLine(h.date + " - " + h.localName);
                }
            }
        }

        // tarihe göre arama
        static void SearchByDate()
        {
            Console.Write("Tarih girin (gg-aa): ");
            string input = Console.ReadLine();

            Console.WriteLine("\n--- Sonuçlar ---");

            bool bulundu = false;
            foreach (Holiday h in allHolidays)
            {
                DateTime tarih = DateTime.Parse(h.date);
                string formatliTarih = tarih.ToString("dd-MM");

                if (formatliTarih == input)
                {
                    Console.WriteLine(h.date + " -> " + h.localName);
                    bulundu = true;
                }
            }

            if (bulundu == false)
            {
                Console.WriteLine("Tatil bulunamadı.");
            }
        }

        // isme göre arama
        static void SearchByName()
        {
            Console.Write("Tatil adı girin: ");
            string aranan = Console.ReadLine().ToLower();

            Console.WriteLine("\n--- Sonuçlar ---");

            bool bulundu = false;
            foreach (Holiday h in allHolidays)
            {
                if (h.localName.ToLower().Contains(aranan))
                {
                    Console.WriteLine(h.date + " - " + h.localName);
                    bulundu = true;
                }
            }

            if (bulundu == false)
            {
                Console.WriteLine("Bulunamadı.");
            }
        }

        // tümünü listele
        static void ListAll()
        {
            Console.WriteLine("\n--- Tüm Tatiller (2023-2025) ---");
            foreach (Holiday h in allHolidays)
            {
                Console.WriteLine(h.date + ": " + h.localName);
            }
        }
    }

}
