namespace flipper_wasm.Processor;

public interface IImageFlipperService {
    public Task<MemoryStream> FlipImage(Stream stream);

}