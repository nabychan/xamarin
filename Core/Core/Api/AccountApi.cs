using System;
using System.Collections.Generic;
using System.Text;
using Core.Http;
namespace Core.Api
{
    public class AccountApi : Api
    {
        public AccountApi()
        {
            Url = "/Account/Login";
        }
        public ModelBase<Model> User(string username, string password)
        {
            return Request<ModelBase<Model>>(HttpMethod.GET,null, new Dictionary<string, string> { { nameof(username) , username }, { nameof(password), password } });
        }
    }

    public class Model
    {
    public string access_token { get; set; }
    public string expires_in { get; set; }
    public string refresh_token { get; set; }
    public long user_id { get; set; }
    }
    public class ModelBase<T>
    {
     public     string state_code{ get; set; }
    public string message { get; set; }
    public string help_link { get; set; }
    public T data { get; set; }
    }
}
