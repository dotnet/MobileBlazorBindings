namespace ComponentWrapperGenerator
{
    public class GeneratorResult
    {
        public FileResult Component { get; set; }
        public FileResult ComponentHandler { get; set; }
    }

    public class FileResult
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}