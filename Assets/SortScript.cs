using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SortScript : MonoBehaviour
{
    public Transform[] objects;
    public Transform[] pointers;
    public Transform holder;
    public MoveScript mover;
    int[] numbers;

    #region Setting up
    void Start()
    {
        #region Setup
        List<int> nums = new List<int>();
        for (int i = 0; i < objects.Length; i++)
        {
            nums.Add(i + 1);
        }
        numbers = new int[objects.Length];
        // random numbers   
        int r;
        for (int i = 0; i < numbers.Length; i++)
        {
            r = UnityEngine.Random.Range(0, nums.Count);
            numbers[i] = nums[r];
            nums.RemoveAt(r);
        }

        SetHight();
        #endregion

        /// Sorting
        //SelectionSort(numbers);
        //InsertionSort(numbers);
        ArrayBubbleSort(numbers);
    }

    public void SetHight()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].localScale = new Vector3(1, numbers[i] / 2f, 1);
            objects[i].position = new Vector3(objects[i].position.x, (numbers[i] / 4f) + 0.5f, objects[i].position.z);
        }
    }
    #endregion

    #region Swap
    public async Task exchange(int[] data, int m, int n)
    {
        int temporary = data[m];
        data[m] = data[n];
        data[n] = temporary;

        await mover.RoundSwapObjects(objects[m], objects[n]);

        Transform temp = objects[m];
        objects[m] = objects[n];
        objects[n] = temp;
    }
    public async Task exchange(int[] data, int m, int n, bool usingpointers)
    {
        int temporary = data[m];
        data[m] = data[n];
        data[n] = temporary;

        AttachPointers(objects[m], objects[n]);
        await mover.RoundSwapObjects(objects[m], objects[n]);
        AttachPointers(null, null);
        SwapPointer();

        Transform temp = objects[m];
        objects[m] = objects[n];
        objects[n] = temp;
    }
    public async Task exchange(int m, int n)
    {
        await mover.RoundSwapObjects(objects[m], objects[n]);

        Transform temp = objects[m];
        objects[m] = objects[n];
        objects[n] = temp;
    }

    public void AttachPointers(Transform obj1, Transform obj2)
    {
        pointers[0].SetParent(obj1);
        pointers[1].SetParent(obj2);
    }
    public void SwapPointer()
    {
        Transform temp = pointers[0];
        pointers[0] = pointers[1];
        pointers[1] = temp;
    }
    #endregion

    #region sorting
    public async void ArrayBubbleSort(int[] arr)
    {
        int N = arr.Length;
        Task[] tasks = new Task[2];

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length - 1; j++)
            {
                tasks[0] = pointers[0].GetComponent<PointerScript>().MoveTo(objects[j]);
                tasks[1] = pointers[1].GetComponent<PointerScript>().MoveTo(objects[j + 1]);
                await Task.WhenAll(tasks);

                if (arr[j] > arr[j + 1])
                {
                    await exchange(arr, j, j + 1, true);
                }
            }
        }
    }

    public async void InsertionSort(int[] arr)
    {
        Transform holdT;
        int key, j;
        for (int i = 1; i < arr.Length; ++i)
        {
            await pointers[0].GetComponent<PointerScript>().MoveTo(objects[i]);
            key = arr[i];
            holdT = objects[i];
            objects[i] = holder;
            await mover.RoundSwapObjects(holdT, holder);

            j = i - 1;

            if (!(j >= 0 && arr[j] > key)) await Task.Delay(200);

            while (j >= 0 && arr[j] > key)
            {
                await pointers[1].GetComponent<PointerScript>().MoveTo(objects[j]);
                await exchange(j + 1, j);

                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
            await mover.RoundSwapObjects(objects[j + 1], holdT);
            objects[j + 1] = holdT;
            holdT = holder;
        }
    }

    public async void SelectionSort(int[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            var min = i;
            await pointers[0].GetComponent<PointerScript>().MoveTo(objects[min]);
            for (var j = i + 1; j < input.Length; j++)
            {
                await pointers[1].GetComponent<PointerScript>().MoveTo(objects[j]);
                if (input[min] > input[j])
                {
                    min = j;
                    await pointers[0].GetComponent<PointerScript>().MoveTo(objects[min]);
                }
            }
            if (min != i)
            {
                await exchange(input, i, min);
            }
        }
    }
    #endregion
}
