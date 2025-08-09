namespace Fenit.Toolbox.WPF.UI.Base
{
    public interface IView
    {
        BaseViewModel Model { get; set; }
    }

    public interface IView<T> where T : BaseViewModel
    {
        T Model { get; set; }
    }
}