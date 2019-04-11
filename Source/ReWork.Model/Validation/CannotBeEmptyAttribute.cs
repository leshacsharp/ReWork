using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReWork.Model.Validation
{
    public class CannotBeEmptyAttribute : ValidationAttribute
    {    
        public override bool IsValid(object value)
        {
            ICollection collection = value as ICollection;
            if (collection != null)
                return collection.Count != 0;
            

            IEnumerable enumerable = value as IEnumerable;
            return enumerable != null && enumerable.GetEnumerator().MoveNext();
        }
    }
}
