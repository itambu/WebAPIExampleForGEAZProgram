namespace WebApplication15.Models
{
    public class PageModel<T>
    {
        public int PageNum {  get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}
