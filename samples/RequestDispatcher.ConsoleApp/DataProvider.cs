using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.ConsoleApp;

public class DataProvider
{
    public string Name { get; set; }

    public async IAsyncEnumerable<string> ReadData([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            yield return $"{Name}: {DateTime.Now}";
            await Task.Delay(1000);
        }
    }
}
