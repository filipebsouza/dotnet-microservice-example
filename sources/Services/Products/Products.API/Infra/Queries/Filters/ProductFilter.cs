namespace Products.API.Infra.Filters
{
    public class ProductFilter
    {
        public string Name { get; set; }
        public PaginationFilter Pagination { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}