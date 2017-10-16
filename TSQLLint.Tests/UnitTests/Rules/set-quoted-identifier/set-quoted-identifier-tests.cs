using System;
using System.Collections.Generic;
using NUnit.Framework;
using TSQLLint.Lib.Rules;
using TSQLLint.Lib.Rules.RuleViolations;

namespace TSQLLint.Tests.UnitTests.Rules
{
    public class SetQuotedIdentifierRuleTests
    {
        private static readonly object[] testCases = 
        {
            new object[]
            {
                "set-quoted-identifier", "set-quoted-identifier-no-error",  typeof(SetQuotedIdentifierRule), new List<RuleViolation>()
            },
            new object[]
            {
                "set-quoted-identifier", "set-quoted-identifier-one-error", typeof(SetQuotedIdentifierRule), new List<RuleViolation>
                {
                    new RuleViolation("set-quoted-identifier", 1, 1)
                }
            }
        };
        
        [Test, TestCaseSource(nameof(testCases))]
        public void TestRule(string rule, string testFileName, Type ruleType, List<RuleViolation> expectedRuleViolations)
        {
            RulesTestHelper.RunRulesTest(rule, testFileName, ruleType, expectedRuleViolations);
        }
    }
}
