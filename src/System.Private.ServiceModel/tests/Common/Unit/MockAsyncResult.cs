// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;

public class MockAsyncResult : IAsyncResult
    {
        public MockAsyncResult(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Timeout = timeout;
            Callback = callback;
            AsyncState = state;
            AsyncWaitHandle = new ManualResetEvent(false);
        }

        public TimeSpan Timeout { get; set; }

        public AsyncCallback Callback { get; set; }

        public object AsyncState { get; set; }

        public WaitHandle AsyncWaitHandle { get; set; }

        public bool CompletedSynchronously { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsCompleting { get; set; }

        public void Complete()
        {
            if (!IsCompleting)
            {
                IsCompleting = true;
                CompletedSynchronously = true;
                IsCompleted = true;
                ((ManualResetEvent)AsyncWaitHandle).Set();

                if (Callback != null)
                {
                    Callback(this);
                }
            }
        }
    }
