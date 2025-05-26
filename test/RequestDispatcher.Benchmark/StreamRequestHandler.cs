using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Sreams;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Benchmark;

public record StreamRequest() : IStreamRequest<StreamResult>;
public record struct StreamResult(string Message);

internal class StreamRequestHandler : IStreamRequestHandler<StreamRequest, StreamResult>
{
    private static readonly int[] _streams = Enumerable.Range(1,1).ToArray();
    public async IAsyncEnumerable<StreamResult> Handle(StreamRequest request, CancellationToken cancellationToken = default)
    {
        foreach (var stream in _streams)
        {
            yield return new StreamResult($"" + stream);
        }
    }
}
