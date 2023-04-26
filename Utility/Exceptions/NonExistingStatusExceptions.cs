using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Exceptions
{
    public class NonExistingStatusExceptions : Exception
    {
        public NonExistingStatusExceptions():
            base("Недопустимое значение статуса заказа. Ознакомьтесь с полным списком статусов в Utility.Types.OrderStatuses")
        {

        }
    }
}
