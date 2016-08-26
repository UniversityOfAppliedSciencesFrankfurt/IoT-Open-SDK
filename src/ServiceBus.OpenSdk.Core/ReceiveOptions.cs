﻿//=======================================================================================
// Copyright © daenet GmbH Frankfurt am Main
//
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
using System;

namespace ServiceBus.OpenSdk
{
    public class ReceiveOptions
    {
        public ReceiveOptions()
        {
            this.TimeOut = TimeSpan.FromSeconds(60);
            this.ReceiveMode = ReceiveMode.PeekLock;
            this.SequenceNumber = 0;
        }

        public TimeSpan TimeOut { get; set; }
        public ReceiveMode  ReceiveMode { get; set; }
        public long SequenceNumber { get; set; }
    }
}
