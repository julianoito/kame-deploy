﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kame.Core.Entity
{
    [Serializable]
    public class StepParameter : Parameter
    {
        public string StepID { get; set; }

        private StepParameter() : base()
        {
        }

        public static StepParameter NewStepParameter(string parameterKey, string parameterValue, string parameterDescription)
        {
            return new StepParameter() { 
                EntityState = System.Data.EntityState.Added
                ,ParameterID = Guid.NewGuid().ToString()
                ,ParameterKey = parameterKey
                ,ParameterValue = parameterValue
                ,ParameterDescription = parameterDescription
            };
        }
    }
}
