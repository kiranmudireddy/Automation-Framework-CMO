using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMOS_Automation_Framework.src.Config.Settings;

public class DbSettings
{
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Namespace { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int CommandTimeoutSeconds { get; set; } = 30;
}
