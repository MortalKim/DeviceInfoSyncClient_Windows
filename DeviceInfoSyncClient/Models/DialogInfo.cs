using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInfoSyncClient.Models
{
    public class DialogInfo
    {
        public bool Show { get; set; }

        public string BtnText { get; set; }
        public string Message { get; set; }

        public DialogInfo(bool show, string message, string btnText)
        {
            Show = show;
            BtnText = btnText;
            Message = message;
        }
    }
}
