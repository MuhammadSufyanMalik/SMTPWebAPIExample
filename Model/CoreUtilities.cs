namespace SMTPWebAPIExample.Model
{
    public static class CoreUtilities
    {
        public static byte[] FormFileToByte(this IFormFile file)
        {
            byte[] fileContent;
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            fileContent = ms.ToArray();
            return fileContent;
        }
    }
}
