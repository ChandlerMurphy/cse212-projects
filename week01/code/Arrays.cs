public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Create a new array of type double with a size equal to 'length'.
        // Step 2: Loop from 0 to length - 1.
        // Step 3: In each loop through, multiply the starting number by (i + 1) and store the result in the array at that index. 
        // Step 4: After the loop, return the filled array.

        double[] multiples = new double[length]; // Step 1
        for (int i = 0; i < length; i++) // Step 2
        {
            multiples[i] = number * (i + 1); // Step 3
        }
        return multiples; // Step 4
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Understand the problem which is to rotate the list to the right by 'amount' positions.
        // Example: data = {1,2,3,4,5,6,7,8,9}, amount = 5
        // Result after rotation: {5,6,7,8,9,1,2,3,4}

        // Step 2: Since amount is guaranteed to be between 1 and data.Count, we don't need to worry about invalid input or zero rotations.

        // Step 3: The rotation means the last 'amount' elements move to the front of the list.
        // So, extract the last 'amount' elements using GetRange(startIndex, count):
        // data.Count - amount = startIndex
        // amount = how many numbers to grab after the index
        List<int> tail = data.GetRange(data.Count - amount, amount);

        // Step 4: Remove the last 'amount' elements from the original list using RemoveRange:
        data.RemoveRange(data.Count - amount, amount);

        // Step 5: Insert the extracted 'tail' at the beginning of the list using InsertRange:
        data.InsertRange(0, tail);
    }
}
