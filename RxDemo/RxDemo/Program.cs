using RxDemo;
using System.Reactive;
using System.Reactive.Linq;

class Program
{

  static async Task Main(string[] args)
  {
    Console.WriteLine("=== Rx.NET Playground ===");
    Console.WriteLine("Make sure the project references the NuGet package: System.Reactive");
    Console.WriteLine();

    await DemoRangeBasic();
    await DemoObserverCreate();
    await DemoTimerWithTimestamp();
    //await DemoToObservable();
    //await DemoColdVsHot();

    Console.WriteLine();
    Console.WriteLine("All demos completed. Press any key to exit...");
    Console.ReadKey();
  }

  private static async Task DemoRangeBasic()
  {
    Helpers.Title("1) Observable.Range + basic Subscribe");
    var source = Observable.Range(1, 5);

    using var sub = source.Subscribe(
        x => Console.WriteLine($"OnNext: {x}"),
        ex => Console.WriteLine($"OnError: {ex.Message}"),
        () => Console.WriteLine("OnCompleted"));

    Helpers.Pause("Press any key to continue...");
    await Task.CompletedTask;
  }


  // 2) Observer.Create
  private static async Task DemoObserverCreate()
  {
    Helpers.Title("2) Observer.Create (explicit observer)");

    var source = Observable.Range(10, 3); // 10, 11, 12

    var observer = Observer.Create<int>(
        onNext: x => Console.WriteLine($"[Observer] OnNext: {x}"),
        onError: ex => Console.WriteLine($"[Observer] OnError: {ex.Message}"),
        onCompleted: () => Console.WriteLine("[Observer] OnCompleted"));

    using var sub = source.Subscribe(observer);

    Helpers.Pause("Press any key to continue...");
    await Task.CompletedTask;
  }

  // 3) Timer + Timestamp
  private static async Task DemoTimerWithTimestamp()
  {
    Helpers.Title("3) Timer + Timestamp (first after 5s, then every 1s, take 3)");

    Console.WriteLine("Current Time: " + DateTime.Now.ToString("T"));

    // first value after 5 seconds, then every 1 second
    var source = Observable.Timer(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1))
                           .Timestamp();

    using var sub = source.Subscribe(
        x => Console.WriteLine($"{x.Value} @ {x.Timestamp}"),
        () => Console.WriteLine("Timer sequence completed"));

    Console.WriteLine("Waiting for timer values...");
    // Wait long enough for the sequence to complete (5s + 2 more ticks ≈ 7s)
    await Task.Delay(TimeSpan.FromSeconds(8));

    Helpers.Pause("Press any key to continue...");
  }
}
