namespace App.Infrastructure.Company
{
    public interface ICompanyRepository
    {
        Model.Company GetById(int id);
    }
}