﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CS8 {
    [TestClass]
    public class AsyncEnumerable {
        class LockedQueue<T> {
            readonly Queue<T> _q = new Queue<T>();
            public void Add(T a) {
                lock (_q) {
                    _q.Enqueue(a);
                }
            }
            public Queue<T> Q => _q;
        }

        [TestMethod, Timeout(2000)]
        public void Use() {
            static async IAsyncEnumerable<int> Generator(int generator_id, int first_wait) {
                await Task.Delay(first_wait);
                yield return generator_id;
                await Task.Delay(100);
                yield return generator_id;
                await Task.Delay(100);
                yield return generator_id;
            }

            var q = new LockedQueue<int>();
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
            Task.WaitAll(t1, t2);

            q.Q.ForEach(Console.WriteLine);
            CollectionAssert.AreEqual(new[] { 0, 1, 0, 1, 0, 1 }, q.Q);
        }


        class MyException : Exception { }

        [TestMethod, Timeout(2000)]
        public void ThrowType() {
            static async IAsyncEnumerable<int> Generator() {
                await Task.Delay(100);
                yield return 0;
                throw new MyException();
            }

            Task t1 = Task.Run(async () => {
                await foreach (var _ in Generator()) {
                    Thread.Sleep(50);
                }
            });
            var e = Assert.ThrowsException<AggregateException>(() => Task.WaitAll(t1));
            Assert.IsInstanceOfType(e?.InnerException, typeof(MyException));
        }
    }

    static class ExtraExtension {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach(var x in source) {
                action(x);
            }
        }
    }
}
