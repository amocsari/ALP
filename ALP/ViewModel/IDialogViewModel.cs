namespace ALP.ViewModel
{
    /// <summary>
    /// Dialog with parameter and return parameter
    /// </summary>
    /// <typeparam name="TReturnParameter"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public interface IDialogViewModel<TReturnParameter, TParameter>
    {
        TParameter Parameter { get; set; }
        TReturnParameter ReturnParameter { get; set; }
    }

    /// <summary>
    /// dialog with only parameter
    /// </summary>
    /// <typeparam name="Tparameter"></typeparam>
    public interface IDialogViewModel<Tparameter>
    {
        Tparameter Parameter { get; set; }
    }
}
