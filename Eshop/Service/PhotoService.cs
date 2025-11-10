using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class PhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty");

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Quality("auto").FetchFormat("auto")
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception(result.Error?.Message ?? "Upload failed");

        return result.SecureUrl.ToString();
    }

   
}
