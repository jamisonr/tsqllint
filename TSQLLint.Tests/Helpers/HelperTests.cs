using System;
using System.Collections.Generic;
using NUnit.Framework;
using TSQLLint.Lib.Rules.RuleViolations;

namespace TSQLLint.Tests.Helpers
{
    public class RuleViolationCompareTests
    {
        private readonly RuleViolationComparer _ruleViolationComparer = new RuleViolationComparer();

        public static readonly object[] LineComparison = 
        {
          new object[] 
              {
                  new List<RuleViolation>
                  {
                      new RuleViolation(ruleName: "some-rule", startLine: 99, startColumn: 0),
                      new RuleViolation(ruleName: "some-rule", startLine: 0, startColumn: 0)
                  }
          },
          new object[] 
          {
              new List<RuleViolation>
              {
                  new RuleViolation(ruleName: "some-rule", startLine: 0, startColumn: 99),
                  new RuleViolation(ruleName: "some-rule", startLine: 0, startColumn: 0)
              }
          },
          new object[] 
          {
              new List<RuleViolation>
              {
                  new RuleViolation(ruleName: "some-rule", startLine: 0, startColumn: 0),
                  new RuleViolation(ruleName: "foo", startLine: 0, startColumn: 0)
              }
          }
        };

        [Test, TestCaseSource("LineComparison")]
        public void RuleTests(List<RuleViolation> ruleViolations)
        {
            Assert.AreEqual(-1, _ruleViolationComparer.Compare(ruleViolations[0], ruleViolations[1]));
        }

        [Test]
        public void RuleCompareShouldThrow()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _ruleViolationComparer.Compare(new object(), new object()));

            Assert.That(ex.Message, Is.EqualTo("cannot compare null object"));
        }
    }
}
