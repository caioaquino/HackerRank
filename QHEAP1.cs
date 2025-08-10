using System;
using System.Collections.Generic;
using System.IO;
class Solution {

    static void Main(String[] args) {
        int q = int.Parse(Console.ReadLine());
        MinHeap heap = new();
        for(int i=0;i < q ; i++){
            string[] input = Console.ReadLine().Split(" ");
            Operation operation = (Operation) int.Parse(input[0]);
            int? value = input.Length > 1 ? int.Parse(input[1]) : null;
            handleOperation(operation,heap,value);
        }
    }
    
    public static void handleOperation(Operation operation,MinHeap heap, int? value=null){
       // Console.WriteLine($"Operation {operation}, value {value}");
        switch(operation){
            case Operation.ADD:
                heap.Insert((int)value);
                break;
            case Operation.PRINT:
               Console.WriteLine(heap.Peek());
                break;
            case Operation.DELETE:
                heap.Remove((int)value);
                break;
        }
  //   Console.WriteLine($"Heap {string.Join(" ",heap.GetHeap())}");

    }
}


class MinHeap{
    List<int> heap {get; set;}
    
    public List<int> GetHeap(){
        return this.heap;
    }
    
    public MinHeap(){
        this.heap = new();
    }
    
    public void Insert(int value){
        this.heap.Add(value);
        HeapifyUp(this.heap.Count - 1);
    }
    public int Peek(){
       return this.heap[0];
    }
    public void Remove(int value){
        int valueIndex = this.heap.FindIndex(x => x== value);
        Swap(valueIndex,this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count-1);
        if(valueIndex < this.heap.Count){
        HeapifyUp(valueIndex);
        HeapifyDown(valueIndex);}
    }
    
    public void HeapifyUp(int index){
        
        while(index > 0){
            int parentIndex = (index - 1) / 2;
           // Console.WriteLine($"parentIndex {parentIndex}");
            // propriedade de heap
            if( this.heap[index] >= this.heap[parentIndex])
                return;
                
            Swap(index,parentIndex);
            index = parentIndex;
        }
    }
    
    void HeapifyDown(int index){
            int leftChildIndex = (index * 2) + 1;
            int rightChildIndex = (index * 2) + 2;
            int smallestIndex = index;
           // Console.WriteLine($"leftChildIndex {leftChildIndex}");
           // Console.WriteLine($"rightChildIndex {rightChildIndex}");
            // Console.WriteLine($"heap {string.Join(" ",heap)}");

            if(leftChildIndex < this.heap.Count  && this.heap[leftChildIndex] < this.heap[smallestIndex]){
                smallestIndex=leftChildIndex;
            }
            if(rightChildIndex < this.heap.Count  && this.heap[rightChildIndex] < this.heap[smallestIndex]){
                smallestIndex = rightChildIndex;
            }
            
            if(smallestIndex != index){
                Swap(index,smallestIndex);
                HeapifyDown(smallestIndex);
            }
    }
        
    void Swap(int indexChild, int indexParent){
        int aux = this.heap[indexParent];
        this.heap[indexParent] = this.heap[indexChild];
        this.heap[indexChild] = aux;
    }
    
    
}

enum Operation{
    ADD = 1,
    DELETE = 2,
    PRINT = 3
}
