using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.CQRS.Telegram.ResponseDto
{

    public class TelegramUpdateDto
    {
        [JsonPropertyName("message")]
        public TelegramMessageDto Message { get; set; }
    }

    public class TelegramMessageDto
    {
        [JsonPropertyName("message_id")]
        public long MessageId { get; set; }

        [JsonPropertyName("chat")]
        public TelegramChatDto Chat { get; set; }

        [JsonPropertyName("from")]
        public TelegramUserDto From { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class TelegramChatDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class TelegramUserDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("is_bot")]
        public bool IsBot { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("language_code")]
        public string LanguageCode { get; set; }
    }
}
