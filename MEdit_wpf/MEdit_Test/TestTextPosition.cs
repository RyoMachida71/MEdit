using MEdit_wpf;

namespace MEdit_Test {
    public class TestTextPosition {

        [TestCase(0, 0, 0, 0, true, TestName = "Equal")]
        [TestCase(0, 0, 1, 1, false, TestName = "NotEqual")]
        public void TestTextPositionEquality(int row1, int col1, int row2, int col2, bool expected) {
            var one = new TextPosition(row1, col1);
            var other = new TextPosition(row2, col2);
            Assert.That(one.Equals(other), Is.EqualTo(expected));
        }

        [Test]
        public void TestNotEqualOperator() {
            var one = new TextPosition(0, 1);
            var other = new TextPosition(1, 0);
            Assert.That(one != other, Is.True);
        }

        [TestCase(0, 1, 0, 2, true, TestName = "CompareOneLine")]
        [TestCase(0, 1, 1, 0, true, TestName = "CompareLines")]
        [TestCase(1, 0, 0, 0, false, TestName = "FalseCase")]
        public void TestCompareIsLargerThan(int row1, int col1, int row2, int col2, bool expected) {
            var one = new TextPosition(row1, col1);
            var other = new TextPosition(row2, col2);
            Assert.That(one < other, expected ? Is.True : Is.False);
        }

        [TestCase(0, 1, 0, 1, true, TestName = "Equal")]
        [TestCase(0, 2, 1, 1, true, TestName = "LargerThan")]
        [TestCase(1, 2, 0, 1, false, TestName = "FalseCase")]
        public void TestCompareIsLargerThanOrEqualTo(int row1, int col1, int row2, int col2, bool expected) {
            var one = new TextPosition(row1, col1);
            var other = new TextPosition(row2, col2);
            Assert.That(one <= other, expected ? Is.True : Is.False);
        }

        [TestCase(0, 2, 0, 1, true, TestName = "CompareOneLine")]
        [TestCase(1, 0, 0, 1, true, TestName = "CompareLines")]
        [TestCase(0, 0, 1, 0, false, TestName = "FalseCase")]
        public void TestCompareIsLessThan(int row1, int col1, int row2, int col2, bool expected) {
            var one = new TextPosition(row1, col1);
            var other = new TextPosition(row2, col2);
            Assert.That(one > other, expected ? Is.True : Is.False);
        }

        [TestCase(0, 1, 0, 1, true, TestName = "Equal")]
        [TestCase(1, 1, 0, 1, true, TestName = "LessThan")]
        [TestCase(0, 1, 1, 2, false, TestName = "FalseCase")]
        public void TestCompareIsLessThanOrEqualTo(int row1, int col1, int row2, int col2, bool expected) {
            var one = new TextPosition(row1, col1);
            var other = new TextPosition(row2, col2);
            Assert.That(one >= other, expected ? Is.True : Is.False);
        }
    }
}
