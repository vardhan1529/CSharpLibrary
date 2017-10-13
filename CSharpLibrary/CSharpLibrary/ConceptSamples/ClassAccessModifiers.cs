namespace CSharpLibrary.ConceptSamples
{
    class ClassAccessModifiers
    {
        public class Base
        {
            public Base(int i)
            {

            }
        }

        public static class NestedClass
        {
            private class NC
            {
            }
        }

        sealed class SealedClass
        {
            public void SampleMethod()
            {

            }
        }

        public class ST
        {
            public string s { get; set; }
            public readonly static string t;
            static ST()
            {
                t = "Static field";
            }

            public ST() : this(string.Empty)
            {
            }

            public ST(string s)
            {
                this.s = s;
            }
        }

    }
}
