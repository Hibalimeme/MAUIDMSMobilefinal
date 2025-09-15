using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Static
{
    public static class VisualTreeHelper
    {
        public static List<Entry> GetAllEntries(VisualElement root)
        {
            var entries = new List<Entry>();
            TraverseVisualTree(root, entries);
            return entries;
        }

        private static void TraverseVisualTree(VisualElement element, List<Entry> entries)
        {
            if (element is Entry entry)
            {
                entries.Add(entry);
            }

            if (element is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    if (child is VisualElement ve)
                        TraverseVisualTree(ve, entries);
                }
            }
            else if (element is ContentView contentView && contentView.Content is VisualElement ve)
            {
                TraverseVisualTree(ve, entries);
            }
            else if (element is ScrollView scrollView && scrollView.Content is VisualElement ve2)
            {
                TraverseVisualTree(ve2, entries);
            }
        }
         public static List<Label> GetAllLabeles(VisualElement root)
        {
            var labels = new List<Label>();
            TraverseVisualTreeLabel(root, labels);
            return labels;
        }

        private static void TraverseVisualTreeLabel(VisualElement element, List<Label> labels)
        {
            if (element is Label label)
            {
                labels.Add(label);
            }

            if (element is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    if (child is VisualElement ve)
                        TraverseVisualTreeLabel(ve, labels);
                }
            }
            else if (element is ContentView contentView && contentView.Content is VisualElement ve)
            {
                TraverseVisualTreeLabel(ve, labels);
            }
            else if (element is ScrollView scrollView && scrollView.Content is VisualElement ve2)
            {
                TraverseVisualTreeLabel(ve2, labels);
            }
        }
    }
}
