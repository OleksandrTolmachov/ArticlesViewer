using ArticlesViewer.Domain;
using Microsoft.AspNetCore.Http;

namespace ArticlesViewer.Application.RepositoryContracts;
public enum ContainerType
{
    Images,
    ArticlesContent
}

public interface IBlobRepository
{
    public Task<IBlobObject?> GetBlobFileAsync(string name, ContainerType blobType = ContainerType.Images);
    public Task<string> UploadBlobFileAsync(IFormFile imageFile, string name, ContainerType blobType = ContainerType.Images);
    public Task<string> UploadBlobTextAsync(string text, string name, ContainerType blobType = ContainerType.Images);
    public Task DeleteBlobFileAsync(string name, ContainerType blobType = ContainerType.Images);
}



