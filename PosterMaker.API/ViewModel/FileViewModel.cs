namespace PosterMaker.API.ViewModel
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
