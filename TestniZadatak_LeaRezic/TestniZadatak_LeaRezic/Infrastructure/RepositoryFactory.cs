using TestniZadatak_LeaRezic.Dal;

namespace TestniZadatak_LeaRezic.Infrastructure
{
    public static class RepositoryFactory
    {
        public static IRepository GetDefaultInstance()
        {
            return new DBRepository();
        }
    }
}