using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace MEdit {
    public sealed partial class TextArea : UserControl {

        private CoreTextEditContext _context;

        public string DisplayText { get; set; } = "";
        public TextArea() {
            this.InitializeComponent();
            var wnd = CoreApplication.GetCurrentView().CoreWindow;
            wnd.PointerPressed += TextEditArea_PointerPressed;

            CoreTextServicesManager manager = CoreTextServicesManager.GetForCurrentView();
            _context = manager.CreateEditContext();
            _context.InputScope = CoreTextInputScope.Text;
            _context.TextRequested += TextEditArea_TextRequested;
            _context.TextUpdating += TextEditArea_TextUpdating;
            _context.SelectionRequested += (s, e) => { };
            _context.SelectionUpdating += (s, e) => { };
            _context.LayoutRequested += (s, e) => { };
            _context.CompositionStarted += (s, e) => { };
            _context.CompositionCompleted+= (s, e) => { };

            this.GotFocus += TextArea_GotFocus;
        }

        private void TextArea_GotFocus(object sender, RoutedEventArgs args) {
            _context.NotifyFocusEnter();
        }

        private void TextEditArea_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args) {
            args.DrawingSession.DrawText(DisplayText, 0, 0, Colors.White);
            args.DrawingSession.DrawRectangle(new Windows.Foundation.Rect(0, 0, this.ActualWidth, this.ActualHeight), Colors.White);
        }

        private void TextEditArea_TextRequested(CoreTextEditContext sender, CoreTextTextRequestedEventArgs args) {
            Debug.WriteLine(args.Request.Text);
        }

        private void TextEditArea_TextUpdating(CoreTextEditContext sender, CoreTextTextUpdatingEventArgs args) {
            var newSelection = args.NewSelection;
            // todo:ここでテキストバッファを更新する
            DisplayText += args.Text;
            this.TextEditArea.Invalidate();
        }

        private void TextEditArea_PointerPressed(CoreWindow sender, PointerEventArgs args) {
            this.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            _context.NotifyFocusEnter();
        }
    }
}
