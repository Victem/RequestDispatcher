using Microsoft.Extensions.ObjectPool;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core
{
    public class InvokeParametersBuffer : IResettable
    {
        private readonly object[] _parameters = new object[2];
        private object _request = null;
        private CancellationToken _token;
        public object[] ConvertParameters(object request, CancellationToken cancellationToken) 
        {
            _parameters[0] = request;
            _parameters[1] = cancellationToken;
            return _parameters;
        }
        public bool TryReset()
        {
            _parameters[0] = null;
            _parameters[1] = null;
            return true;
        }
    }
}
