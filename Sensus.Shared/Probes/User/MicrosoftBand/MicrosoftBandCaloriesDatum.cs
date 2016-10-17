﻿// Copyright 2014 The Rector & Visitors of the University of Virginia
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Sensus.Shared.Anonymization;
using Sensus.Shared.Anonymization.Anonymizers;
using Sensus.Shared.Probes.User.Scripts.ProbeTriggerProperties;

namespace Sensus.Shared.Probes.User.MicrosoftBand
{
    public class MicrosoftBandCaloriesDatum : Datum
    {
        private double _calories;

        [Anonymizable(null, new Type[] { typeof(DoubleRoundingOnesAnonymizer), typeof(DoubleRoundingTensAnonymizer), typeof(DoubleRoundingHundredsAnonymizer) }, -1)]
        [DoubleProbeTriggerProperty]
        public double Calories
        {
            get
            {
                return _calories;
            }

            set
            {
                _calories = value;
            }
        }

        public override string DisplayDetail
        {
            get
            {
                return "Calories:  " + Math.Round(_calories, 0);
            }
        }

        /// <summary>
        /// For JSON.net deserialization.
        /// </summary>
        private MicrosoftBandCaloriesDatum()
        {
        }

        public MicrosoftBandCaloriesDatum(DateTimeOffset timestamp, double calories)
            : base(timestamp)
        {
            _calories = calories;
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   "Calories:  " + _calories;
        }
    }
}