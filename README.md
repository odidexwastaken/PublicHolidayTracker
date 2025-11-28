# PublicHolidayTracker

Bu proje, C# Konsol uygulaması olarak geliştirilmiştir. API üzerinden 2023, 2024 ve 2025 yıllarına ait Türkiye resmi tatil verilerini çeker ve listeler.

## Özellikler

* **Veri Çekme:** `HttpClient` kullanılarak `nager.at` adresinden veriler JSON formatında alınır.
* **JSON İşleme:** `System.Text.Json` kütüphanesi ile veriler `Holiday` sınıfına dönüştürülür.
* **Listeleme:** Veriler hafızaya alındıktan sonra döngüler (foreach) kullanılarak filtreleme ve listeleme yapılır.

## Sınıf Yapısı

Hocanın belirttiği `Holiday` sınıfı kullanılmıştır:

```csharp
class Holiday
{
    public string date { get; set; }
    public string localName { get; set; }
    public string name { get; set; }
    public string countryCode { get; set; }
    public bool fixed { get; set; } // C# keyword çakışması için @fixed olarak tanımlandı
    public bool global { get; set; }
}
