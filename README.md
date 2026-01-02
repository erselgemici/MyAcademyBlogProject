# 📘 Blogy - AI Powered .NET CMS Architecture

**Blogy**, modern web teknolojileri ve **N-Katmanlı Mimari (N-Tier Architecture)** prensipleri kullanılarak geliştirilmiş, **Yapay Zeka (OpenAI)** destekli, ölçeklenebilir bir İçerik Yönetim Sistemi (CMS) projesidir.

Proje, klasik blog fonksiyonlarının ötesine geçerek; içerik üretimi, kurumsal yazı yönetimi ve içerik güvenliği noktalarında yapay zeka servislerini aktif olarak kullanmaktadır.

---

## 🌟 Öne Çıkan Özellikler (Key Features)

Bu projeyi benzersiz kılan en önemli özellik, sistemin çekirdeğine entegre edilmiş **3 Farklı AI Servisi**dir:

### 1. 🤖 AI Article Generator (Otomatik Makale Üreticisi)
Yazarların "Writer Paneli" üzerinden erişebildiği bu modül, içerik üretim sürecini otomatize eder.
* **Nasıl Çalışır:** Kullanıcı bir konu başlığı girer. Sistem, OpenAI API'ye optimize edilmiş bir prompt gönderir.
* **Sonuç:** Dönen cevap, HTML formatında (başlıklar, paragraflar düzenlenmiş) parse edilir ve veritabanına yeni bir blog yazısı olarak kaydedilir.

### 2. 🏢 Corporate Content AI (Kurumsal İçerik Yönetimi)
Admin panelinde bulunan "AI ile Oluştur" özelliği sayesinde, sitenin statik alanları dinamikleşir.
* **Fonksiyon:** "Hakkımızda", "Vizyon", "Misyon" ve "Footer Açıklamaları" gibi alanlar için yapay zeka tarafından özgün ve kurumsal dilde metinler üretilir.

### 3. 🛡️ AI Content Moderator (Akıllı İçerik Filtreleme)
Platformun kalitesini korumak için geliştirilmiş bir güvenlik katmanıdır.
* **İşleyiş:** Kullanıcılar tarafından gönderilen yorumlar ve metinler, yapay zeka tabanlı duygu ve içerik analizinden geçirilir. Zararlı, hakaret içeren veya spam niteliğindeki içerikler tespit edilerek yayınlanması engellenir.

---

## 🏗️ Mimari Yapı (Architecture)

Proje, **Clean Code** ve **SoC (Separation of Concerns)** prensiplerine uygun olarak 4 ana katmana ayrılmıştır:

| Katman | Açıklama |
| :--- | :--- |
| **Entity Layer** | Veritabanı tablolarına karşılık gelen somut sınıflar (`Blog`, `Category`, `AppUser` vb.) bulunur. |
| **Data Access Layer (DAL)** | `DbContext`, `Migrations` ve veritabanı CRUD işlemlerinin soyutlandığı `Repository` desenini içerir. |
| **Business Layer (BL)** | Validasyon kuralları (`FluentValidation`), DTO dönüşümleri (`AutoMapper`) ve AI Servis çağrılarının yönetildiği iş mantığı katmanıdır. |
| **WebUI (Presentation)** | Kullanıcı ile etkileşime giren arayüz. `Controllers`, `ViewComponents`, `Views` ve `Areas` (Admin/Writer) yapılarını barındırır. |

---

## 🛠️ Teknoloji Yığını (Tech Stack)

* **Core Framework:** .NET 8.0 / ASP.NET Core
* **Language:** C#
* **Database:** MS SQL Server
* **ORM:** Entity Framework Core (Code First Approach)
* **AI Integration:** OpenAI API (GPT Models)
* **Frontend:** Bootstrap 5, HTML5, CSS3, JavaScript (jQuery)
* **Libraries & Tools:**
    * `AutoMapper` (Object-Object Mapping)
    * `FluentValidation` (Server-Side Validation)
    * `ASP.NET Core Identity` (Authentication & Authorization)
    * `Scrutor` (Dependency Injection Scanning)
    * `PagedList.Core` (Pagination)
    * `SweetAlert2` (UI Notifications)

---

## ⚙️ Fonksiyonel Modüller

* **Role Based Management:** Admin, Writer ve User rolleri için özelleştirilmiş paneller.
* **Dashboard:** Admin ve Yazarlar için grafiksel (Chart.js) verilerin ve istatistiklerin sunulduğu özet ekranı.
* **Advanced Profile Management:** Kullanıcıların profil bilgilerini ve resimlerini güncelleyebildiği arayüz.
* **Category & Tag System:** Blogların kategorize edilmesi ve etiketlenmesi için ilişkisel veritabanı yapısı.
* **ViewComponent Architecture:** Sayfa yüklenme hızını artırmak için `Sidebar`, `Footer`, `RecentPosts` gibi alanların modüler parçalanması.

---

## 📷 Ekran Görüntüleri (Screenshots)
<img width="947" height="278" alt="image" src="https://github.com/user-attachments/assets/fdcc5d52-c2a4-45ff-889a-6618005b3113" />
<img width="922" height="299" alt="image" src="https://github.com/user-attachments/assets/c815e26d-9120-441a-82df-24af5dfd2836" />
<img width="946" height="471" alt="image" src="https://github.com/user-attachments/assets/0a35dfe8-c474-4ce1-8035-2faa3a99512a" />
<img width="950" height="404" alt="image" src="https://github.com/user-attachments/assets/d4a57217-f14e-4c3c-8e71-f8cee5bdd2ed" />
<img width="952" height="448" alt="image" src="https://github.com/user-attachments/assets/d6d337b9-71e4-4dbc-b7b1-347d47214370" />
<img width="950" height="350" alt="image" src="https://github.com/user-attachments/assets/83bae4f9-c876-4f5a-9b01-f66faba0cc90" />
<img width="944" height="466" alt="image" src="https://github.com/user-attachments/assets/a1f5b62d-dd83-4624-9bc8-36bd81086fe9" />
<img width="943" height="374" alt="image" src="https://github.com/user-attachments/assets/b44fa95e-1e5a-46fb-91d6-a370a1a3cc0d" />
<img width="938" height="412" alt="image" src="https://github.com/user-attachments/assets/cf882763-a617-4495-ac61-af07fe2c70c6" />
<img width="941" height="394" alt="image" src="https://github.com/user-attachments/assets/52289aaa-03e2-4a4e-9922-6b5705e34156" />


