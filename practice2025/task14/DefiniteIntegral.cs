namespace task14;

public class DefiniteIntegral
{
    private static int _usingResource = 0;
    private static double _result = 0.0;

    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        _result = 0.0;
        double range = b - a;
        double stepSize = range / threadsnumber;
        using Barrier barrier = new Barrier(threadsnumber + 1);

        Thread[] threads = new Thread[threadsnumber];

        for (int i = 0; i < threadsnumber; i++)
        {
            double threadStart = a + i * stepSize;
            double threadEnd = (i == threadsnumber - 1) ? b : threadStart + stepSize;

            threads[i] = new Thread(() =>
            {
                SolvePartially(threadStart, threadEnd, function, step);
                barrier.SignalAndWait();
            });

            threads[i].Start();
        }

        barrier.SignalAndWait();
        
        foreach (var thread in threads)
        {
            thread.Join();
        }

        return _result;
    }

    private static void SolvePartially(double a, double b, Func<double, double> function, double step)
    {
        double current = 0.0;

        for (double x = a; x < b; x += step)
        {
            current += function(x) * step;
        }

        while (!IncrementResult(current))
        {
            Thread.Sleep(1);
        }
    }

    static bool IncrementResult(double current)
    {
        if (0 == Interlocked.Exchange(ref _usingResource, 1))
        {
            _result += current;
        
            Interlocked.Exchange(ref _usingResource, 0);
            return true;
        }

        return false;
    }
}
