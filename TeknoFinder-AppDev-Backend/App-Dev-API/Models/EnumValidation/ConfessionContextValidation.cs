using AppDev.API.Models.Entities;

namespace AppDev.API.Models.EnumValidation
{
    // used to validate the confession context.. for 
    public class ConfessionContextValidation
    {
        // checks whether the context type is valid (Classroom or Building)
        // and its value if Classroom (NGE101,NGE102) if Building (NGE,GLE,RTL)
        // How to use: 
        // message to frontend if u have a choicebox if building or classroom make sure the value is Classroom, Building
        // same thing sa contextvalue, make sure if classroom ang gipick ang values sa room is NGE101,102 etc
        public void ValidateConfessionContext(Confession confession)
        {
            if (confession.ContextType == ConfessionContextType.Classroom && !Enum.TryParse<Classroom>(confession.ContextValue, out _))
            {
                throw new Exception("Invalid Classroom Value");
            }
            if (confession.ContextType == ConfessionContextType.Building && !Enum.TryParse<Building>(confession.ContextValue, out _))
            {
                throw new Exception("Invalid Building value");
            }
        }
    }
}
