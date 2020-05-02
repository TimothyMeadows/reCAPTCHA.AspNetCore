using System.Collections.Generic;

namespace System.CodeDom.Compiler
{
    public class CompilerError
    {
        public string ErrorText;
        public bool IsWarning;
    }

    public class CompilerErrorCollection : List<CompilerError>
    {
        // This is a place holder class, it most likely should be included in dotnet core it's self but for some reason it is not.
    }
}