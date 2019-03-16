using Fenit.Toolbox.Core.Answers;
using Gat.Controls;

namespace Fenit.HelpTool.UI.Core.Dialog
{
    public class OpenDialog
    {
        private readonly OpenDialogViewModel _viewModel;

        public OpenDialog()
        {
            var openDialog = new OpenDialogView();
            _viewModel = (OpenDialogViewModel) openDialog.DataContext;
        }


        public Response<string> SelectFolder()
        {
            var res = new Response<string>();

            _viewModel.SelectFolder = true;
            _viewModel.IsDirectoryChooser = true;

            var resDialog = _viewModel.Show();
            if (resDialog.HasValue && resDialog.Value && _viewModel.SelectFolder)
                res.AddValue(_viewModel.SelectedFilePath);
            else
                res.AddError("nie wybrano");

            return res;
        }
    }
}