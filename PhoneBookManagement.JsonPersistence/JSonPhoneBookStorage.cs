using Newtonsoft.Json;
using PhoneBookManagement.Library.Domain;
using PhoneBookManagement.Library.UseCases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookManagement.JsonPersistence
{
    /// <summary>
    ///     Implementimi sipas persistences ne File JSON
    /// </summary>
    public class JSonPhoneBookStorage : IStorage
    {
        /// <summary>
        ///     objekt sa per lock, per ta bere metoden thread safe
        /// </summary>
        static readonly object _locker = new object();

        /// <summary>
        ///     Pathi ku ndodhet skedari .json
        /// </summary>
        private string _jsonFilePath;

        /// <summary>
        ///     Inicializon nje menaxhues kontaktesh ne json file
        /// </summary>
        /// <param name="filePath">Pathi ku ndodhet skedari json</param>
        public JSonPhoneBookStorage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath) || !filePath.Contains(".json"))
            {
                using (var file = File.Create(filePath))
                {
                    file.Close();
                }
            }

            _jsonFilePath = filePath;
        }
        /// <summary>
        ///     Shton kontaktin ne skedar
        /// </summary>
        /// <param name="contact"></param>
        public void Add(Contact contact)
        {
           lock(_locker)
            {
                var fileContent = ReadFileContent();
                var newJson = AddObjectsToJson(fileContent,  contact );
                File.WriteAllText(_jsonFilePath,newJson);
            }
        }

        /// <summary>
        ///     Shton kontaktin ne skedar asinkron
        ///     **Nuk eshte implementuar
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public Task AddAsync(IEnumerable<Contact> contacts)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Kthen listen e kontakteve te ruajtura ne skedar te renditura sipas emrit
        /// </summary>
        /// <returns>Listen me kontakte ose null nese skedari es</returns>
        public IEnumerable<Contact> ContactsOrdered()
        {
            lock (_locker)
            {
                var fileContent = ReadFileContent();
                return JsonConvert.DeserializeObject<List<Contact>>(fileContent)
                    ?.OrderBy(x => x.FirstName);
            }
        }

        /// <summary>
        ///     Njelloj si <see cref="ContactsOrdered"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> ContactsOrderedByFirstName()
        {
           lock(_locker)
            {
                return ContactsOrdered();
            }
        }

        /// <summary>
        ///     Kthen kontaktet te renditura sipas mbiemrit
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> ContactsOrderedByLastName()
        {
           lock(_locker)
            {
                return ContactsOrdered()?.OrderBy(x => x.LastName);
            }
        }

        /// <summary>
        ///     Fshin nje kontakt nga skedari
        /// </summary>
        /// <param name="contact"></param>
        public void Delete(Contact contact)
        {
            lock(_locker)
            {
                var fileContent = ReadFileContent();
                File.WriteAllText( _jsonFilePath, RemoveObjectsFromJson<Contact>(fileContent, contact));
            }
        }

        /// <summary>
        ///     Fshin nje kontakt nga skedari ne menyre asinkrone
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public Task DeleteContactsAsync(IEnumerable<Contact> contacts)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Editon nje kontakt ne skedar
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Kontaktin e edituar ose null nese nuk gjendet</returns>
        public Contact Edit(Contact contact)
        {
            lock(_locker)
            {
                var listOfContacts = JsonConvert.DeserializeObject<List<Contact>>(ReadFileContent()) ?? new List<Contact>();
                var existingContact = listOfContacts.FirstOrDefault(x => x.Guid == contact.Guid);
                if (existingContact == null) return null;

                listOfContacts.Remove(existingContact);
                listOfContacts.Add(contact);
                SaveContentToFile(JsonConvert.SerializeObject(listOfContacts));
                return contact;
            }
        }

        /// <summary>
        ///     Editon nje kontakt ne skedar ne menyre asinkrone
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public Task EditContactsAsync(IEnumerable<Contact> contacts)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Lexon listen me kontakte ne menyre asinkrone
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Contact>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Metode ndihmese qe shton nje liste me objekte tek nje string json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        private string AddObjectsToJson(string json, Contact objects)
        {
            List<Contact> list = JsonConvert.DeserializeObject<List<Contact>>(json);
            if (list == null) list = new List<Contact>();
            if (list.Any(x => x.Guid == objects.Guid))
                throw new ArgumentException($"{objects} already exists!");
            list.Add(objects);
            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }

        /// <summary>
        ///     Metode ndihmese qe heq nje kontakt nga skedari
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        private string RemoveObjectsFromJson<T>(string json, T contact)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json);
            if (list == null) list = new List<T>();
            list.Remove(contact);
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        ///     Metode ndihmese qe kthen permbajtjen e skedarit json
        /// </summary>
        /// <returns></returns>
        private string ReadFileContent() => File.ReadAllText(_jsonFilePath);

        /// <summary>
        ///     Metode ndihmese qe mbishkruan permbajtjen e skedarit json
        /// </summary>
        /// <param name="jsonContent"></param>
        private void SaveContentToFile(string jsonContent) => File.WriteAllText(_jsonFilePath, jsonContent);
    }
}
