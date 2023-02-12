namespace flipper_wasm.Processor;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;

public class ImageFlipperService : IImageFlipperService
{
    private Image? _loadedImage;
    private Task<bool> loadImage(byte[] imageData) {
        try {
            this._loadedImage = Image.Load<Rgba32>(imageData);   
        }
        catch (Exception) {
            return Task.FromResult(false);
        } 
        return Task.FromResult(true);
    }

    public async Task<MemoryStream> FlipImage(Stream inputImageStream) {

        var inputBytes = new byte[inputImageStream.Length];
        var tempStream = new MemoryStream();

        await inputImageStream.CopyToAsync(tempStream);
       
        var image = await loadImage(tempStream.ToArray());
        var stream = new MemoryStream();
        
        this._loadedImage?
            .Mutate(x => x.Flip(FlipMode.Horizontal));

        var encoder = new PngEncoder();    

        this._loadedImage?
            .Save(stream,encoder);

        return stream;
    }

}
