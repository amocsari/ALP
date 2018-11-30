﻿namespace ALP.ViewModel
{
    public interface IDialogViewModel<TReturnParameter, TParameter>
    {
        TParameter Parameter { get; set; }
        TReturnParameter ReturnParameter { get; set; }
    }
}