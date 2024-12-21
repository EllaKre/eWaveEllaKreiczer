namespace eWaveEllaKreiczer.Models
{
    public class Movie
    {
        public required string Title { get; set; } // שם הסרט
        public required string ImageUrl { get; set; } // קישור לתמונה
        public required string Categories { get; set; } // רשימה של קטגוריות
        public double Rating { get; set; } // דירוג בין 1 ל-10
    }

}
