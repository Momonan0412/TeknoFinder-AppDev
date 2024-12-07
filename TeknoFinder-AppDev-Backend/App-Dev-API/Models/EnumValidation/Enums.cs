namespace AppDev.API.Models.EnumValidation
{
    public class Enums
    {
    }
    // Enum Accessible anywhere for thhe confessions
    public enum Classroom
    {
        NGE101,
        NGE102,
        NGE103,
    }
    public enum Building
    {
        NGE,
        GLE,
        RTL,
    }
    public enum Day
    {
        SUNDAY,
        MONDAY,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY,
        SATURDAY,
    }
    public enum ConfessionContextType
    {
        Classroom,
        Building,

    }

    public enum StudentProgram
    {
        ComputerScience,
        InformationTechnology,
        Engineering,
        BusinessAdministration,
        Architecture,
        Nursing,
        MultimediaArts,
        Criminology,
    }

    public enum YearLevel
    {
        FirstYearFreshman,
        SecondYearSophomore,
        ThirdYearJunior,
        FourthYearSenior,
    }

    public enum Status
    {
        Regular,
        Irregular
    }
}
