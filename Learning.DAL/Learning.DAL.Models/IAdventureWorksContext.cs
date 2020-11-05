using Learning.DAL.Generation.Repository;
using Learning.DAL.Models.AdventureWorksModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.DAL.Models
{
    public interface IAdventureWorksContext : IDbContextDuckType
    {
        DbSet<Address> Address { get; set; }
        DbSet<AddressType> AddressType { get; set; }
        DbSet<AwbuildVersion> AwbuildVersion { get; set; }
        DbSet<BillOfMaterials> BillOfMaterials { get; set; }
        DbSet<BusinessEntity> BusinessEntity { get; set; }
        DbSet<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        DbSet<BusinessEntityContact> BusinessEntityContact { get; set; }
        DbSet<ContactType> ContactType { get; set; }
        DbSet<CountryRegion> CountryRegion { get; set; }
        DbSet<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        DbSet<CreditCard> CreditCard { get; set; }
        DbSet<Culture> Culture { get; set; }
        DbSet<Currency> Currency { get; set; }
        DbSet<CurrencyRate> CurrencyRate { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<DatabaseLog> DatabaseLog { get; set; }
        DbSet<Department> Department { get; set; }
        DbSet<EmailAddress> EmailAddress { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; }
        DbSet<EmployeePayHistory> EmployeePayHistory { get; set; }
        DbSet<ErrorLog> ErrorLog { get; set; }
        DbSet<Illustration> Illustration { get; set; }
        DbSet<JobCandidate> JobCandidate { get; set; }
        DbSet<Location> Location { get; set; }
        DbSet<Password> Password { get; set; }
        DbSet<Person> Person { get; set; }
        DbSet<PersonCreditCard> PersonCreditCard { get; set; }
        DbSet<PersonPhone> PersonPhone { get; set; }
        DbSet<PhoneNumberType> PhoneNumberType { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ProductCategory> ProductCategory { get; set; }
        DbSet<ProductCostHistory> ProductCostHistory { get; set; }
        DbSet<ProductDescription> ProductDescription { get; set; }
        DbSet<ProductInventory> ProductInventory { get; set; }
        DbSet<ProductListPriceHistory> ProductListPriceHistory { get; set; }
        DbSet<ProductModel> ProductModel { get; set; }
        DbSet<ProductModelIllustration> ProductModelIllustration { get; set; }
        DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
        DbSet<ProductPhoto> ProductPhoto { get; set; }
        DbSet<ProductProductPhoto> ProductProductPhoto { get; set; }
        DbSet<ProductReview> ProductReview { get; set; }
        DbSet<ProductSubcategory> ProductSubcategory { get; set; }
        DbSet<ProductVendor> ProductVendor { get; set; }
        DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        DbSet<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }
        DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }
        DbSet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReason { get; set; }
        DbSet<SalesPerson> SalesPerson { get; set; }
        DbSet<SalesPersonQuotaHistory> SalesPersonQuotaHistory { get; set; }
        DbSet<SalesReason> SalesReason { get; set; }
        DbSet<SalesTaxRate> SalesTaxRate { get; set; }
        DbSet<SalesTerritory> SalesTerritory { get; set; }
        DbSet<SalesTerritoryHistory> SalesTerritoryHistory { get; set; }
        DbSet<ScrapReason> ScrapReason { get; set; }
        DbSet<Shift> Shift { get; set; }
        DbSet<ShipMethod> ShipMethod { get; set; }
        DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        DbSet<SpecialOffer> SpecialOffer { get; set; }
        DbSet<SpecialOfferProduct> SpecialOfferProduct { get; set; }
        DbSet<StateProvince> StateProvince { get; set; }
        DbSet<Store> Store { get; set; }
        DbSet<TransactionHistory> TransactionHistory { get; set; }
        DbSet<TransactionHistoryArchive> TransactionHistoryArchive { get; set; }
        DbSet<UnitMeasure> UnitMeasure { get; set; }
        DbSet<VAdditionalContactInfo> VAdditionalContactInfo { get; set; }
        DbSet<VEmployee> VEmployee { get; set; }
        DbSet<VEmployeeDepartment> VEmployeeDepartment { get; set; }
        DbSet<VEmployeeDepartmentHistory> VEmployeeDepartmentHistory { get; set; }
        DbSet<Vendor> Vendor { get; set; }
        DbSet<VIndividualCustomer> VIndividualCustomer { get; set; }
        DbSet<VJobCandidate> VJobCandidate { get; set; }
        DbSet<VJobCandidateEducation> VJobCandidateEducation { get; set; }
        DbSet<VJobCandidateEmployment> VJobCandidateEmployment { get; set; }
        DbSet<VPersonDemographics> VPersonDemographics { get; set; }
        DbSet<VProductAndDescription> VProductAndDescription { get; set; }
        DbSet<VProductModelCatalogDescription> VProductModelCatalogDescription { get; set; }
        DbSet<VProductModelInstructions> VProductModelInstructions { get; set; }
        DbSet<VSalesPerson> VSalesPerson { get; set; }
        DbSet<VSalesPersonSalesByFiscalYears> VSalesPersonSalesByFiscalYears { get; set; }
        DbSet<VStateProvinceCountryRegion> VStateProvinceCountryRegion { get; set; }
        DbSet<VStoreWithAddresses> VStoreWithAddresses { get; set; }
        DbSet<VStoreWithContacts> VStoreWithContacts { get; set; }
        DbSet<VStoreWithDemographics> VStoreWithDemographics { get; set; }
        DbSet<VVendorWithAddresses> VVendorWithAddresses { get; set; }
        DbSet<VVendorWithContacts> VVendorWithContacts { get; set; }
        DbSet<WorkOrder> WorkOrder { get; set; }
        DbSet<WorkOrderRouting> WorkOrderRouting { get; set; }
    }
}