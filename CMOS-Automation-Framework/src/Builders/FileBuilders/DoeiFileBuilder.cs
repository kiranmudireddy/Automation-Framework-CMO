using System.Globalization;
using CMOS_Automation_Framework.src.Constants;

namespace CMOS_Automation_Framework.src.Builders.FileBuilders;

public class DoeiFileBuilder
{
    public IEnumerable<string> BuildInstructionLines(IEnumerable<(string AccountNumber, decimal Amount, string Reference)> instructions)
    {
        return instructions.Select(instruction =>
            string.Join(",",
                FileTypes.Doei,
                instruction.AccountNumber,
                instruction.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                instruction.Reference));
    }
}
