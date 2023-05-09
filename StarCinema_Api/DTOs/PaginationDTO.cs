namespace StarCinema_Api.DTOs
{
    public class PaginationDTO<T>
    {

        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
        public int TotalPage => (int)Math.Ceiling((double)TotalCount / PageSize);
        public List<T> ListItem { get; set; }
    }
}
