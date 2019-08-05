using Habitual.Core.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitual.Test.Threading
{
    public class TestMainThread : MainThread
    {
        public void Post(Action action)
        {
            action.Invoke();
        }
    }
}
