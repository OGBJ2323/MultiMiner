﻿using MultiMiner.Xgminer.Api.Data;
using System.Collections.Generic;
using System.Linq;

namespace MultiMiner.Xgminer.Api.Parsers
{
    class SummaryInformationParser : ResponseTextParser
    {
        public static void ParseTextForSummaryInformation(string text, SummaryInformation summaryInformation)
        {
            List<string> summaryBlob = text.Split('|').ToList();
            summaryBlob.RemoveAt(0);

            if (summaryBlob.Count == 0)
                return;

            string summaryText = summaryBlob.First();

            if (summaryText == "\0")
                return;

            //bfgminer may have multiple entries for the same key, e.g. Hardware Errors
            //seen with customer data/hardware
            //remove dupes using Distinct()
            var summaryAttributes = summaryText.Split(',').ToList().Distinct();

            Dictionary<string, string> keyValuePairs = summaryAttributes
                .Where(value => value.Contains('='))
                .Select(value => value.Split('='))
                .ToDictionary(pair => pair[0], pair => pair[1]);

            //seen Count == 0 with user API logs
            if (keyValuePairs.Count > 0)
            {
                summaryInformation.Elapsed = TryToParseInt(keyValuePairs, "Elapsed", 0);
                summaryInformation.AverageHashrate = TryToParseDouble(keyValuePairs, "MHS av", 0);
                summaryInformation.FoundBlocks = TryToParseInt(keyValuePairs, "Found Blocks", 0);
                summaryInformation.GetWorks = TryToParseInt(keyValuePairs, "Getworks", 0);
                summaryInformation.AcceptedShares = TryToParseInt(keyValuePairs, "Accepted", 0);
                summaryInformation.RejectedShares = TryToParseInt(keyValuePairs, "Rejected", 0);
                summaryInformation.HardwareErrors = TryToParseInt(keyValuePairs, "Hardware Errors", 0);
                summaryInformation.Utility = TryToParseDouble(keyValuePairs, "Utility", 0);
                summaryInformation.DiscardedShares = TryToParseInt(keyValuePairs, "Discarded", 0);
                summaryInformation.StaleShares = TryToParseInt(keyValuePairs, "Stale", 0);
                summaryInformation.GetFailures = TryToParseInt(keyValuePairs, "Get Failures", 0);
                summaryInformation.LocalWork = TryToParseInt(keyValuePairs, "Local Work", 0);
                summaryInformation.RemoteFailures = TryToParseInt(keyValuePairs, "Remote Failures", 0);
                summaryInformation.NetworkBlocks = TryToParseInt(keyValuePairs, "Network Blocks", 0);
                summaryInformation.TotalHashrate = TryToParseDouble(keyValuePairs, "Total MH", 0);
                summaryInformation.WorkUtility = TryToParseDouble(keyValuePairs, "Work Utility", 0);
                summaryInformation.DifficultyAccepted = TryToParseDouble(keyValuePairs, "Difficulty Accepted", 0);
                summaryInformation.DifficultyRejected = TryToParseDouble(keyValuePairs, "Difficulty Rejected", 0);
                summaryInformation.DifficultyStale = TryToParseDouble(keyValuePairs, "Difficulty Stale", 0);
                summaryInformation.BestShare = TryToParseDouble(keyValuePairs, "Best Share", 0);
                summaryInformation.DeviceHardwarePercent = TryToParseInt(keyValuePairs, "Device Hardware", 0);
                summaryInformation.DeviceRejectedPercent = TryToParseInt(keyValuePairs, "Device Rejected", 0);
                summaryInformation.PoolRejectedPercent = TryToParseInt(keyValuePairs, "Pool Rejected", 0);
                summaryInformation.PoolStatePercent = TryToParseInt(keyValuePairs, "Pool Stale", 0);
            }            
        }
    }
}
