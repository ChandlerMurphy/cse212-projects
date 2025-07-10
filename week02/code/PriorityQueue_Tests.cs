using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: The Enqueue function shall add an item with both data and priority to the back of the queue.The Dequeue function shall remove the item with the highest priority and return its value.
    // Expected Result: priorityQueue = [mike("Mike", 2), dax("Dax", 1)]
    // Defect(s) Found: The last item in the stack is not checked by the Dequeue method so I had to go add that. 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        var josh = new PriorityItem ("Josh", 2);
        var mike = new PriorityItem ("Mike", 3);
        var dax = new PriorityItem ("Dax", 1);

        PriorityItem[] expectedResult = new PriorityItem[] {josh, mike, dax};

        priorityQueue.Enqueue(josh.Value, josh.Priority);
        priorityQueue.Enqueue(mike.Value, mike.Priority);
        priorityQueue.Enqueue(dax.Value, dax.Priority);

        var dequeuedValue = priorityQueue.Dequeue();
        Assert.AreEqual(expectedResult[1].Value, dequeuedValue);
    }

    [TestMethod]
    // Scenario: In cases where multiple items share the same priority pick the first in the stack.
    // Expected Result: Mike should be dequeued first, then Josh.
    // Defect(s) Found: The last person in the Queue is being taken off the stack when the first should be. In addition, the Dequeue method does not actually remove the item from the list.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        var mike = new PriorityItem("Mike", 3);
        var josh = new PriorityItem("Josh", 3);
        var dax = new PriorityItem("Dax", 3);

        priorityQueue.Enqueue(mike.Value, mike.Priority);
        priorityQueue.Enqueue(josh.Value, josh.Priority);
        priorityQueue.Enqueue(dax.Value, dax.Priority);

        // Mike should be dequeued first
        var dequeuedValue = priorityQueue.Dequeue();
        Assert.AreEqual(mike.Value, dequeuedValue, "We expected Mike but got someone else.");

        // Josh should be dequeued second
        var secondDequeued = priorityQueue.Dequeue();
        Assert.AreEqual(josh.Value, secondDequeued, "We expected Josh but got someone else.");
    }

    [TestMethod]
    // Scenario: If the queue is empty then an error exception shall be thrown.
    // Expected Result: A "The queue is empty." error should be thrown. 
    // Defect(s) Found: 
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        var mike = new PriorityItem("Mike", 3);

        priorityQueue.Enqueue(mike.Value, mike.Priority);
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();
    }

    // Add more test cases as needed below.
}