using Spring.IO;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using System.IO;

namespace Chat.FileExtensions
{
    public static class DropboxUploader
    {
        public static string DropboxAppKey = "tosgsn5l5f5dqd7";
        public static string DropboxAppSecret = "07w4xxmvmbk8yp7";

        public static DropboxServiceProvider dropboxServiceProvider =
            new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

        public static string[] oAuthToken = new string[]
                                            {   
                                                "qp8vh6xewiqemowq",
                                                "vn7h561eeb5hdx9"
                                            };

        public static string DropboxShareFile(Stream stream, string filename)
        {
            //OAuthToken oauthAccessToken = LoadOAuthToken();
            IDropbox dropbox = dropboxServiceProvider.GetApi(oAuthToken[0], oAuthToken[1]);

            Entry uploadFileEntry = dropbox.UploadFileAsync(new StreamResource(stream)
                , filename).Result;

            var sharedUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;
            return (sharedUrl.Url + "?dl=1"); // we can download the file directly
        }   
    }
}
