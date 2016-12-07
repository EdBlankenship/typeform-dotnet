namespace Typeform.Dotnet.Enums
{
    /// <summary>
    /// Specifies all of the different question types on Typeform.com
    /// </summary>
    /// <remarks>
    /// More information about the question types that are possible are available at:  https://www.typeform.com/help/question-types/
    /// </remarks>
    public enum QuestionType
    {
        /// <summary>
        /// Also known as "textfield"
        /// </summary>
        ShortText = 0,
        /// <summary>
        /// Also known as "textarea"
        /// </summary>
        LongText = 1,
        Statement = 2,
        Dropdown = 3,
        Email = 4,
        Date = 5,
        /// <summary>
        /// Also known as "terms"
        /// </summary>
        Legal = 6,
        Website = 7,
        List = 8,
        ImageList = 9,
        /// <summary>
        /// Also known as "group"
        /// </summary>
        QuestionGroup = 10,
        YesNo = 11,
        Rating = 12,
        OpinionScale = 13,
        Number = 14,
        FileUpload = 15,
        Payment = 16
    }
}
