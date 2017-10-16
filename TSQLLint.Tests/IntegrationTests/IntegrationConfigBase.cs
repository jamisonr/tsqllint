using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NSubstitute;
using NUnit.Framework;
using TSQLLint.Common;
using TSQLLint.Console;
using TSQLLint.Lib.Rules.RuleViolations;
using TSQLLint.Tests.Helpers;

namespace TSQLLint.Tests.IntegrationTests
{
    public class IntegrationConfigBase
    {
        protected readonly string DefaultConfigFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".tsqllintrc");

        protected static readonly string TestFileDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\IntegrationTests\TestFiles");
        protected static readonly string TestFileOne = Path.Combine(TestFileDirectory, @"integration-test-one.sql");

        protected static readonly IEnumerable<RuleViolation> TestFileOneRuleViolations = new List<RuleViolation>
        {
            new RuleViolation(ruleName: "conditional-begin-end", startLine: 2, startColumn: 1),
            new RuleViolation(ruleName: "data-compression", startLine: 6, startColumn: 1),
            new RuleViolation(ruleName: "data-type-length", startLine: 13, startColumn: 16),
            new RuleViolation(ruleName: "disallow-cursors", startLine: 17, startColumn: 1),
            new RuleViolation(ruleName: "information-schema", startLine: 20, startColumn: 27),
            new RuleViolation(ruleName: "keyword-capitalization", startLine: 23, startColumn: 1),
            new RuleViolation(ruleName: "multi-table-alias", startLine: 27, startColumn: 10),
            new RuleViolation(ruleName: "object-property", startLine: 38, startColumn: 7),
            new RuleViolation(ruleName: "print-statement", startLine: 42, startColumn: 1),
            new RuleViolation(ruleName: "schema-qualify", startLine: 45, startColumn: 17),
            new RuleViolation(ruleName: "select-star", startLine: 48, startColumn: 8),
            new RuleViolation(ruleName: "semicolon-termination", startLine: 51, startColumn: 31),
            new RuleViolation(ruleName: "set-ansi", startLine: 1, startColumn: 1),
            new RuleViolation(ruleName: "set-nocount", startLine: 1, startColumn: 1),
            new RuleViolation(ruleName: "set-quoted-identifier", startLine: 1, startColumn: 1),
            new RuleViolation(ruleName: "set-transaction-isolation-level", startLine: 1, startColumn: 1),
            new RuleViolation(ruleName: "upper-lower", startLine: 59, startColumn: 8),
        };

        private readonly RuleViolationCompare _comparer = new RuleViolationCompare();

        protected void PerformApplicationTest(List<string> argumentsUnderTest, string expectedMessage, List<RuleViolation> expectedRuleViolations, int expectedFileCount)
        {
            // arrange
            expectedRuleViolations = expectedRuleViolations.OrderBy(o => o.Line).ToList();
            
            var appArgs = argumentsUnderTest.ToArray();
            var testReporter = Substitute.For<IReporter>();

            var reportedViolations = new List<IRuleViolation>();
            testReporter.When(reporter => reporter.ReportViolation(Arg.Any<IRuleViolation>())).Do(x => reportedViolations.Add(x.Arg<IRuleViolation>()));

            var reportedMessages = new List<string>();
            testReporter.When(reporter => reporter.Report(Arg.Any<string>())).Do(x => reportedMessages.Add(x.Arg<string>()));

            var application = new Application(appArgs, testReporter);

            // act
            application.Run();

            // assert
            Assert.AreEqual(expectedRuleViolations.Count, reportedViolations.Count);
            reportedViolations = reportedViolations.OrderBy(o => o.Line).ToList();
            Assert.IsTrue(string.IsNullOrEmpty(expectedMessage) || reportedMessages.Contains(expectedMessage));
            CollectionAssert.AreEqual(expectedRuleViolations, reportedViolations, _comparer);
        }
    }
}
