namespace StarCinema_Api.DTOs
{
    public class LinkDTO
    {
        public LinkDTO(string href, string rel, string type) {
            this.Href = href;
            this.Rel = rel;
            this.Type = type;   
        }
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Type { get; private set; }

    }
}
