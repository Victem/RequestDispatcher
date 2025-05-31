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
            if (false) 
            {
                yield return $"{Name}: {DateTime.Now}";
            }
            else
            {
                Console.WriteLine($"{Name} nothing to return");
                await Task.Delay(1000);
                await Task.Yield();
            }

            //await Task.Delay(1000);
        }
    }
}
