namespace Mechanics.Cell
{
    public interface ICell 
    {
        bool IsDefined
        {
            get;
        }

        bool CouldBe(NumericValue value);

        ICell ExcludeValue(NumericValue value);

        NumericValue Value { get; }
    }
}
