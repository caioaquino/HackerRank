using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class DoubleHeap{
    public List<int> minHeap {get;set;}
    public List<int> maxHeap {get;set;}
    public DoubleHeap(){
        this.minHeap = new();
        this.maxHeap = new();
    }
    
    public int PeekMin(){
        return this.minHeap[0];
    }
    public int PeekMax(){
        return this.maxHeap[0];
    }
    public void Insert(int value){
        if(this.maxHeap.Count == 0 || value <= PeekMax()){
            InsertMaxHeap(value);
        }else{
            InsertMinHeap(value);
        }
        
 
        BalanceHeaps();
    }
        private void BalanceHeaps() {
        if (this.maxHeap.Count > this.minHeap.Count + 1) {
            int peek = RemoveMaxPeek();
            InsertMinHeap(peek);
        } else if (this.minHeap.Count > this.maxHeap.Count + 1) {
            int peek = RemoveMinPeek();
            InsertMaxHeap(peek);
        }
    }
    public void InsertMaxHeap(int value)
    {
        this.maxHeap.Add(value);
        HeapifyUpMax(this.maxHeap.Count - 1);
    }
    
    public void InsertMinHeap(int value)
    {
        this.minHeap.Add(value);
        HeapifyUpMin(this.minHeap.Count - 1);
    }
    public void HeapifyUpMin(int index){
        if(index <=0) return;
        
        int parentIndex = (index - 1) / 2;
        
        if(this.minHeap[index] >= this.minHeap[parentIndex])
            return;
        
        Swap(index,parentIndex,this.minHeap);
        HeapifyUpMin(parentIndex);
    }
    
    public void HeapifyUpMax(int index){
        if(index <= 0) return;
        
        int parentIndex = (index-1)/2;
        
        if(this.maxHeap[index] <= this.maxHeap[parentIndex]) return;
        
        Swap(index,parentIndex,this.maxHeap);
        HeapifyUpMax(parentIndex);
    }
    
    public int RemoveMinPeek(){
        int peek = PeekMin();
        Swap(0,this.minHeap.Count-1,this.minHeap);
        this.minHeap.RemoveAt(this.minHeap.Count -1);
        HeapifyDownMin(0);
        return peek;
    }
    
    public int RemoveMaxPeek(){
        int peek = PeekMax();
        Swap(0,this.maxHeap.Count-1,this.maxHeap);
        this.maxHeap.RemoveAt(this.maxHeap.Count -1);
        HeapifyDownMax(0);
        return peek;
    }
    
    public void HeapifyDownMin(int index){
        int leftChildIndex = (index*2) + 1;
        int rightChildIndex = (index*2) + 2;
        int smallerIndex = index;
        if(leftChildIndex < this.minHeap.Count && this.minHeap[leftChildIndex] < this.minHeap[smallerIndex]){
            smallerIndex = leftChildIndex;
        }
        if(rightChildIndex < this.minHeap.Count && this.minHeap[rightChildIndex] < this.minHeap[smallerIndex]){
            smallerIndex = rightChildIndex;
        }
        if(smallerIndex != index){
        Swap(smallerIndex,index,this.minHeap);
        HeapifyDownMin(smallerIndex);
    }}


    public void HeapifyDownMax(int index){
        int leftChildIndex = (index*2) + 1;
        int rightChildIndex = (index*2) + 2;
        int largerIndex = index;
        if(leftChildIndex < this.maxHeap.Count && this.maxHeap[leftChildIndex] > this.maxHeap[largerIndex]){
            largerIndex = leftChildIndex;
        }
        if(rightChildIndex < this.maxHeap.Count && this.maxHeap[rightChildIndex] > this.maxHeap[largerIndex]){
            largerIndex = rightChildIndex;
        }
        
        if(largerIndex != index){
        Swap(largerIndex,index,this.maxHeap);
        HeapifyDownMax(largerIndex);}
    }
    
    public void Swap(int i1, int i2, List<int> root){
        int aux = root[i1];
        root[i1] = root[i2];
        root[i2] = aux;
    }
    
    
}


class Result
{
    
    /*
     * Complete the 'runningMedian' function below.
     *
     * The function is expected to return a DOUBLE_ARRAY.
     * The function accepts INTEGER_ARRAY a as parameter.
     */

    public static List<double> runningMedian(List<int> a)
    {
        DoubleHeap heap = new();
        int half = a.Count/2;
        List<double> medians = [];
        for(int i = 0 ; i < a.Count ; i++){
            heap.Insert(a[i]);
            double median;
            
            int maxCount= heap.maxHeap.Count;
            int minCount = heap.minHeap.Count;
            int totalCount =maxCount  + minCount;
            if(totalCount % 2 == 0){
                median = ((float) heap.PeekMax() + (float) heap.PeekMin()) / 2;
            }else {
            if (maxCount > minCount)
                median = heap.PeekMax();
            else
                median = heap.PeekMin();
            }
            medians.Add(median);
        }
        
        return medians;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int aCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> a = new List<int>();

        for (int i = 0; i < aCount; i++)
        {
            int aItem = Convert.ToInt32(Console.ReadLine().Trim());
            a.Add(aItem);
        }

        List<double> result = Result.runningMedian(a);
        List<string> resultStr = result.Select(x => string.Format("{0:F1}",x)).ToList();
        textWriter.WriteLine(String.Join("\n", resultStr));

        textWriter.Flush();
        textWriter.Close();
    }
}
