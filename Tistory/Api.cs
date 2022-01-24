using AutoMoney;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Tistory.Model;

namespace Tistory
{

    public class Api
    {
        public readonly string AppID;
        public readonly string SecretKey;
        public readonly string CallBack;
        public readonly string BlogName;
        private string accessToken;
        private TistoryAPISharp.TistoryAPI TistoryAPI = new TistoryAPISharp.TistoryAPI();

        public Api(string appId, string secertKey, string callBack, string blogName)
        {
            this.AppID = appId;
            this.SecretKey = secertKey;
            this.CallBack = callBack;
            this.BlogName = blogName;
            TistoryAPI.SetClientID(AppID);
        }
        public string GetAuthCodeUrl()
        {
            var sb = new StringBuilder();
            sb.Append($"https://www.tistory.com/oauth/authorize?");
            sb.Append($"client_id={AppID}");
            sb.Append($"&redirect_uri={CallBack}");
            sb.Append($"&response_type=code");
            sb.Append($"&state=");
            return sb.ToString();
        }
        public void SetAccessToken(string authCode)
        {
            if (authCode.IsNullOrWhiteSpace())
            {
                return;
            }
            if (this.accessToken.IsNullOrWhiteSpace() == false)
            {
                return;
            }
            var sb = new StringBuilder();
            sb.Append($"https://www.tistory.com/oauth/access_token?");
            sb.Append($"client_id={AppID}&");
            sb.Append($"client_secret={SecretKey}&");
            sb.Append($"redirect_uri={CallBack}&");
            sb.Append($"code={authCode}&");
            sb.Append($"grant_type=authorization_code");
            string address = sb.ToString();
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(address);
            httpWebRequest.Method = "GET";
            string readToEnd = httpWebRequest.ReadToEnd();
            var token = readToEnd.Push("=");
            this.accessToken = token;
            TistoryAPI.SetAccessToken(token);
        }
        public BlogInfo GetBlogInfo()
        {
            BlogInformation blogInformation = TistoryAPI.GetBlogInformation(TistoryAPISharp.TistoryAPI.OutputStyle.JSON).ToClass<BlogInformation>();
            if (blogInformation is null)
            {
                throw new Exception("설정한 블로그를 찾을 수 없습니다.");
            }
            BlogInfo blogInfo = blogInformation.tistory.item.blogs.Find(x => x.name == this.BlogName);
            if (blogInfo is null)
            {
                throw new Exception("설정한 블로그를 찾을 수 없습니다.\n블로그 이름을 확인해주세요.");
            }
            return blogInfo;
        }

        public Model.Category GetCategory()
        {
            return this.TistoryAPI.GetCategory(this.BlogName, TistoryAPISharp.TistoryAPI.OutputStyle.JSON).ToClass<Model.Category>();
        }

        public List<Post> GetPostList(int page = 1)
        {
            var getPostList = this.TistoryAPI.GetPostList(this.BlogName, page, TistoryAPISharp.TistoryAPI.OutputStyle.JSON).ToClass<PostInfo>();
            List<Post> postList = getPostList.tistory.item.posts;
            return postList;
        }

        public PostResult WritePost(Product product)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("https://www.tistory.com/apis/post/write?");
                sb.Append($"access_token={this.accessToken}");
                sb.Append($"&output=json");
                sb.Append($"&blogName={this.BlogName}");
                sb.Append($"&title={product.title}");
                sb.Append($"&content={product.html}");
                sb.Append($"&visibility=Public");
                sb.Append($"&category=0");
                sb.Append($"&published=\"\"");
                sb.Append($"&slogan=\"asd\"");
                sb.Append($"&tag=\"\"");
                sb.Append($"&acceptComment=Deny");
                sb.Append($"&password=\"\"");
                string address = sb.ToString();
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp(address);
                httpWebRequest.Method = "POST";
                var read = httpWebRequest.ReadToEnd();
                var result = read.ToClass<PostResult>();
                return result;
            }
            catch (WebException webException)
            {
                var error = webException.ReadToEnd();
            }
            return null;
            //return this.TistoryAPI.WritePost(this.BlogName, product.title, product.html, null,
            //                                   Visibillity.Public, AcceptComment.Deny,
            //                                   "0", "", "", "", OutputStyle.JSON).ToClass<PostResult>();
        }
        public object GetList(string authCode, string blogName, int pageNum = 1)
        {
            return null;
        }
        public ImageResult UploadImage(string imageUrl, CookieContainer cookieContainer)
        {
            ImageResult result = null;
            try
            {
                byte[] getImageBytes = imageUrl.GetImageBytes();
                string boundary = "----WebKitFormBoundary0M6ujQMWumqcWzcq";
                var multiPartBuilder = new MultiPartBuilder(boundary);
                multiPartBuilder.Add("file", "text.png", "image/png", getImageBytes);
                byte[] getBytes = multiPartBuilder.GetBytes(Encoding.UTF8, MultiPartBuilder.ImageType.Bytes);

                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://mostitem.tistory.com/manage/post/attach.json");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.Host = "mostitem.tistory.com";
                httpWebRequest.Write(getBytes);
                var readToEnd = httpWebRequest.ReadToEnd<ImageResult>();
                result = readToEnd;
            }
            catch (WebException webException)
            {
                Console.WriteLine(webException.ReadToEnd());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return result;

        }

    }
}
