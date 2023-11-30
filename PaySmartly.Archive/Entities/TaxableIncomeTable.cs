namespace PaySmartly.Archive.Entities
{
    public record class TaxableIncomeTable(IReadOnlyCollection<TaxableRange> Ranges);
}