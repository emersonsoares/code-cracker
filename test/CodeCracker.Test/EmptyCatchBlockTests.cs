﻿using TestHelper;
using Xunit;

namespace CodeCracker.Test
{
    public class EmptyCatchBlockTests : CodeFixTest<EmptyCatchBlockAnalyzer, EmptyCatchBlockCodeFixProvider>
    {
        string test = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public void Foo()
            {
                try
                {
                   // do something
                }
                catch
                {
                }
            }
        }
    }";
        [Fact]
        public void EmptyCatchBlockAnalyzerCreateDiagnostic()
        {
            var test = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public void Foo()
            {
                try
                {
                   // do something
                }
                catch (Exception ex)
                {
                   throw;
                }
            }
        }
    }";
            VerifyCSharpHasNoDiagnostics(test);
        }

        [Fact]
        public void WhenRemoveTryCatchStatement()
        {

            var fixtest = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public void Foo()
            {
                {
                   // do something
                }
            }
        }
    }";
            VerifyCSharpFix(test, fixtest, 0,false,true);
        }

        [Fact]
        public void WhenRemoveTryCatchStatementAndPutComment()
        {
            var fixtest = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public void Foo()
            {
                {
                   // do something
                }
                //TODO: Consider reading MSDN Documentation about how to use Try...Catch => http://msdn.microsoft.com/en-us/library/0yd65esw.aspx
            }
        }
    }";

            VerifyCSharpFix(test, fixtest, 1, false, true);
        }

        [Fact]
        public void WhenPutExceptionClassInCatchBlock()
        {
            var fixtest = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            public void Foo()
            {
                try
                {
                   // do something
                }
                catch (Exception ex)
                {
                   throw;
                }
            }
        }
    }";

            VerifyCSharpFix(test, fixtest, 2, false, true);
        }
    }
}