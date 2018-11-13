namespace ALP.Service
{
    public class DialogResult<TResult>
    {
        public TResult Value { get; set; }
        public bool Accepted { get; set; }
    }
}
