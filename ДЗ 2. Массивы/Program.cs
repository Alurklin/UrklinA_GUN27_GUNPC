class Program
{
    static void Main()
    {
        // Задача A
        int[] fibonacciSequence = new int[8];
        fibonacciSequence[0] = 0;
        fibonacciSequence[1] = 1;
        for (int i = 2; i < fibonacciSequence.Length; i++)
        {
            fibonacciSequence[i] = fibonacciSequence[i - 1] + fibonacciSequence[i - 2];
        }

        string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        int[,] matrix = new int[3, 3];
        matrix[0, 0] = 2;
        matrix[0, 1] = 3;
        matrix[0, 2] = 4;
        matrix[1, 0] = 2 * 2;
        matrix[1, 1] = 3 * 3;
        matrix[1, 2] = 4 * 4;
        matrix[2, 0] = 2 * 2 * 2;
        matrix[2, 1] = 3 * 3 * 3;
        matrix[2, 2] = 4 * 4 * 4;

        double[][] jaggedArray = new double[3][];
        jaggedArray[0] = new double[] { 1, 2, 3, 4, 5 };
        jaggedArray[1] = new double[] { Math.E, Math.PI };
        jaggedArray[2] = new double[] { Math.Log10(1), Math.Log10(10), Math.Log10(100), Math.Log10(1000) };

        // Задача B
        int[] array = { 1, 2, 3, 4, 5 };
        int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };

        Array.Copy(array, 0, array2, 0, 3);

        Array.Resize(ref array, array.Length * 2);

        foreach (var item in array)
        {
            Console.WriteLine(item);
        }
    }
}