using ClassLibrary;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class CompanyRepository
    {
        /// <summary>
        /// Removes company entity from Companies DbSet
        /// </summary>
        /// <param name="selectedItem">Selected Company object from DataGrid</param>
        /// <param name="_context">DbContext object</param>
        public void RemoveCompany(Company selectedItem, DatabaseContext _context)
        {
            _context.Companies.Remove(selectedItem);
            _context.SaveChanges();
        }

        /// <summary>
        /// Adds Company entity to Companies DbSet
        /// </summary>
        /// <param name="company">New Company object</param>
        /// <param name="context">DbContext object</param>
        /// <param name="isUpdate">isUpdate specify add or update entity</param>
        public void AddCompany(Company company, DatabaseContext context, bool isUpdate)
        {
            if (isUpdate)
                context.Companies.Update(company);
            else
                context.Companies.Add(company);

            context.SaveChanges();
        }
    }
}
