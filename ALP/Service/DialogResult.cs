namespace ALP.Service
{
    /// <summary>
    /// Stores the result of the dialogwindow
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class DialogResult<TResult>
    {
        public TResult Value { get; set; }
        public bool Accepted { get; set; }
    }
}
