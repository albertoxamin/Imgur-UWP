using Newtonsoft.Json;

namespace imgur.ImgurAPI
{
    public class ImgurAtomicImageData
    {
        [JsonProperty(PropertyName = "data")]
        public ImgurImage Image { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
    }
}
