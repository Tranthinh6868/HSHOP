namespace WebApp.Models;
public class SiteProvider : BaseProvider {
    public SiteProvider(IConfiguration configuration) : base(configuration){

    }
    CategoryRepository? category;
    ProductRepository ? product;
    CartRepository ? cart;
    ProvinceRepository? province;
    InvoiceRepository ? invoice;
    VnPaymentRespository? vnPayment;
    DistrictRepository? district;
    WardRepository ? ward;
    InvoiceDetailsRepository ? invoiceDetailsRepository;
    public CategoryRepository Category => category ??= new CategoryRepository(Connection);
    public ProductRepository Product => product ??= new ProductRepository(Connection);
    public CartRepository Cart => cart ??= new CartRepository(Connection);
    public InvoiceRepository Invoice => invoice ??= new InvoiceRepository(Connection);
    public VnPaymentRespository VnPayment => vnPayment ??= new VnPaymentRespository(Connection);
    public ProvinceRepository Province => province ??= new ProvinceRepository(Connection);
    public DistrictRepository District => district ??= new DistrictRepository(Connection);
    public WardRepository Ward => ward ??= new WardRepository(Connection);
    public InvoiceDetailsRepository InvoiceDetailsRepository => invoiceDetailsRepository ??= new InvoiceDetailsRepository(Connection);
}