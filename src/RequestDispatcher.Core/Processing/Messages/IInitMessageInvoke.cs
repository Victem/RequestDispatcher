using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Messages
{

    internal interface IInitMessageInvoke<TEmprtyResult>
    {
        ValueTask<TEmprtyResult> InitInvoke(IMessage<TEmprtyResult> request, CancellationToken cancellationToken = default);
    }
}
