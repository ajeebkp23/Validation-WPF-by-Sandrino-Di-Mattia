using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using POC.Validatie.Model.Validation;

namespace POC.Validatie.Model
{
    public class Person
    {
        [TextValidation(MinLength = 10)]
        public string Name { get; set; }

        [NumberValidation]
        public string Age { get; set; }

        [NumberValidation]
        [TextValidation(MinLength = 2)]
        public string Money { get; set; }
    }
}