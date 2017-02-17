﻿/*
 * Copyright © 2016 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */
using Newtonsoft.Json.Linq;
using System.Linq;

namespace EDDiscovery.EliteDangerous.JournalEvents
{
//    When Written: when claiming payment for combat bounties and bonds
//Parameters:
//•	Type
//•	Amount: (Net amount received, after any broker fee)
//•	BrokerPercenentage

    public class JournalRedeemVoucher : JournalEntry, ILedgerJournalEntry
    {
        public JournalRedeemVoucher(JObject evt) : base(evt, JournalTypeEnum.RedeemVoucher)
        {
            Type = JSONHelper.GetStringDef(evt["Type"]);
            Amount = JSONHelper.GetLong(evt["Amount"]);
            BrokerPercentage = JSONHelper.GetDouble(evt["BrokerPercentage"]);
        }
        public string Type { get; set; }
        public long Amount { get; set; }
        public double BrokerPercentage { get; set; }

        public static System.Drawing.Bitmap Icon { get { return EDDiscovery.Properties.Resources.bounty; } }

        public void Ledger(EDDiscovery2.DB.MaterialCommoditiesLedger mcl, DB.SQLiteConnectionUser conn)
        {
            mcl.AddEvent(Id, EventTimeUTC, EventTypeID, Type + " Broker " + BrokerPercentage.ToString("0.0") + "%", Amount);
        }

    }
}


