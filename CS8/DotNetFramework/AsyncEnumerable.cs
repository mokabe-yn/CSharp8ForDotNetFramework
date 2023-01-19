using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CS8 {
    [TestClass]
    public class AsyncEnumerable {
        static async IAsyncEnumerable<int> Generator(int generator_id, int first_wait) {
            await Task.Delay(first_wait);
            yield return generator_id;
            await Task.Delay(100);
            yield return generator_id;
            await Task.Delay(100);
            yield return generator_id;
        }
        class LockedQueue {
            readonly Queue<int> _q = new Queue<int>();
            public void Add(int a) {
                lock (_q) {
                    _q.Enqueue(a);
                }
            }
            public Queue<int> Q => _q;
        }

        [TestMethod, Timeout(2000)]
        public void TestMethod1() {
            var q = new LockedQueue();
            Task t1 = Task.Run(async () => {
                await foreach (var x in Generator(0, 0)) {
                    q.Add(x);
                }
            });
            Task t2 = Task.Run(async () => {
                await foreach (var x in Generator(1, 50)) {
                    q.Add(x);
                }
            });
            Task.WaitAll(new[] { t1, t2 });

            CollectionAssert.AreEqual(new[] { 0, 1, 0, 1, 0, 1 }, q.Q);
            //System.Threading.Tasks.Sources.ManualResetValueTaskSourceCore;
            //System.Runtime.CompilerServices.ConfiguredAsyncDisposable;
            //System.Runtime.CompilerServices.AsyncIteratorMethodBuilder
        }
    }
}
