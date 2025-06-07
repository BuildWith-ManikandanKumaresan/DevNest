#region using directives
using DevNest.Infrastructure.Entity;
using DevNest.Plugin.Contracts.Storage;
using DevNest.Plugin.Json.Handler;
#endregion using directives

namespace DevNest.Plugin.Json
{
    /// <summary>
    /// Represents the class instance for JSON data context plugin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonDataContext<T> : IStorageDataContext<T> where T : class
    {
        private JsonDataHandler<T> _JsonHandler;
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataContext{T}"/> class with the specified connection parameters.
        /// </summary>
        /// <param name="_connectionParams"></param>
        public JsonDataContext(Dictionary<string, object>? _connectionParams)
        {
            ConnectionParams = _connectionParams;
            _JsonHandler = new JsonDataHandler<T>(_connectionParams ?? []);
        }

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; }

        /// <summary>
        /// Add the entity type of T to the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T? Add(T? entity)
        {
            if (entity == null)
                return entity;
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            if (!data?.Exists(a=>a.Id == (entity as CredentialEntity ?? new()).Id) ?? false)
            {
                data?.Add((entity as CredentialEntity ?? new()));
                _JsonHandler.Write(data as List<T> ?? []);
            }
            return entity;
        }

        /// <summary>
        /// Archives the entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Archive(Guid id)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;

            if (data == null) return false;

            var entity = data.FirstOrDefault(a => a.Id == id);
            if (entity == null) return false;

            entity.IsDisabled = true;

            _JsonHandler.Write(data.Cast<T>().ToList());

            return true;
        }


        /// <summary>
        /// Deletes the entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(Guid id)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            int res = data?.RemoveAll(x => x.Id == id) ?? 0;
            if(res > 0)
            {
                _JsonHandler.Write(data as List<T> ?? []);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes all entities of type T from the data context.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteAll()
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            int res = data?.RemoveAll(a => a.Id == a.Id) ?? 0;
            if (res > 0)
            {
                _JsonHandler.Write(data as List<T> ?? []);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the collection of entities of type T from the data context.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T>? Get()
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            return data as IEnumerable<T>;
        }

        /// <summary>
        /// Gets an entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? GetById(Guid id)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            return data?.FirstOrDefault(a => a.Id == id) as T;
        }

        /// <summary>
        /// Updates an existing entity of type T in the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T? Update(T? entity)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            if (entity == null || data == null)
                return entity;

            var input = entity as CredentialEntity;
            var existingEntity = data.FirstOrDefault(a => a.Id == input?.Id);

            if (existingEntity != null && input != null)
            {
                if (input.Title != null) existingEntity.Title = input.Title;
                if (input.Domain != null) existingEntity.Domain = input.Domain;
                if (input.Host != null) existingEntity.Host = input.Host;
                if (input.Username != null) existingEntity.Username = input.Username;
                if (input.Password != null) existingEntity.Password = input.Password;
                if (input.Type != null) existingEntity.Type = input.Type;
                if (input.Workspace != null) existingEntity.Workspace = input.Workspace;
                if (input.Environment != null) existingEntity.Environment = input.Environment;
                if (input.Tags != null) existingEntity.Tags = input.Tags;
                if (input.Notes != null) existingEntity.Notes = input.Notes;
                if (input.IsValid != null) existingEntity.IsValid = input.IsValid;
                if (input.IsPasswordMasked != null) existingEntity.IsPasswordMasked = input.IsPasswordMasked;
                if (input.IsEncrypted != null) existingEntity.IsEncrypted = input.IsEncrypted;
                if (input.EncryptionAlgorithm != null) existingEntity.EncryptionAlgorithm = input.EncryptionAlgorithm;
                if (input.ShowPasswordAsEncrypted != null) existingEntity.ShowPasswordAsEncrypted = input.ShowPasswordAsEncrypted;
                if (input.ExpirationDate != null) existingEntity.ExpirationDate = input.ExpirationDate;
                if (input.RotationPolicyInDays != null) existingEntity.RotationPolicyInDays = input.RotationPolicyInDays;
                if (input.IsDisabled != null) existingEntity.IsDisabled = input.IsDisabled;
                if (input.AssociatedGroups != null) existingEntity.AssociatedGroups = input.AssociatedGroups;

                _JsonHandler.Write(data as List<T> ?? []);
            }

            return existingEntity as T;
        }

    }
}
