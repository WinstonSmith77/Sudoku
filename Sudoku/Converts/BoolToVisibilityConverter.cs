namespace Sudoku.Converts
{
    namespace DMG.MV5.WPFLibrary.Converter
    {
        public class BoolToVisibilityConverter : BoolToVisibilityConverterBase
        {
            protected override bool GetBoolValue(object value)
            {
                return (bool)value;
            }

            protected override bool CheckInput(object value)
            {
                return value is bool;
            }
        }
    }
}
