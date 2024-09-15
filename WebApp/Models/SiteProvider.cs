namespace WebApp.Models;
public class SiteProvider : BaseProvider {
    public SiteProvider(IConfiguration configuration) : base(configuration){

    }
    CategoryRepository? category;
    ProductRepository ? product;
    CartRepository ? cart;
    
    InvoiceRepository ? invoice;
    public CategoryRepository Category => category ??= new CategoryRepository(Connection);
    public ProductRepository Product => product ??= new ProductRepository(Connection);

    public CartRepository Cart => cart ??= new CartRepository(Connection);
    public InvoiceRepository Invoice => invoice ??= new InvoiceRepository(Connection);
}