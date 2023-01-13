using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using FluentAssertions;
using myList;

namespace NetLab1_Test
{
    public class myListTests
    {
        [Fact]
        public void Count_1AddingElement_1Returned()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            mylist.Add(4);

            //Assert
            mylist.Should().HaveCount(1);
        }

        [Fact]
        public void Count_1RemovingElement_0Returned()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };

            // Act
            mylist.Remove(1);

            //Assert
            mylist.Should().BeEmpty();
        }

        [Fact]
        public void IsReadOnly_FalseReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };

            // Act
            var IRO = mylist.IsReadOnly;

            //Assert
            IRO.Should().BeFalse();
        }


        [Fact]
        public void EConstructor_OtherGenericCollection_1234List()
        {
            // Arrange
            var list2 = new List<int>() { 1, 2, 3, 4 };
            var mylistToComp = new myList<int>() { 1, 2, 3, 4 };

            // Act
            var mylist = new myList<int>(list2);

            // Assert
            mylist.Should().Equal(mylistToComp);
        }

        [Fact]
        public void EConstructor_OtherGenericCollectionNull_ErrorReturn()
        {
            // Arrange
            List<int> list2 = null;
            var mylist = new myList<int>();

            // Act
            Action action = () => mylist = new myList<int>(list2);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("null collection");
        }

        [Fact]
        public void Enumerator_notCorrectIteration_ErrorReturn()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            var enumerator = mylist.AsEnumerable().GetEnumerator();
            enumerator.Reset();
            object a;
            System.Action action = new Action(() => a = ((System.Collections.IEnumerator)enumerator).Current);

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Enumerator_notCorrectIteration2_NotReturn()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            var enumerator = mylist.AsEnumerable().GetEnumerator();
            enumerator.Reset();
            enumerator.MoveNext();
            object a;
            System.Action action = new Action(() => a = ((System.Collections.IEnumerator)enumerator).Current);

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetNonGenericEnumerator_NOTErrorReturn()
        {
            // Arrange
            myList<int> mylist = new myList<int>() { 1, 2, 3 };


            // Act
            var res = ((IEnumerable)mylist).GetEnumerator();

            // Assert
            res.Should().NotBeOfType<Exception>();
        }

        [Fact]
        public void Enumerator_notCorrectIteration3_notErorr()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2 };

            // Act
            var enumerator = mylist.AsEnumerable().GetEnumerator();
            enumerator.MoveNext();
            int a = 0;
            a = (int)((System.Collections.IEnumerator)enumerator).Current;

            //Assert
            a.Should().Be(1);
        }

        [Fact]
        public void IndexOf_SecondElementInList_1Returned()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };

            // Act
            var res = mylist.IndexOf(2);

            // Assert
            res.Should().Be(1);
        }

        [Fact]
        public void IndexOf_ElementNotinList_0Returned()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };

            // Act
            var res = mylist.IndexOf(5);

            // Assert
            res.Should().Be(-1);
        }

        [Fact]
        public void IndexOf_ElementInEmptyList_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            System.Action action = new Action(() => mylist.IndexOf(0));

            //Assert
            Assert.Throws<Exception>(action);
            action.Should().Throw<Exception>();
        }


        [Fact]
        public void Insert_ElementToOutOfRangeIndex_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };

            // Act
            Action action = new Action(() => mylist.Insert(2, 1));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("index");
        }

        [Fact]
        public void Insert_ElementToEmptyListwithOutOfRangeIndex_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            Action action = new Action(() => mylist.Insert(4, 1));

            // Assert
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Insert_ElementToNegativeIndex_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };

            // Act
            Action action = new Action(() => mylist.Insert(-1, 1));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("index");
        }

        [Fact]
        public void Insert_ElementToNotEmpteList_1()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3, 4 };

            // Act
            mylist.Insert(2, 1);

            //Assert
            mylist.Should().HaveElementAt(2, 1);
        }

        [Fact]
        public void Insert_ElementToBeginOfList_1()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3, 4 };

            // Act
            mylist.Insert(0, 1);

            //Assert
            mylist.Should().HaveElementAt(0, 1);
        }

        [Fact]
        public void Insert_ElementToIndexThatEqualsCount_1()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            mylist.Insert(0, 1);

            //Assert
            mylist.Should().HaveElementAt(0, 1);
        }

        [Fact]
        public void Remove_ElementThatIsNotInList_FalseReturn()
        {
            // Arrange
            var mylist = new myList<object>() { 1, 2, 3, 4 };

            // Act
            var res = mylist.Remove(5);

            //Assert
            res.Should().BeFalse();
        }

        [Fact]
        public void Remove_ElementThatIsInList_TrueReturn()
        {
            // Arrange
            var mylist = new myList<object>() { 1, 2, 3, 4 };

            // Act
            var res = mylist.Remove(4);

            //Assert
            res.Should().BeTrue();
        }

        [Fact]
        public void Remove_ElementFromEmptylist_ErrorReturn()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            System.Action action = new Action(() => mylist.Remove(0));

            //Assert
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void RemoveAt_ElementAtIndexThatOutOfRange_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };

            // Act
            Action action = new Action(() => mylist.RemoveAt(100));

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("index");
        }

        [Fact]
        public void RemoveAt_ElementAtIndexThatNegative_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };

            // Act
            Action action = new Action(() => mylist.RemoveAt(-1));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("index");
        }

        [Fact]
        public void RemoveAt_ElementAtIndexThatInRange_EmptyList()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };

            // Act
            mylist.RemoveAt(0);

            //Assert
            mylist.Should().BeEmpty();
        }

        [Fact]
        public void RemoveAt_ElementAtIndexThatInRangeInListWithData_List()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3, 4 };

            // Act
            mylist.RemoveAt(2);

            //Assert
            mylist.Should().NotContain(3);
        }

        [Fact]
        public void RemoveAt_ElementThatFirstInListWithData_List()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3, 4 };

            // Act
            mylist.RemoveAt(0);

            //Assert
            mylist.Should().NotContain(1);
        }

        [Fact]
        public void RemoveAt_InEmptyList_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>();

            // Act
            Action action = new Action(() => mylist.RemoveAt(5));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void thisintIndexGet_NegativeIndex_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };

            // Act+Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => mylist[-1]);
        }

        [Fact]
        public void thisintIndexSet_NegativeIndex_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };

            // Act+Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => mylist[-1] = 4);
        }

        [Fact]
        public void thisintIndexSet_OutOFRangeIndex_ErrorReturned()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };

            // Act+Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => mylist[100] = 4);
        }

        [Fact]
        public void thisintIndexSet_ElementToNotEmptyList_List()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };
            var mylistToComp = new myList<int>() { 1, 2, 4 };

            // Act
            mylist[2] = 4;

            //Assert
            mylist.Should().Equal(mylistToComp); 
        }

        [Fact]
        public void Add_ElementToNotEmptyList_List()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };
            var mylistToComp = new myList<int>() { 1, 2, 3, 4 };

            // Act
            mylist.Add(4);

            //Assert
            mylist.Should().Equal(mylistToComp);
        }

        [Fact]
        public void Clear_NotEmptyList_EmptyList()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3, 4};
            var mylistToComp = new myList<int>();

            // Act
            mylist.Clear();

            //Assert
            mylist.Should().Equal(mylistToComp);
        }

        [Fact]
        public void Clear_EmptyList_EmptyList()
        {
            // Arrange
            var mylist = new myList<int>();
            var mylistToComp = new myList<int>();

            // Act
            mylist.Clear();

            //Assert
            mylist.Should().Equal(mylistToComp);
        }

        [Fact]
        public void Clear_OneElementList_EmptyList()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            var mylistToComp = new myList<int>();

            // Act
            mylist.Clear();

            //Assert
            mylist.Should().Equal(mylistToComp);
        }

        [Fact]
        public void Contains_ElementThatInList_TrueReturn()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };

            // Act
            var res = mylist.Contains(2);

            //Assert
            res.Should().BeTrue();
        }

        [Fact]
        public void Contains_ElementThatNotInList_FalseReturn()
        {
            // Arrange
            var mylist = new myList<int>() { 1, 2, 3 };

            // Act
            var res = mylist.Contains(0);

            //Assert
            res.Should().BeFalse();
        }

        [Fact]
        public void CopyTo_ArrayThatHaveEnoughSpace_Array()
        {
            // Arrange
            var mylist = new myList<int>() { 0, 1, 2 };
            int[] array = new int[] { 0, 1, 2, 3, 4 };
            int[] arrayToComp = new int[] { 0, 0, 1, 2, 4 };

            // Act
            mylist.CopyTo(array, 1);

            //Assert
            array.Should().Equal(arrayToComp);
        }

        [Fact]
        public void CopyTo_NullArray_ErrorReturn()
        {
            // Arrange
            var mylist = new myList<int>() { 0, 1, 2 };
            int[] array = null;

            //Act
            Action action = new Action(() => mylist.CopyTo(array, 1));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CopyTo_OutOfRangeIndexOFArray_ErrorReturn()
        {
            // Arrange
            var mylist = new myList<int>() { 0, 1, 2 };
            int[] array = new int[4] { 1, 2, 3, 4 };

            // Act
            Action action = new Action(() => mylist.CopyTo(array, 5));

            // Act+Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CopyTo_OutOfRangeIndexNegativeOFArray_ErrorReturn()
        {
            // Arrange
            var mylist = new myList<int>() { 0, 1, 2 };
            int[] array = new int[4] { 1, 2, 3, 4 };

            // Act
            Action action = new Action(() => mylist.CopyTo(array, -1));

            // Act+Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CopyTo_ArrayThatDoesntHaveEnoughSpace_ErrorReturn()
        {
            // Arrange
            var mylist = new myList<int>() { 0, 1, 2 };
            int[] array = new int[2] { 1, 2 };

            // Act
            Action action = new Action(() => mylist.CopyTo(array, 0));

            // Act+Assert
            action.Should().Throw<ArgumentException>();
        }


        [Fact]
        public void EventCount_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>();
            mylist.Notify += CompareMessage;

            // Act
            var n = mylist.Count;

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("Count is executing; ");
            };
        }

        [Fact]
        public void EventIsReadOnly_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>();
            mylist.Notify += CompareMessage;

            // Act
            var n = mylist.IsReadOnly;

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("IsReadOnly is executing; ");
            }
        }

        [Fact]
        public void EventIndexOf_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            var n = mylist.IndexOf(0);

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("IndexOf is executing; ");
            };
        }

        [Fact]
        public void EventInsert_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            mylist.Insert(0, 1);

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("Insert is executing; ");
            };
        }

        [Fact]
        public void EventRemoveAt_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            mylist.RemoveAt(0);

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("RemoveAt is executing; ");
            };
        }

        [Fact]
        public void EventIndexGet_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            var n = mylist[0];

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("indexGet is executing; ");
            };
        }

        [Fact]
        public void EventIndexSet_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            mylist[0] = 1;

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("indexSet is executing; ");
            };
        }

        [Fact]
        public void EventAdd_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            mylist.Add(0);

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("Add is executing; ");
            };
        }

        [Fact]
        public void EventClear_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            mylist.Clear();

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("Clear is executing; ");
            };
        }

        [Fact]
        public void EventContains_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            mylist.Notify += CompareMessage;

            // Act
            mylist.Contains(1);

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("Contains is executing; ");
            };
        }

        [Fact]
        public void EventCopyTo_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            int i = 0;
            int[] arr = new int[1];
            mylist.Notify += CompareMessage;

            // Act
            mylist.CopyTo(arr, 0);

            //Assert
            void CompareMessage(string message)
            {
                if(i == 0)
                    message.Should().Be("CopyTo is executing; ");
                i++;
            };
        }

        [Fact]
        public void EventRemove_NotNull_CorrectString()
        {
            // Arrange
            var mylist = new myList<int>() { 1 };
            int[] arr = new int[1];
            mylist.Notify += CompareMessage;

            // Act
            mylist.Remove(1);

            //Assert
            void CompareMessage(string message)
            {
                message.Should().Be("Remove is executing; ");
            };
        }

    }
}