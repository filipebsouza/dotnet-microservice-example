namespace Products.API.Infra.Filters
{
    public class PaginationFilter
    {
        public int Total { get; set; }
        public int ItensPerPage { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int Pages { get; set; }
        public string OrderByFieldName { get; set; } = "Id";
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ASC;
    }
}