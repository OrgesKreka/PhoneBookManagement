using PhoneBookManagement.Library.Domain;
using PhoneBookManagement.Library.UseCases;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookManagement.Library.Persistence.Commands
{
    /// <summary>
    ///     Implementimi konkret i klases se menagimit
    ///     Mapimi i metodave te klases menaxhuese me metodat e menyres se persistences
    /// </summary>
    public class PhoneBookManager : IPhoneBook
    {
        private readonly IStorage _persistenceStorage;

        public PhoneBookManager(IStorage storage)
        {
            _persistenceStorage = storage;
        }

        public void Add(Contact contact) => _persistenceStorage.Add(contact);

        public Task AddAsync(IEnumerable<Contact> contacts) => _persistenceStorage.AddAsync(contacts);

        public IEnumerable<Contact> ContactsOrdered() => _persistenceStorage.ContactsOrdered();

        public IEnumerable<Contact> ContactsOrderedByFirstName() => _persistenceStorage.ContactsOrderedByFirstName();

        public IEnumerable<Contact> ContactsOrderedByLastName() => _persistenceStorage.ContactsOrderedByLastName();

        public void Delete(Contact contact) => _persistenceStorage.Delete(contact);

        public Task DeleteContactsAsync(IEnumerable<Contact> contacts) => _persistenceStorage.DeleteContactsAsync(contacts);

        public Contact Edit(Contact contact) => _persistenceStorage.Edit(contact);

        public Task EditContactsAsync(IEnumerable<Contact> contacts) => _persistenceStorage.EditContactsAsync(contacts);

        public Task<IEnumerable<Contact>> GetContactsAsync() => _persistenceStorage.GetContactsAsync();
    }
}
