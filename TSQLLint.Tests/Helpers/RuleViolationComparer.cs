using System;
using System.Collections;
using System.Collections.Generic;
using TSQLLint.Lib.Rules.RuleViolations;

namespace TSQLLint.Tests.Helpers
{
    public class RuleViolationComparer : IComparer, IComparer<RuleViolation>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as RuleViolation;
            var rhs = y as RuleViolation;

            if (lhs == null || rhs == null)
            {
                throw new InvalidOperationException("cannot compare null object");
            }

            return Compare(lhs, rhs);
        }

        public int Compare(RuleViolation x, RuleViolation y)
        {
            if (x.Line != y.Line)
            {
                return -1;
            }

            if (x.Column != y.Column)
            {
                return -1;
            }

            if (x.RuleName != y.RuleName)
            {
                return -1;
            }

            return 0;
        }
    }
}
