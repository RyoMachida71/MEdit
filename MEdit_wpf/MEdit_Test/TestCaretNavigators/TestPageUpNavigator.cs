using MEdit_wpf;
using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using Moq;

namespace MEdit_Test.TestCaretNavigators {
    public class TestPageUpNavigator {
        [Test]
        public void TestPageUp() {
            var mockVisualText = new Mock<IVisualTextInfo>();
            mockVisualText.SetupGet(x => x.LineHeight).Returns(10);
            var mockTextArea = new Mock<ITextArea>();
            mockTextArea.SetupGet(x => x.ActualHeight).Returns(100);
            mockTextArea.SetupGet(x => x.VisualTextInfo).Returns(mockVisualText.Object);

            var doc = new TextDocument("1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\n16\r\n17\r\n18\r\n19\r\n20\r\n21\r\n22\r\n23\r\n24\r\n25\r\n26\r\n27\r\n28\r\n29\r\n30\r\n31\r\n32\r\n33\r\n34\r\n35\r\n36\r\n37\r\n38\r\n39\r\n40\r\n41\r\n42\r\n43\r\n44\r\n45\r\n46\r\n47\r\n48\r\n49\r\n50");
            var navigator = new PageUpNavigator(mockTextArea.Object);
            var position = navigator.GetNextPosition(new TextPosition(50, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(40, 0)));
        }

        [Test]
        public void TestReturnCurrentPositionAtFirstPosition() {
            var mockVisualText = new Mock<IVisualTextInfo>();
            mockVisualText.SetupGet(x => x.LineHeight).Returns(10);
            var mockTextArea = new Mock<ITextArea>();
            mockTextArea.SetupGet(x => x.ActualHeight).Returns(100);
            mockTextArea.SetupGet(x => x.VisualTextInfo).Returns(mockVisualText.Object);

            var doc = new TextDocument("1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\n16\r\n17\r\n18\r\n19\r\n20\r\n21\r\n22\r\n23\r\n24\r\n25\r\n26\r\n27\r\n28\r\n29\r\n30\r\n31\r\n32\r\n33\r\n34\r\n35\r\n36\r\n37\r\n38\r\n39\r\n40\r\n41\r\n42\r\n43\r\n44\r\n45\r\n46\r\n47\r\n48\r\n49\r\n50");
            var navigator = new PageUpNavigator(mockTextArea.Object);
            var position = navigator.GetNextPosition(new TextPosition(0, 6), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 6)));
        }
    }
}