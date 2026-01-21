namespace CoreWebAPIPract.SOLID_Principles
{
    //Problem
    public class LSPClass
    {
        public virtual string Content {  get; set; }

        public void Read()
        {
            Console.WriteLine(Content);
        }

        public virtual void Write(string content)
        {
            Content = content;
        }
    }

    public class ReadOnlyDoc : LSPClass
    {
        public override void Write(string content)
        {
            throw new InvalidOperationException("can not write to read-only doc");
        }
    }

    public class UseClass
    {
        public void Method()
        {
            LSPClass lSPClass = new LSPClass();
            lSPClass.Write("new content");
        }
    }

}
