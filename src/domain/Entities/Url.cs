using Syllabore;

namespace Domain.Entities
{
    public sealed class Url : BaseEntity<int>
    {
        public int Hits { get; private set; }

        public string OriginalUrl { get; private set; }

        public string ShortUrl { get; private set; }

        public Url(int id, int hits, string originalUrl, string shortUrl) : base(id)
        {
            Hits = hits;
            OriginalUrl = originalUrl;
            ShortUrl = shortUrl;
        }

        public Url(string originalUrl)
        {
            OriginalUrl = originalUrl;
            Hits = 0;
        }

        public Url()
        {
        }

        public void CreateShortUrl()
        {
            var fantasyName = new NameGenerator();

            var shortUrl = $"{fantasyName.Next()}-{fantasyName.Next()}";

            ShortUrl = shortUrl;
        }
    }
}