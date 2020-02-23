using System;

namespace PhoneBookManagement.Library.Domain
{
    /// <summary>
    ///     Entitet i Domain-it te librarise
    /// </summary>
    public class Contact
    {
        /// <summary>
        ///     Identifikues unik i nje kontakti
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        ///     Emri i kontaktit
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Mbiemri i kontaktit
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Lloji i telefonit
        ///     <see cref="PhoneBookManagement.Library.Domain.PhoneType"/>
        /// </summary>
        public PhoneType PhoneType { get; set; }

        /// <summary>
        ///     Numri i telefonit
        /// </summary>
        public PhoneNumber Number { get; set; }

        public override string ToString() => $"{FirstName}-{LastName} : {PhoneType.ToString()} : {Number}";
    }
}
