using System.Threading.Tasks;

namespace ModelMapperDemo.Utility
{
    public static class TaskHelper
    {
        /// <summary>
        /// Eventually returns a tuple containing the results of the tasks.
        /// </summary>
        public static Task<(T1, T2)> WhenAll<T1, T2>(
            Task<T1> task1,
            Task<T2> task2) =>
            Task.WhenAll(task1, task2)
                .ContinueWith(t => (task1.Result, task2.Result));

        /// <see cref="WhenAll{T1, T2}(Task{T1}, Task{T2})"/>
        public static Task<(T1, T2, T3)> WhenAll<T1, T2, T3>(
            Task<T1> task1,
            Task<T2> task2,
            Task<T3> task3) =>
            Task.WhenAll(task1, task2, task3)
                .ContinueWith(t => (task1.Result, task2.Result, task3.Result));
    }
}
