namespace PhoneBookManagement.Library.Domain
{
    /// <summary>
    ///     Entitet i Domainit
    ///     Modeli i nje numri telefoni
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        ///     Nje numer telefoni mund te jete nga  vende te ndryshme
        ///     Ruan kodin e vendit
        /// </summary>
        public string CountryCodeSelected { get; set; } = "+355";

        /// <summary>
        ///     Duke supozuar se numri eshte validuar 
        ///     ne shtresat e mesiperme.
        ///     Kthen numrin ne formatin kodVendi + NumriTelefonit
        /// </summary>
        public string PhoneNumberRaw { get; set; }

        public override string ToString() => PhoneNumberRaw;
    }
}