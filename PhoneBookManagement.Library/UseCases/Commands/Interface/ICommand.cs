namespace PhoneBookManagement.Library.UseCases.Commands.Interface
{
    /// <summary>
    ///     Nderfaqe e pergjithshme per cdo komande qe vepron mbi objektet e domainit
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public interface ICommand<TIn>
    {
        /// <summary>
        ///     Duke qene se cdo komande mund te veproje vetem njehere mbi domain
        ///     ka vetem nje metode ku e ben logjiken
        /// </summary>
        /// <param name="parameter"></param>
        void Execute(TIn parameter);
    }
}
