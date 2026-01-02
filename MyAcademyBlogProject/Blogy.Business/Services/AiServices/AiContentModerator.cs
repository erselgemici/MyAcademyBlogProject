using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Blogy.Business.Services.AiServices
{
    public class AiContentModerator
    {
        private readonly string _apiKey = "";

        private const string TranslationModelUrl = "https://router.huggingface.co/hf-inference/models/Helsinki-NLP/opus-mt-tr-en";
        private const string ToxicBertModelUrl = "https://router.huggingface.co/hf-inference/models/unitary/toxic-bert";

        public async Task<bool> IsContentToxicAsync(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return false;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                // 1. ADIM: ÇEVİRİ
                string translatedText = await SendRequestAsync(client, TranslationModelUrl, content, "translation_text");

                // 2. ADIM: TOKSİKLİK ANALİZİ
                double score = await SendRequestAsync(client, ToxicBertModelUrl, translatedText, "score");

                // Skor 0.50'den büyükse toksiktir
                return score > 0.50;
            }
        }

        // --- MERKEZİ İSTEK METODU ---
        private async Task<dynamic> SendRequestAsync(HttpClient client, string url, string text, string propertyToLookFor)
        {
            var requestBody = new { inputs = text };
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, jsonContent);
            var responseString = await response.Content.ReadAsStringAsync();

            // 🛑 HALA HATA VARSA GÖRELİM
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"HUGGING FACE HATASI: {response.StatusCode} - {responseString}");
            }

            using (JsonDocument doc = JsonDocument.Parse(responseString))
            {
                var root = doc.RootElement;

                // 1. Çeviri Cevabı
                if (propertyToLookFor == "translation_text")
                {
                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        return root[0].GetProperty("translation_text").GetString();
                    }
                }
                // 2. Toksiklik Cevabı
                else if (propertyToLookFor == "score")
                {
                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        var items = root[0].ValueKind == JsonValueKind.Array ? root[0] : root;
                        foreach (var item in items.EnumerateArray())
                        {
                            if (item.GetProperty("label").GetString() == "toxic")
                            {
                                return item.GetProperty("score").GetDouble();
                            }
                        }
                    }
                }
            }
            // Beklenmedik format
            return propertyToLookFor == "score" ? 0.0 : text;
        }
    }
}

