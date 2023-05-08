using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace ArticlesViewer.Infrastructure;

public class BlobRepository : IBlobRepository
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobRepository(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task DeleteBlobFileAsync(string name,
        ContainerType blobType = ContainerType.Images)
    {
        var client = _blobServiceClient.GetBlobContainerClient(blobType.ToString().ToLower());
        var blobClient = client.GetBlobClient(name);
        await blobClient.DeleteIfExistsAsync();
    }

    public async Task<BlobObject?> GetBlobFileAsync(string? name = "user-default",
        ContainerType blobType = ContainerType.Images)
    {
        var client = _blobServiceClient.GetBlobContainerClient(blobType.ToString().ToLower());
        var blobClient = client.GetBlobClient(name);
        if (await blobClient.ExistsAsync())
        {
            var content = await blobClient.DownloadContentAsync();
            var filestream = content.Value.Content.ToStream();
            filestream.Position = 0;
            var blobProperties = await blobClient.GetPropertiesAsync();

            string contentType = blobProperties.Value.ContentType;
            return new BlobObject() { File = filestream, ContentType = contentType };
        }

        return null;
    }

    public async Task<string> UploadBlobFileAsync(IFormFile file, string name,
        ContainerType blobType = ContainerType.Images)
    {
        var client = _blobServiceClient.GetBlobContainerClient(blobType.ToString().ToLower());
        using var filestream = new MemoryStream();
        await file.CopyToAsync(filestream);

        filestream.Position = 0;

        var blobClient = client.GetBlobClient(name);
        var response = await blobClient.UploadAsync(filestream, new BlobUploadOptions()
        { HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType } });

        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<string> UploadBlobTextAsync(string text, string name, ContainerType blobType)
    {
        var client = _blobServiceClient.GetBlobContainerClient(blobType.ToString().ToLower());

        byte[] byteArray = Encoding.UTF8.GetBytes(text);

        var stream = new MemoryStream(byteArray);

        var blobClient = client.GetBlobClient(name);
        await blobClient.UploadAsync(stream, new BlobUploadOptions()
        { HttpHeaders = new BlobHttpHeaders { ContentType = "plain/text" } });

        return blobClient.Uri.AbsoluteUri;
    }
}

public class FakeBlobRepository : IBlobRepository
{
    public Task DeleteBlobFileAsync(string name, ContainerType blobType = ContainerType.Images)
    {
        return Task.CompletedTask;
    }

    public Task<BlobObject?> GetBlobFileAsync(string name, ContainerType blobType = ContainerType.Images)
    {
        return Task.FromResult(null as BlobObject);
    }

    public Task<string> UploadBlobFileAsync(IFormFile imageFile, string name, ContainerType blobType = ContainerType.Images)
    {
        return Task.FromResult(null as string);
    }

    public Task<string> UploadBlobTextAsync(string text, string name, ContainerType blobType = ContainerType.Images)
    {
        return Task.FromResult(null as string);
    }
}