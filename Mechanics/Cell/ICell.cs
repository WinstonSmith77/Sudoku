namespace Mechanics.Cell
{
    public interface ICell 
    {
        bool IsDefined
        {
            get;
        }

        bool MayHaveValue(NumericValue value);

        ICell ExcludeValue(NumericValue value);

        NumericValue Value { get; }
    }
}
