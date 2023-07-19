using Basics.Units;
using Xunit;

namespace Basics.Tests
{
    public class ToDoListTests
    {
        [Fact]
        public void Add_SavesTodoItem()
        {
            // arrange
            var list = new TodoList();

            // act
            list.Add(new("Test Content"));

            // assert
            var savedItem = Assert.Single(list.All);
            Assert.NotNull(savedItem);
            Assert.Equal(1, savedItem.Id);
            Assert.Equal("Test Content", savedItem.Content);
            Assert.False(savedItem.Complete);
        }

        [Fact]
        public void TodoItem_Id_Increments_Every_Time_We_Add()
        {
            var list = new TodoList();

            list.Add(new("Test Content 1"));
            list.Add(new("Test Content 2"));

            var items = list.All.ToArray();
            Assert.Equal(1, items[0].Id);
            Assert.Equal(2, items[1].Id);
        }

        [Fact]
        public void Complete_Sets_TodoItem_Compelete_Flag_To_True()
        {
            var list = new TodoList();
            list.Add(new("Test Content 1"));

            list.Complete(1);

            var completedItem = Assert.Single(list.All);
            Assert.NotNull(completedItem);
            Assert.Equal(1, completedItem.Id);
            Assert.True(completedItem.Complete);
        }
    }
}
