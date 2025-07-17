using task18;
using CommandLib;

public class MultiStepCommandTests
{
    [Fact]
    public void MultiStepCommand_Executes_All_Steps()
    {
        var scheduler = new RoundRobinScheduler();
        var thread = new SchedulerThread(scheduler);

        for (int i = 0; i < 5; i++)
        {
            scheduler.Add(new MultiStepCommand(i, 3, scheduler));
        }

        thread.Start();

        Thread.Sleep(100);

        thread.RequestHardStop();
    }

    [Fact]
    public void MultiStepCommand_DoesNotExceed_MaxCount()
    {
        var scheduler = new RoundRobinScheduler();
        var thread = new SchedulerThread(scheduler);

        var counters = new int[5];
        for (int i = 0; i < 5; i++)
        {
            scheduler.Add(new TestCounterCommand(i, 3, scheduler, counters));
        }

        thread.Start();
        Thread.Sleep(200);
        thread.RequestHardStop();

        foreach (var count in counters)
        {
            Assert.Equal(3, count);
        }
    }

    private class TestCounterCommand : ICommand
    {
        private readonly int _id;
        private int _counter = 0;
        private readonly int _maxCount;
        private readonly IScheduler _scheduler;
        private readonly int[] _counters;

        public TestCounterCommand(int id, int maxCount, IScheduler scheduler, int[] counters)
        {
            _id = id;
            _maxCount = maxCount;
            _scheduler = scheduler;
            _counters = counters;
        }

        public void Execute()
        {
            if (_counter < _maxCount)
            {
                _counters[_id]++;
                _counter++;
                if (_counter < _maxCount)
                {
                    _scheduler.Add(this);
                }
            }
        }
    }
}
