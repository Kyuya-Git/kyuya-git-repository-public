using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnableFeatures
{
    public enum ErrorCode
    {
        ERROR_SUCCESS = 0,
        ERROR_MUTEX = 1,
        ERROR_NO_NET35 = 2,
        ERROR_READINIERROR = 3,
        ERROR_CONTROLINVALID = 4,
        ERROR_CANCEL = 5,
        ERROR_ON_ENABLE = 6
    }
}
