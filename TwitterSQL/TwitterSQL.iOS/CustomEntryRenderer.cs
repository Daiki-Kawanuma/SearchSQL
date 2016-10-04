using System;
using System.Collections.Generic;
using System.Text;
using TwitterSQL.Controls;
using TwitterSQL.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace TwitterSQL.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private UITextField _nativeTextField;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                _nativeTextField = Control;
                _nativeTextField.Started += TextFieldStarted;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                _nativeTextField.Started -= TextFieldStarted;
            }
            base.Dispose(disposing);
        }

        private void TextFieldStarted(object sender, EventArgs e)
        {
            var textField = sender as UITextField;
            if (textField != null)
                textField.SelectedTextRange = textField.GetTextRange(textField.EndOfDocument, textField.EndOfDocument);
        }
    }
}