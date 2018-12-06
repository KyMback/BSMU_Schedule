using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSMU_Schedule.Common.ActionResults
{
    public class ActionResult
    {
        public IList<ActionError> Errors { get; set; }

        public bool Succeeded => Errors == null || !Errors.Any();

        public void AddError(ActionError actionError)
        {
            if (Errors == null)
            {
                Errors = new List<ActionError>();
            }

            Errors.Add(actionError);
        }
    }
}
