﻿using MEdit_wpf;
using MEdit_wpf.Selection;

namespace MEdit_Test {
    public class TestSelection {
        [Test]
        public void TestSingleSelection() {
            var sel = new SingleSelection(new TextDocument("selection test"));
            sel.StartOrExtend(new TextPosition(0, 5), new TextPosition(0, 10));
            Assert.That(sel.StartPosition, Is.EqualTo(new TextPosition(0, 5)));
            Assert.That(sel.EndPosition, Is.EqualTo(new TextPosition(0, 10)));
            Assert.That(sel.SelectedText, Is.EqualTo("tion "));

            sel.StartOrExtend(TextPosition.Empty, new TextPosition(0, 11));
            Assert.That(sel.StartPosition, Is.EqualTo(new TextPosition(0, 5)));
            Assert.That(sel.EndPosition, Is.EqualTo(new TextPosition(0, 11)));
            Assert.That(sel.SelectedText, Is.EqualTo("tion t"));

            sel.Unselect();
            Assert.That(sel.StartPosition, Is.EqualTo(new TextPosition(0, 11)));
            Assert.That(sel.EndPosition, Is.EqualTo(new TextPosition(0, 11)));
            Assert.That(sel.SelectedText, Is.EqualTo(""));
        }
    }
}
