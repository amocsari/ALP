namespace Model.Model
{
    public class AlpApiResponse<T>
    {
        public T Value { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }

    public class AlpApiResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}
