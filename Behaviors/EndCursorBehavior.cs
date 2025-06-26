using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Behaviors
{
    public class EndCursorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.Focused += OnEntryFocused!;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Focused -= OnEntryFocused!;
            base.OnDetachingFrom(entry);
        }

        private async void OnEntryFocused(object sender, FocusEventArgs e)
        {
            await Task.Delay(100);

            if (sender is Entry entry && entry.IsFocused)
            {
                // Move cursor to end and optionally select all text
                entry.CursorPosition = entry.Text?.Length ?? 0;
            }
        }
    }
}