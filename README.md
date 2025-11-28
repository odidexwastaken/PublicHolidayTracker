# 📅 PublicHolidayTracker - Türkiye Resmi Tatil Takip Sistemi

Bu proje, bir C# Konsol uygulamasıdır. `nager.at` API servisini kullanarak 2023, 2024 ve 2025 yıllarına ait Türkiye resmi tatil verilerini çeker ve kullanıcıya sorgulama imkanı sunar.

## 🚀 Proje Özellikleri ve İsterler

Proje, belirtilen senaryoya uygun olarak aşağıdaki teknik gereksinimleri karşılamaktadır:

* **Veri Kaynağı:** `HttpClient` kütüphanesi kullanılarak API üzerinden JSON verisi çekilmektedir.
* **JSON İşleme:** Çekilen veriler `System.Text.Json` ile `Holiday` sınıfına deserialize edilmektedir.
* **Hafıza Yönetimi:** Uygulama açıldığında veriler tek seferde çekilip hafızaya (`List<Holiday>`) alınır. Daha sonraki sorgular bu liste üzerinden yapılır.

### 📋 Sınıf Yapısı
Projede kullanılan `Holiday` sınıfı, JSON yapısına uygun olarak şu şekildedir:

```csharp
class Holiday
{
    public string date { get; set; }
    public string localName { get; set; }
    public string name { get; set; }
    public string countryCode { get; set; }
    public bool fixed { get; set; }
    public bool global { get; set; }
}
