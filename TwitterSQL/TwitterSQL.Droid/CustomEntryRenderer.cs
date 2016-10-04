using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TwitterSQL.Controls;
using TwitterSQL.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace TwitterSQL.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private EditText _nativeEditText;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                _nativeEditText = Control;
                _nativeEditText.FocusChange += EditTextFocusChange;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                _nativeEditText.FocusChange -= EditTextFocusChange;
            }
            base.Dispose(disposing);
        }

        private void EditTextFocusChange(object sender, FocusChangeEventArgs e)
        {
            var editText = sender as EditText;
            editText?.SetSelection(editText.Text.Length);
        }
    }
}