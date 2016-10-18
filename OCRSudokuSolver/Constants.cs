namespace OCRSudokuSolver
{
    public partial class LearnWindow
    {
        const float NUM_OF_PERCENT_FILLED = 0.01f;
        public static int DIGIT_PATTERN_WIDTH = 30;
        public static int DIGIT_PATTERN_HEIGHT = 30;
        public static int DIGIT_PATTERN_MARGIN_TOP = 3;
        public static int DIGIT_PATTERN_MARGIN_BOTTOM = 4;
        public static int DIGIT_PATTERN_MARGIN_LEFT = 7;
        public static int DIGIT_PATTERN_MARGIN_RIGHT = 6;
    }

    public static partial class OcrReader
    {
        const double NUM_OF_PERCENT_OF_WIDTH_TO_BE_LINE = 0.8;
    }
}