namespace GradeBook.GradeBooks
{
    class StandardGradeBooks : BaseGradeBook
    {
        public StandardGradeBooks(string name) : base(name)
        {
            Type = Enums.GradeBookType.Standard;
        }
    }
}
