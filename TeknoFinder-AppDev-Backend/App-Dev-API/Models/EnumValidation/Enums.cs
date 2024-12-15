namespace AppDev.API.Models.EnumValidation
{
    public class Enums
    {
    }
    // Enum Accessible anywhere for thhe confessions
    public enum Classroom
    {
        // NGE Classrooms
        NGE100,
        NGE101,
        NGE102,
        NGE103,
        NGE104,
        NGE105,
        NGE200,
        NGE201,
        NGE202,
        NGE203,
        NGE204,
        NGE205,
        NGE300,
        NGE301,
        NGE302,
        NGE303,
        NGE304,
        NGE305,
        NGE400,
        NGE401,
        NGE402,
        NGE403,
        NGE404,
        NGE405,
        NGE500,
        NGE501,
        NGE502,
        NGE503,
        NGE504,
        NGE505,

        // GLE Classrooms
        GLE100,
        GLE101,
        GLE102,
        GLE103,
        GLE104,
        GLE105,
        GLE200,
        GLE201,
        GLE202,
        GLE203,
        GLE204,
        GLE205,
        GLE300,
        GLE301,
        GLE302,
        GLE303,
        GLE304,
        GLE305,
        GLE400,
        GLE401,
        GLE402,
        GLE403,
        GLE404,
        GLE405,
        GLE500,
        GLE501,
        GLE502,
        GLE503,
        GLE504,
        GLE505,

        // RTL Classrooms
        RTL100,
        RTL101,
        RTL102,
        RTL103,
        RTL104,
        RTL105,
        RTL200,
        RTL201,
        RTL202,
        RTL203,
        RTL204,
        RTL205,
        RTL300,
        RTL301,
        RTL302,
        RTL303,
        RTL304,
        RTL305,
        RTL400,
        RTL401,
        RTL402,
        RTL403,
        RTL404,
        RTL405,
        RTL500,
        RTL501,
        RTL502,
        RTL503,
        RTL504,
        RTL505
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
