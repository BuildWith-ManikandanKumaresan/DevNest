using DevNest.Infrastructure.Entity;
using DevNest.Plugin.Contracts;

namespace DevNest.Plugin.Json
{
    public class JsonDataContext<T> : IDataContext<T> where T : class
    {
        public JsonDataContext(Dictionary<string, object>? _connectionParams)
        {
            ConnectionParams = _connectionParams;
        }

        public Dictionary<string, object>? ConnectionParams { get; private set; }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T>? Get()
        {
            return new List<CredentialEntity>()
            {
                new(){
                    Id = Guid.NewGuid(),
                    Title = "Test Credential",
                    HistoryInformation = new Common.Base.Entity.HistoryEntity()
                    { 
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                }
            } as IEnumerable<T>;
        }

        public T? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
