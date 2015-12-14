using App.Model;

namespace App.Infrastructure
{
    public interface ICompanyRepository
    {
        Company GetById(int id);
    }
}