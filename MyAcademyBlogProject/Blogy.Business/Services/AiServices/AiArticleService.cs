using System.Text;
using System.Text.Json;

namespace Blogy.Business.Services.AiServices
{
    public class AiArticleService
    {
        // 🔑 OpenAI Key'ini buraya ekle (Moderasyonda kullandığının aynısı)
        private readonly string _apiKey = "";

        public async Task<string> GenerateArticleAsync(string keywords, string userPrompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
                client.Timeout = TimeSpan.FromSeconds(60); // 60 saniye bekleme süresi tanıyalım

                var systemMessage = "Sen SEO uyumlu, Türkçe blog yazıları yazan profesyonel bir yazarsın. Yazılarını HTML formatında (<p>, <h3> vb.) ver.";
                var userMessage = $"Anahtar Kelimeler: {keywords}\nKonu: {userPrompt}\n\nLütfen yaklaşık 1000 karakterlik ilgi çekici bir makale yaz.";

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = systemMessage },
                        new { role = "user", content = userMessage }
                    },
                    temperature = 0.7
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);
                    var responseString = await response.Content.ReadAsStringAsync();

                    // 🛑 EĞER OPENAI HATA DÖNDÜYSE YAKALAYALIM
                    if (!response.IsSuccessStatusCode)
                    {
                        // Hatayı görelim (Quota exceeded, Invalid Key vs.)
                        return $"HATA OLUŞTU: {response.StatusCode} - {responseString}";
                    }

                    using (JsonDocument doc = JsonDocument.Parse(responseString))
                    {
                        // Cevap formatı bazen değişebilir, güvenli erişim yapalım
                        if (doc.RootElement.TryGetProperty("choices", out JsonElement choices) && choices.GetArrayLength() > 0)
                        {
                            return choices[0].GetProperty("message").GetProperty("content").GetString();
                        }
                        else
                        {
                            return "HATA: OpenAI boş veya beklenmedik bir cevap döndü.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return $"KRİTİK HATA: {ex.Message}";
                }
            }
        }

        public async Task<string> GenerateContactReplyAsync(string userMessage)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
                client.Timeout = TimeSpan.FromSeconds(60);

                var systemMessage = @"You are a strict language-matching assistant.
                              
                              YOUR TASK: 
                              1. Detect the language of the USER INPUT.
                              2. Translate the following sentence into the DETECTED LANGUAGE:
                                 'We have received your message, thank you. Our team will review it and get back to you as soon as possible.'
                              3. Output ONLY the translated sentence.

                              EXAMPLES:
                              Input: 'Hello, I need help.'
                              Output: We have received your message, thank you. Our team will review it and get back to you as soon as possible.

                              Input: 'Merhaba, yardım lazım.'
                              Output: Mesajınızı aldık, teşekkür ederiz. Ekibimiz inceleyip size en kısa sürede dönüş yapacaktır.

                              Input: 'Ich brauche Hilfe.'
                              Output: Wir haben Ihre Nachricht erhalten, vielen Dank. Unser Team wird sie überprüfen und sich so schnell wie möglich bei Ihnen melden.

                              Input: '안녕하세요'
                              Output: 메시지를 잘 받았습니다, 감사합니다. 저희 팀이 검토 후 조속히 답변 드리겠습니다.

                              CRITICAL RULE: NEVER reply in English unless the input is English.";

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
            {
                new { role = "system", content = systemMessage },
                new { role = "user", content = userMessage }
            },
                    temperature = 0
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);

                    if (!response.IsSuccessStatusCode) return "Mesajınız alındı (Otomatik yanıt servisi meşgul).";

                    var responseString = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(responseString))
                    {
                        if (doc.RootElement.TryGetProperty("choices", out JsonElement choices) && choices.GetArrayLength() > 0)
                        {
                            return choices[0].GetProperty("message").GetProperty("content").GetString();
                        }
                    }
                    return "Mesajınız alındı.";
                }
                catch
                {
                    return "Mesajınız başarıyla alındı.";
                }
            }
        }

        public async Task<string> GenerateFooterAboutTextAsync()
        {
            // API Key zaten sınıfın tepesinde tanımlı, onu kullanıyoruz.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
                client.Timeout = TimeSpan.FromSeconds(30);

                // Footer için özel, kısa ve öz prompt
                var prompt = "Blogy adında teknoloji, yazılım ve güncel gelişmeler üzerine içerik üreten bir blog sitesiyiz. " +
                             "Sitemizin footer (alt bilgi) kısmı için 'Hakkımızda' başlığı altına gelecek; " +
                             "samimi, profesyonel, okuyucuyu harekete geçiren ve maksimum 3 cümleden oluşan Türkçe bir tanıtım yazısı yaz.";

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                new { role = "system", content = "Sen yaratıcı bir içerik yazarısın." },
                new { role = "user", content = prompt }
            },
                    max_tokens = 100 // Kısa olması için limit
                };

                var jsonBody = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsonDoc = JsonDocument.Parse(responseString);
                    var result = jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                    return result?.Trim(); // Başındaki sonundaki boşlukları temizle
                }

                return "Yapay zeka servisine şu an ulaşılamıyor, lütfen manuel giriş yapınız.";
            }
        }
    }
}
